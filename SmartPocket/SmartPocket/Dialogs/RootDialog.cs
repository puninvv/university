using SmartPocket.DALC;
using SmartPocket.Dialogs;
using SmartPocket.Dialogs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartPocket.Dialogs
{
    class RootDialog : IUserDialog
    {
        public DialogType DialogType => DialogType.Default;

        public List<ICommand> SupportedCommands { get; set; } = new List<ICommand>();

        public string Greeting => "Вы в корневом диалоге. Введите отправьте мне что-нибудь";

        public bool ProcessMessage(Message _message, ITelegramBotClient _bot, DALC.User _user)
        {
            var user = UserDalc.GetUser(_message.Chat.Username);
            if (user == null)
            {
                _bot.SendTextMessageAsync(_message.Chat.Id, "я тебя не знаю, иди нахуй");
                return true;
            }

            var lowerInvariantMessage = _message.Text.ToLower();

            var commandTmp = SupportedCommands.FirstOrDefault(c => c.SwitchTo.DialogType == user.DialogType);
            if (commandTmp != null)
            {
                commandTmp.SwitchTo.ProcessMessage(_message, _bot, user);
                return true;
            }

            foreach (var command in SupportedCommands)
            {
                if (lowerInvariantMessage.StartsWith(command.Command))
                {
                    user.DialogType = command.SwitchTo.DialogType;

                    UserDalc.CreateOrUpdateUser(user);

                    if (!string.IsNullOrWhiteSpace(command.SwitchTo.Greeting))
                        _bot.SendTextMessageAsync(_message.Chat.Id, command.SwitchTo.Greeting);

                    command.SwitchTo.ProcessMessage(_message, _bot, user);

                    return true;
                }
            }

            var errorMessage = new StringBuilder();
            errorMessage.AppendLine("Не понял тебя:( выбери, что хочешь сделать:");
            foreach (var command in SupportedCommands)
                errorMessage.Append(command.Command).Append(" - ").AppendLine(command.Info);

            _bot.SendTextMessageAsync(_message.Chat.Id, errorMessage.ToString());
            return false;
        }

        public RootDialog()
        {
            SupportedCommands.Add(new NewUserCommand());
            SupportedCommands.Add(new GetUserInfoCommand());
            SupportedCommands.Add(new GetAllUsers());
        }
    }
}
