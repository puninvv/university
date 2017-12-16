using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPocket.DALC;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartPocket.Dialogs.Commands
{
    class GetUserInfoCommand : ICommand
    {
        public string Command => "покажи инфо";

        public string Info => "вывод информации по пользователю";

        public IUserDialog SwitchTo => new GetUserInfoDialog();
    }

    class GetUserInfoDialog : IUserDialog
    {
        public DialogType DialogType => DialogType.GetUserInfo;

        public string Greeting => "Введите логин пользователя в телеграмме";

        public List<ICommand> SupportedCommands => new List<ICommand>() { new ToRootDialogCommand()};

        public bool ProcessMessage(Message _message, ITelegramBotClient _bot, DALC.User _user)
        {
            var user = UserDalc.GetUser(_message.Text);
            if (user == null)
            {
                _bot.SendTextMessageAsync(_message.Chat.Id, "Не нашёл такого");
                return false;
            }

            var result = new StringBuilder();
            result.AppendLine(user.FirstName);
            result.AppendLine(user.LastName);
            result.AppendLine(user.Info);

            _bot.SendTextMessageAsync(_message.Chat.Id, result.ToString());
            return true;
        }
    }
}
