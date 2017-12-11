using log4net;
using log4net.Appender;
using System.Linq;
using log4net.Config;
using SmartPocket.DALC;
using System;
using System.Threading;

namespace SmartPocket
{
    public class Program
    {
        public static void Main(string[] _args)
        {
            Logger.Log.Info("Started");
            var bot = new Telegram.Bot.TelegramBotClient("403796151:AAF7ia5i-jbet0jEFE2DltS9w263_SqCptk");
            bot.OnMessage += Bot_OnMessage;
            bot.StartReceiving();

            Logger.Log.Info("Bot is working!");
            Console.ReadKey();

            bot.OnMessage -= Bot_OnMessage;
            bot.StopReceiving();
            Logger.Log.Info("Stopped");
        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Logger.Log.Info("New message!");
            Logger.Log.Info($"{nameof(e.Message.Chat.FirstName)}:{e.Message?.Chat?.FirstName}");
            Logger.Log.Info($"{nameof(e.Message.Chat.Id)}:{e.Message?.Chat?.Id}");
            Logger.Log.Info($"{nameof(e.Message.Chat.Username)}:{e.Message?.Chat?.Username}");
            Logger.Log.Info($"{nameof(e.Message.Contact.FirstName)}:{e.Message?.Contact?.FirstName}");
            Logger.Log.Info($"{nameof(e.Message.Contact.LastName)}:{e.Message?.Contact?.LastName}");
            Logger.Log.Info($"{nameof(e.Message.Contact.PhoneNumber)}:{e.Message?.Contact?.PhoneNumber}");
            Logger.Log.Info($"{nameof(e.Message.Contact.UserId)}:{e.Message?.Contact?.UserId}");
            Logger.Log.Info($"{nameof(e.Message.Text)}:{e.Message?.Text}");
            Logger.Log.Info($"{nameof(e.Message)}:{e.Message}");
        }
    }
}
