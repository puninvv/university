using System;
using System.Collections.Generic;
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
    class ProcessCurrentTransactionsDialog : IUserDialog
    {
        public string Greeting { get => string.Empty; set { } }
        public List<ICommand> SupportedCommands { get => new List<ICommand>(); set { } }

        public bool IsAsked { get; set; } = false;
        public Transaction CurrentTransaction = null;

        public void ProcessMessage(Message _message, ITelegramBotClient _bot, DALC.User _user)
        {
            CurrentTransaction = GetCurrentTransaction(_message, _bot, _user);
            if (CurrentTransaction == null)
                return;

            if (!IsAsked)
            {
                var fromUser = UserDalc.GetUser(CurrentTransaction.FromUserID);
                var message = string.Concat("Входящий перевод от ", fromUser.FirstName, " ", fromUser.LastName, " на ", CurrentTransaction.Amount, Environment.NewLine, "Подтвердить?");

                var keyboard = new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton("Да"),
                        new KeyboardButton("Нет"),
                    },
                });

                _bot.SendTextMessageAsync(_message.Chat.Id, message, replyMarkup: keyboard);
                IsAsked = true;
                _user.DialogContext = this.SerializeToJson();
                UserDalc.CreateOrUpdateUser(_user);
                return;
            }
            else
            {
                var yes = _message.Text == "Да";
                var no = _message.Text == "Нет";


                try
                {
                    if (!yes && !no)
                        throw new Exception("Unknown message");

                    TransactionDalc.SetTransactionState(CurrentTransaction.Id, yes ? TransactionState.Confirmed : TransactionState.Aborted);
                    var result = _bot.SendTextMessageAsync(_message.Chat.Id, "Посмотрим, есть ли что-нибудь ещё..", replyMarkup: new ReplyKeyboardRemove());
                    while (!(result.IsCanceled || result.IsFaulted || result.IsCompleted)) { Thread.Sleep(10); }

                    ProcessMessage(_message, _bot, _user);
                }
                catch (Exception ex)
                {
                    Logger.Log.Error("Беда", ex);
                    IsAsked = false;
                    CurrentTransaction = null;
                    _user.DialogContext = this.SerializeToJson();
                    UserDalc.CreateOrUpdateUser(_user);
                    return;
                }
            }
        }

        private Transaction GetCurrentTransaction(Message _message, ITelegramBotClient _bot, DALC.User _user)
        {
            var result = TransactionDalc.GetTransactionCurrent(_user.Id.Value);
            if (result == null)
            {
                _bot.SendTextMessageAsync(_message.Chat.Id, "Входящих транзакций нет:)", replyMarkup: new ReplyKeyboardRemove());

                _user.DialogContext = new RootDialog().SerializeToJson();
                UserDalc.CreateOrUpdateUser(_user);
            }

            return result;
        }
    }
}
