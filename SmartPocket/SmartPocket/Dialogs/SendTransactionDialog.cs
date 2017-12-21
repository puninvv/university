using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPocket.DALC;
using SmartPocket.Dialogs.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartPocket.Dialogs
{
    public class SendTransactionDialog : IUserDialog
    {
        public string Greeting { get => string.Empty; set { } }
        public List<ICommand> SupportedCommands { get => new List<ICommand>(); set { } }

        public bool IsUserToAsked { get; set; } = false;
        public bool IsUserToSelected { get; set; } = false;
        public DALC.User UserTo { get; set; } = null;

        public decimal Money { get; set; } = 0;


        public void ProcessMessage(Message _message, ITelegramBotClient _bot, DALC.User _user)
        {
            if (!IsUserToAsked)
            {
                _bot.SendTextMessageAsync(_message.Chat.Id, "Кому?");
                IsUserToAsked = true;
                goto end;
            }

            if (!IsUserToSelected)
            {
                var userName = _message.Text;

                UserTo = UserDalc.GetUser(userName);
                if (UserTo != null)
                {
                    _bot.SendTextMessageAsync(_message.Chat.Id, "Сколько?");
                    IsUserToSelected = true;
                }
                else
                {
                    _bot.SendTextMessageAsync(_message.Chat.Id, "Не нашёл такого, попробуйте поискать по номеру телефона?");
                    IsUserToSelected = false;
                }

                goto end;
            }

            try
            {
                Money = decimal.Parse(_message.Text);

                TransactionDalc.CreateTransaction(_user.Id.Value, UserTo.Id.Value, Money);
                _user.DialogContext = new RootDialog().SerializeToJson();
                UserDalc.CreateOrUpdateUser(_user);

                _bot.SendTextMessageAsync(_message.Chat.Id, "Отправил:)");
                return;
            }
            catch (Exception ex)
            {
                _bot.SendTextMessageAsync(_message.Chat.Id, ex.ToString());
                IsUserToAsked = false;
                IsUserToSelected = false;
            }

            end:
            _user.DialogContext = this.SerializeToJson();
            UserDalc.CreateOrUpdateUser(_user);
        }
    }
}
