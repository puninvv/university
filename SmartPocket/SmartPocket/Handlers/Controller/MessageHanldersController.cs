using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartPocket.Handlers.Controller
{
    class MessageHanldersController : IMessageHandlersController
    {
        private Dictionary<string, IMessageHandler> m_handlers = new Dictionary<string, IMessageHandler>();

        public void ProcessMessage(Message _message, ITelegramBotClient _bot)
        {
            var command = string.Join(string.Empty, _message.Text.TakeWhile(c => c != ' '));

            if (m_handlers.ContainsKey(command))
                m_handlers[command].ProcessMessage(_message, _bot);
            else
            {
                _bot.SendTextMessageAsync(_message.Chat.Id, "Нихренашечки не понял");
            }
        }

        public void Register(IMessageHandler _handler)
        {
            foreach (var command in _handler.SupportedCommands)
            {
                if (m_handlers.ContainsKey(command))
                    throw new InvalidOperationException(nameof(Register));

                m_handlers.Add(command, _handler);
            }
        }

        public void Unregister(IMessageHandler _handler)
        {
            foreach (var command in _handler.SupportedCommands)
                if (m_handlers.ContainsKey(command))
                    m_handlers.Remove(command);
        }
    }
}
