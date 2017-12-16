using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPocket.DALC;
using SmartPocket.Dialogs.Commands;
using SmartPocket.Handlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartPocket.Dialogs
{
    class CreateUserDialog : IUserDialog
    {
        public DialogType DialogType => DialogType.CreateUser;

        public string Greeting => string.Empty;

        public List<ICommand> SupportedCommands => throw new NotImplementedException();

        public bool ProcessMessage(Message _message, ITelegramBotClient _bot, DALC.User _user)
        {
            var context = CreateUserDialogContext.CreateFrom(_user.DialogContext);

            if (!context.IsFirstNameAsked)
            {
                _bot.SendTextMessageAsync(_message.Chat.Id, "Введите его имя:");
                context.IsFirstNameAsked = true;
                _user.DialogContext = context.ToString();
                UserDalc.CreateOrUpdateUser(_user);
                return true;
            }


            if (context.IsFirstNameAsked && !context.IsLastNameAsked)
            {
                context.NewUser.FirstName = _message.Text;
                _bot.SendTextMessageAsync(_message.Chat.Id, "Введите фамилию:");
                context.IsLastNameAsked = true;
                _user.DialogContext = context.ToString();
                UserDalc.CreateOrUpdateUser(_user);
                return true;
            }

            if (context.IsFirstNameAsked && context.IsLastNameAsked && !context.IsInfoAsked)
            {
                context.NewUser.LastName = _message.Text;
                _bot.SendTextMessageAsync(_message.Chat.Id, "Введите дополнительную информацию:");
                context.IsInfoAsked = true;
                _user.DialogContext = context.ToString();
                UserDalc.CreateOrUpdateUser(_user);
                return true;
            }

            if (context.IsFirstNameAsked && context.IsLastNameAsked && context.IsInfoAsked && !context.IsTelegramUserNameAsked)
            {
                context.NewUser.Info = _message.Text;
                _bot.SendTextMessageAsync(_message.Chat.Id, "Введите его логин в телеграме:");
                context.IsTelegramUserNameAsked = true;
                _user.DialogContext = context.ToString();
                UserDalc.CreateOrUpdateUser(_user);
                return true;
            }

            if (context.IsFirstNameAsked && context.IsLastNameAsked && context.IsInfoAsked && context.IsTelegramUserNameAsked && !context.IsRoleAsked)
            {
                context.NewUser.TelegramUserName = _message.Text;
                _bot.SendTextMessageAsync(_message.Chat.Id, "Введите его роль (0, 1, 2, 3):");
                context.IsRoleAsked = true;
                _user.DialogContext = context.ToString();
                UserDalc.CreateOrUpdateUser(_user);
                return true;
            }

            if (context.IsFirstNameAsked && context.IsLastNameAsked && context.IsInfoAsked && context.IsTelegramUserNameAsked && context.IsRoleAsked && !context.IsUserCreated)
            {
                try
                {
                    context.NewUser.Role = (UserRole)int.Parse(_message.Text);
                    UserDalc.CreateOrUpdateUser(context.NewUser);

                    _user.DialogType = DialogType.Default;
                    _user.DialogContext = string.Empty;
                    UserDalc.CreateOrUpdateUser(_user);
                    _bot.SendTextMessageAsync(_message.Chat.Id, "Угу, создал:)");
                }
                catch (Exception ex)
                {
                    Logger.Log.Error("exception occured:", ex);


                    _bot.SendTextMessageAsync(_message.Chat.Id, "Не понял. Введите его роль (0, 1, 2, 3):");
                    context.IsRoleAsked = false;
                    _user.DialogContext = context.ToString();

                    return false;
                }
            }

            return false;
        }
    }

    public class CreateUserDialogContext
    {
        public DALC.User NewUser { get; set; } = new DALC.User();

        public bool IsFirstNameAsked { get; set; }
        public bool IsLastNameAsked { get; set; }
        public bool IsInfoAsked { get; set; }
        public bool IsRoleAsked { get; set; }
        public bool IsTelegramUserNameAsked { get; set; }
        public bool IsUserCreated { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static CreateUserDialogContext CreateFrom(string _json)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<CreateUserDialogContext>(_json) ?? new CreateUserDialogContext();
            }
            catch (Exception ex)
            {
                return new CreateUserDialogContext();
            }
        }
    }
}
