using Code2PngBot.Responders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Code2PngBot
{
    public class BotWrapper : IDisposable
    {
        private TelegramBotClient m_bot;

        public BotWrapper()
        {
            m_bot = new TelegramBotClient("438407447:AAG1O3d_8B6IeyAn78PMU3MmhcO3tWm7FbI");

            m_bot.OnMessage += OnMessage;
            m_bot.OnMessageEdited += OnMessage;
            m_bot.OnUpdate += OnUpdate;

            m_bot.StartReceiving();
        }

        private void OnUpdate(object sender, Telegram.Bot.Args.UpdateEventArgs e)
        {
            Logger.Instance.Write($"Type={e.Update.Type}, From={e.Update.Message?.Chat?.FirstName ?? "Undefined"}, LastName={e.Update.Message?.Chat?.LastName ?? "Undefined"}, UserName={e.Update.Message?.Chat?.Username ?? "Undefined"}, MesageText = {e.Update.Message?.Text}, EditedMessage = {e.Update.EditedMessage?.Text}");
        }

        private void OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message == null)
                return;

            IResponder responder = RespondersFactory.GetResponder(e.Message);
            responder.SendResponce(e.Message, m_bot);
        }

        public void Dispose()
        {
            m_bot.OnMessage -= OnMessage;
            m_bot.OnMessageEdited -= OnMessage;

            m_bot.OnUpdate -= OnUpdate;

            m_bot.StopReceiving();

            m_bot = null;
        }
    }
}
