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
    class GetAllUsers : ICommand
    {
        public string Command => "пользователи";

        public string Info => "отобразить список пользователей";

        public IUserDialog SwitchTo => new GetAllUsersDialog();
    }

    class GetAllUsersDialog : IUserDialog
    {
        public DialogType DialogType => DialogType.GetAllUsers;

        public string Greeting => "Ок, сейчас покажу";

        public List<ICommand> SupportedCommands => new List<ICommand>() { new ToRootDialogCommand()};

        public bool ProcessMessage(Message _message, ITelegramBotClient _bot, DALC.User _user)
        {
            try
            {
                if (_user.Role != UserRole.ZeroLevel && _user.Role != UserRole.FirstLevel)
                {
                    _bot.SendTextMessageAsync(_message.Chat.Id, "Прав у тебя не достаточно");
                    return false;
                }

                var users = UserDalc.GetUsers();
                var sb = new StringBuilder();

                foreach (var user in users)
                    sb.AppendLine(user.ToStringMinInfo());

                _bot.SendTextMessageAsync(_message.Chat.Id, sb.ToString());
                _user.DialogType = DialogType.Default;
                UserDalc.CreateOrUpdateUser(_user);

                return true;
            }
            catch (Exception ex)
            {
                _bot.SendTextMessageAsync(_message.Chat.Id, ex.ToString());
                return false;
            }
        }
    }
}
