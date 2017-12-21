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
    public class GetAllUsersDialog : IUserDialog
    {
        public string Greeting { get => "Давай посмотрим, кто тут у нас есть:)"; set { } }
        public List<ICommand> SupportedCommands { get => new List<ICommand>(); set { } }

        public void ProcessMessage(Message _message, ITelegramBotClient _bot, DALC.User _user)
        {
            if (_user.Role != UserRole.ZeroLevel && _user.Role != UserRole.FirstLevel)
            {
                _bot.SendTextMessageAsync(_message.Chat.Id, "Прав у тебя не достаточно");
                return;
            }

            var users = UserDalc.GetUsers();
            var sb = new StringBuilder();

            foreach (var user in users)
                sb.AppendLine(user.ToStringMinInfo());

            _bot.SendTextMessageAsync(_message.Chat.Id, sb.ToString());
        }
    }
}
