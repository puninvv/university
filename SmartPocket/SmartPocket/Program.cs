using System;
using Telegram.Bot;
using SmartPocket.Dialogs;
using SmartPocket.DALC;

namespace SmartPocket
{
    public class Program
    {
        private static readonly IUserDialog m_rootDialog = new RootDialog();
        private static readonly ITelegramBotClient m_bot = new TelegramBotClient("403796151:AAF7ia5i-jbet0jEFE2DltS9w263_SqCptk");

        private static void CreateAdmin()
        {
            var admin = UserDalc.GetUser(Properties.Settings.Default.AdminTelegramUserName);
            if (admin == null)
            {
                admin = new User();
                admin.TelegramUserName = Properties.Settings.Default.AdminTelegramUserName;
                admin.FirstName = "Виктор";
                admin.LastName = "Пунин";
                admin.Role = UserRole.ZeroLevel;
                admin.Info = "punin.v.v@gmail.com";
                admin.DialogType = DialogType.Default;

                admin = UserDalc.CreateOrUpdateUser(admin);
            }

        }

        public static void Main(string[] _args)
        {
            Logger.Log.Info("Started");

            CreateAdmin();

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
                m_rootDialog.ProcessMessage(e.Message, m_bot, null);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Some exception occured:", ex);
            }

            Logger.Log.Info($"{nameof(e.Message)}:{Newtonsoft.Json.JsonConvert.SerializeObject(e.Message)}");
        }
    }
}
