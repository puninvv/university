using SmartPocket.DALC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace SmartPocket.Dialogs
{
    class DialogsController
    {
        private readonly string m_defaultDialogContext = new RootDialog().SerializeToJson();
        private readonly string m_defaultDialogGreeting = new RootDialog().Greeting;

        public void ProcessMessage(Message _message, ITelegramBotClient _bot)
        {
            DALC.User currentUser = null;

            try
            {
                currentUser = GetUserFrom(_message);

                if (currentUser == null)
                {
                    _bot.SendTextMessageAsync(_message.Chat.Id, $"Напишите {Properties.Settings.Default.AdminTelegramUserName} чтобы он добавил вас в систему");
                    return;
                }


                currentUser.TelegramChatId = _message.Chat.Id;
                UserDalc.CreateOrUpdateUser(currentUser);

                if (_message.Text.ToLowerInvariant().StartsWith("назад"))
                {
                    _bot.SendTextMessageAsync(_message.Chat.Id, "Ну ок");
                    currentUser.DialogContext = m_defaultDialogContext;
                    UserDalc.CreateOrUpdateUser(currentUser);
                }

                var dialog = IUserDialogExtensions.DeserializeObject(currentUser.DialogContext);
                if (dialog == null)
                    throw new Exception($"Cannot deserialize {nameof(dialog)} :(");

                dialog.ProcessMessage(_message, _bot, currentUser);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Some exception occured: ", ex);
                _bot.SendTextMessageAsync(_message.Chat.Id, $"Произошла ошибка. Сорян.");
            }
        }

        private DALC.User GetUserFrom(Message _message)
        {
            var user = UserDalc.GetUser(_message.Chat.Username);
            if (user == null)
                user = UserDalc.GetUser(_message.Contact.UserId);

            return user;
        }
    }
}
