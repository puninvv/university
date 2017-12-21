using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPocket.DALC;
using SmartPocket.Dialogs.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SmartPocket.Dialogs
{
    public class ReceiveTransactionDialog : IUserDialog
    {
        public string Greeting { get => string.Empty; set { } }
        public List<ICommand> SupportedCommands { get => new List<ICommand>(); set { } }

        public bool IsUserFrom { get; set; } = false;
        public bool IsUserFromAsked { get; set; } = false;
        public string SelectedInfo { get; set; } = string.Empty;
        public bool IsUserFromSelected { get; set; } = false;
        public DALC.User UserTo { get; set; } = null;

        public decimal Money { get; set; } = 0;

        public void ProcessMessage(Message _message, ITelegramBotClient _bot, DALC.User _user)
        {
            if (!IsUserFrom)
            {
                _bot.SendTextMessageAsync(_message.Chat.Id, "От кого?");
                IsUserFrom = true;
                goto end;
            }

            if (!IsUserFromSelected && !IsUserFromAsked)
            {
                var userName = _message.Text;
                SelectedInfo = userName;

                UserTo = UserDalc.GetUser(userName);
                if (UserTo != null)
                {
                    _bot.SendTextMessageAsync(_message.Chat.Id, "Сколько?", replyMarkup: new ReplyKeyboardRemove());
                    IsUserFromSelected = true;
                    IsUserFromAsked = true;
                }
                else
                {
                    _bot.SendTextMessageAsync(_message.Chat.Id, "Не нашёл такого, пробую искать по информации о пользователях...");

                    var users = UserDalc.GetUsersByInfo(userName);
                    if (users.Count != 0)
                    {
                        var board = new List<KeyboardButton[]>();

                        for (int i = 0; i < users.Count; i++)
                            board.Add(new KeyboardButton[] { new KeyboardButton(string.Concat(i, "\t", users[i].ToStringMinInfo())) });

                        var keyboard = new ReplyKeyboardMarkup(board.ToArray());

                        _bot.SendTextMessageAsync(_message.Chat.Id, "Выберите:", replyMarkup: keyboard);
                        IsUserFromAsked = true;
                    }
                    else
                    {
                        _bot.SendTextMessageAsync(_message.Chat.Id, "Всё равно не нашёл... Дайте ещё какую-нибудь зацепочку!");
                        IsUserFromAsked = false;
                    }

                    IsUserFromSelected = false;
                }

                goto end;
            }

            if (!IsUserFromSelected && IsUserFromAsked)
            {
                var users = UserDalc.GetUsersByInfo(SelectedInfo);

                int index = -1;
                try
                {
                    index = int.Parse(_message.Text.Split(new string[] { " " }, StringSplitOptions.None).First());
                    if (index < 0 || index >= users.Count)
                        index = -1;
                }
                catch (Exception ex) { }


                if (index != -1)
                {
                    UserTo = users[index];
                    _bot.SendTextMessageAsync(_message.Chat.Id, "Сколько?", replyMarkup: new ReplyKeyboardRemove());
                    IsUserFromSelected = true;
                    IsUserFromAsked = true;
                    goto end;
                }
                else
                {
                    IsUserFromAsked = false;
                    IsUserFromSelected = false;
                    UserTo = null;
                    _bot.SendTextMessageAsync(_message.Chat.Id, "Чёт вообще не понял, давайте ещё разок уточним, кого же мы ищем?", replyMarkup: new ReplyKeyboardRemove());
                    goto end;
                }
            }

            try
            {
                Money = decimal.Parse(_message.Text);

                var trId = TransactionDalc.CreateTransaction(_user.Id.Value, UserTo.Id.Value, Money);
                TransactionDalc.SetTransactionState(trId.Value, TransactionState.Confirmed);

                _user.DialogContext = new RootDialog().SerializeToJson();
                UserDalc.CreateOrUpdateUser(_user);
                _bot.SendTextMessageAsync(_message.Chat.Id, "Запомнил:)");
                return;
            }
            catch (Exception ex)
            {
                _bot.SendTextMessageAsync(_message.Chat.Id, ex.ToString());
                IsUserFrom = false;
                IsUserFromSelected = false;
            }

            end:
            _user.DialogContext = this.SerializeToJson();
            UserDalc.CreateOrUpdateUser(_user);
        }
    }
}
