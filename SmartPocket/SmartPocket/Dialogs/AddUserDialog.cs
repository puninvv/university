using SmartPocket.DALC;
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
    public class AddUserDialog : IUserDialog
    {
        public string Greeting { get => ""; set { }}
        public List<ICommand> SupportedCommands { get => new List<ICommand>(); set { } }

        public bool IsFirstNameAsked { get; set; } = false;
        public bool IsLastNameAsked { get; set; } = false;
        public bool IsPhoneNumberAsked { get; set; } = false;
        public bool IsTelegramLoginAsked { get; set; } = false;
        public DALC.User NewUser { get; set; } = new DALC.User();


        public void ProcessMessage(Message _message, ITelegramBotClient _bot, DALC.User _user)
        {
            if (!IsFirstNameAsked)
            {
                _bot.SendTextMessageAsync(_message.Chat.Id, "Введите имя пользователя:");
                IsFirstNameAsked = true;
                goto end;
            }

            if (!IsLastNameAsked)
            {
                NewUser.FirstName = _message.Text;
                _bot.SendTextMessageAsync(_message.Chat.Id, "Введите его фамилию:");
                IsLastNameAsked = true;
                goto end;
            }

            if (!IsPhoneNumberAsked)
            {
                NewUser.LastName = _message.Text;
                _bot.SendTextMessageAsync(_message.Chat.Id, "Введите номер телефона");
                IsPhoneNumberAsked = true;
                goto end;
            }

            if (!IsTelegramLoginAsked)
            {
                NewUser.Info = _message.Text;
                _bot.SendTextMessageAsync(_message.Chat.Id, "Введите его логин в телеграме");
                IsTelegramLoginAsked = true;
                goto end;
            }

            try
            {
                NewUser.TelegramUserName = _message.Text;
                NewUser = UserDalc.CreateOrUpdateUser(NewUser);
                _bot.SendTextMessageAsync(_message.Chat.Id, "Создал:)");
                _user.DialogContext = new RootDialog().SerializeToJson();
                UserDalc.CreateOrUpdateUser(_user);
                return;
            }
            catch (Exception ex)
            {
                _bot.SendTextMessageAsync(_message.Chat.Id, ex.ToString());
                IsFirstNameAsked = false;
                IsLastNameAsked = false;
                IsPhoneNumberAsked = false;
                IsTelegramLoginAsked = false;
            }

            end:
            _user.DialogContext = this.SerializeToJson();
            UserDalc.CreateOrUpdateUser(_user);
        }
    }
}
