using System.Collections.Generic;
using SmartPocket.DALC;
using SmartPocket.Dialogs;
using SmartPocket.Dialogs.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

public class RootDialog : IUserDialog
{
    private string m_defaultContext = string.Empty;

    public string Greeting { get => "Добрый день! Чем могу быть полезен?"; set { } }
    public List<ICommand> SupportedCommands { get => new List<ICommand>() { new GetAllUsersCommand(), new AddUserCommand(), new SendTransactionCommand(), new GetTransactionsCommand() }; set { } }
    
    public RootDialog()
    {
        m_defaultContext = this.SerializeToJson();
    }

    public void ProcessMessage(Message _message, ITelegramBotClient _bot, SmartPocket.DALC.User _user)
    {
        var lowerText = _message.Text.ToLowerInvariant();

        var currentTransaction = TransactionDalc.GetTransactionCurrent(_user.Id.Value);
        if (currentTransaction != null)
        {
            DialogsFactory.CreateFrom(DialogType.ProcessCurrentTransactions).ProcessMessage(_message, _bot, _user);
            return;
        }

        foreach (var command in SupportedCommands)
        {
            if (lowerText.StartsWith(command.Command))
            {
                DialogsFactory.CreateFrom(command.SwitchTo).ProcessMessage(_message, _bot, _user);
                return;
            }
        }

        this.SendSupportedCommandsMessage(_message, _bot);
    }
}