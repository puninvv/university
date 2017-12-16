using log4net;
using log4net.Appender;
using System.Linq;
using log4net.Config;
using SmartPocket.DALC;
using System;
using System.Threading;
using SmartPocket.Handlers;
using SmartPocket.Handlers.Controller;
using Telegram.Bot;

namespace SmartPocket
{
    public class Program
    {
        private static readonly IMessageHandlersController m_handlersController = new MessageHanldersController();
        private static readonly ITelegramBotClient m_bot = new TelegramBotClient("403796151:AAF7ia5i-jbet0jEFE2DltS9w263_SqCptk");

        public static void Main(string[] _args)
        {
            Logger.Log.Info("Started");

            m_handlersController.Register(new CreateUserHandler());

            m_bot.OnMessage += Bot_OnMessage;
            m_bot.StartReceiving();
            
            Logger.Log.Info("Bot is working!");
            Console.ReadKey();

            m_bot.OnMessage -= Bot_OnMessage;
            m_bot.StopReceiving();
            Logger.Log.Info("Stopped");
        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {

            try
            {
                m_handlersController.ProcessMessage(e.Message, m_bot);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Some exception occured:", ex);
            }

            Logger.Log.Info($"{nameof(e.Message)}:{Newtonsoft.Json.JsonConvert.SerializeObject(e.Message)}");
        }
    }
}
