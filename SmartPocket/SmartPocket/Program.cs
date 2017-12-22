using System;
using Telegram.Bot;
using SmartPocket.Dialogs;
using SmartPocket.DALC;

namespace SmartPocket
{
    public class Program
    {
        private static readonly DialogsController m_controller = new DialogsController();
        private static readonly ITelegramBotClient m_bot = new TelegramBotClient("403796151:AAF7ia5i-jbet0jEFE2DltS9w263_SqCptk");

        private static void CreateAdmin()
        {
            var admin = UserDalc.GetUser(Properties.Settings.Default.AdminTelegramUserName);
            if (admin == null)
            {
                admin = new User();
                admin.TelegramUserName = Properties.Settings.Default.AdminTelegramUserName;
                admin.FirstName = Properties.Settings.Default.AdminTelegramFirstName;
                admin.LastName = Properties.Settings.Default.AdminTelegramLastName;
                admin.Role = UserRole.ZeroLevel;
                admin.Info = Properties.Settings.Default.AdminTelegramInfo;

                admin = UserDalc.CreateOrUpdateUser(admin);
            }

        }

        public static void Main(string[] _args)
        {
            Logger.Log.Info("Started");

            CreateAdmin();

            m_bot.OnMessage += OnBotMessage;
            m_bot.StartReceiving();
            
            Logger.Log.Info("Bot is working!");
            Console.ReadKey();

            m_bot.OnMessage -= OnBotMessage;
            m_bot.StopReceiving();
            Logger.Log.Info("Stopped");
        }

        private static void OnBotMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            try
            {
                m_controller.ProcessMessage(e.Message, m_bot);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Some exception occured:", ex);
            }

            Logger.Log.Info($"{nameof(e.Message)}:{Newtonsoft.Json.JsonConvert.SerializeObject(e.Message)}");
        }
    }
}
