using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartPocket.DALC;
using SmartPocket.Dialogs.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SmartPocket.Dialogs
{
    public class GetTransactionsDialog : IUserDialog
    {
        public string Greeting { get => string.Empty; set { } }
        public List<ICommand> SupportedCommands { get => new List<ICommand>(); set { } }

        public bool IsDirectionAsked { get; set; } = false;
        public bool Outside { get; set; } = false;
        public bool Inside { get; set; } = false;

        public bool IsStateAsked { get; set; } = false;
        public bool OnlyConfirmed { get; set; } = false;

        public void ProcessMessage(Message _message, ITelegramBotClient _bot, DALC.User _user)
        {
            if (!IsDirectionAsked)
            {
                var keyboard = new ReplyKeyboardMarkup(new[]
                {
                    new[] 
		            {
                        new KeyboardButton("Входящие"),
                        new KeyboardButton("Исходящие"),
                        new KeyboardButton("Все"),
                    },
                });

                _bot.SendTextMessageAsync(_message.Chat.Id, "Входящие? Исходящие? Все?", replyMarkup: keyboard);

                IsDirectionAsked = true;
                goto end;
            }

            if (!IsStateAsked)
            {
                var isSelected = _message.Text.Equals("Входящие") || (_message.Text.Equals("Исходящие")) || (_message.Text.Equals("Все"));

                if (isSelected)
                {
                    if (_message.Text.Equals("Входящие")) Inside = true;
                    else
                    if (_message.Text.Equals("Исходящие")) Outside = true;
                    else
                    if (_message.Text.Equals("Все"))
                    {
                        Inside = true;
                        Outside = true;
                    }

                    var keyboard = new ReplyKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            new KeyboardButton("Все"),
                            new KeyboardButton("Подтверждённые"),
                        },
                    });

                    _bot.SendTextMessageAsync(_message.Chat.Id, "Все, или только подтверждённые?", replyMarkup: keyboard);

                    IsStateAsked = true;
                    goto end;
                }
                else
                {
                    _bot.SendTextMessageAsync(_message.Chat.Id, "Не понял, ещё разок:)");
                    IsStateAsked = false;
                    goto end;
                } 
            }

            try
            {
                var transactions = TransactionDalc.GetTransactionsOfUser(_user.Id.Value, DateTime.Now.AddDays(-30), DateTime.Now, Outside & !Inside, OnlyConfirmed);
                var csv = Transaction.ToCsv(transactions, ";", Environment.NewLine);
                var tmpName = (_user.FirstName+"_" + _user.LastName + DateTime.Now.ToShortDateString() + ".csv");

                System.IO.File.WriteAllText(tmpName, csv);
                var filePath = Path.GetFullPath(tmpName);

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var fts = new FileToSend(filePath, stream);

                    var result = _bot.SendDocumentAsync(_message.Chat.Id, fts, replyMarkup: new ReplyKeyboardRemove());

                    while (!(result.IsCompleted || result.IsCanceled || result.IsFaulted)) { Thread.Sleep(10); }
                }

                System.IO.File.Delete(filePath);

                _bot.SendTextMessageAsync(_message.Chat.Id, "Отправил все, что нашёл :)", replyMarkup: new ReplyKeyboardRemove());

                _user.DialogContext = new RootDialog().SerializeToJson();
                UserDalc.CreateOrUpdateUser(_user);
                return;
            }
            catch (Exception ex)
            {
                _bot.SendTextMessageAsync(_message.Chat.Id, ex.ToString());
                IsDirectionAsked = false;
                IsStateAsked = false;
            }

            end:
            _user.DialogContext = this.SerializeToJson();
            UserDalc.CreateOrUpdateUser(_user);
        }
    }
}
