using log4net;
using log4net.Appender;
using System.Linq;
using log4net.Config;
using SmartPocket.DALC;
using System;
using System.Threading;

namespace SmartPocket
{
    class Program
    {
        public static void Main(string[] _args)
        {
            var bot = new Telegram.Bot.TelegramBotClient("403796151:AAF7ia5i-jbet0jEFE2DltS9w263_SqCptk");
            bot.OnMessage += Bot_OnMessage;
        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Logger.Log.Warn($"nameof(Bot_OnMessage):{e.Message.Text}");
        }
    }
}
