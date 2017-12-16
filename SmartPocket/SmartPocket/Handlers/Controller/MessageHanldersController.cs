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
            var command = string.Join(string.Empty, _message.Text.TakeWhile(c => c != ' ')).ToLowerInvariant();

            try
            {
                if (m_handlers.ContainsKey(command))
                    m_handlers[command].ProcessMessage(_message, _bot);
                else
                {
                    var supportedCommands = new StringBuilder();
                    supportedCommands.AppendLine("Не понял команды. Поддерживаемые:");

                    foreach (var item in m_handlers)
                        supportedCommands.Append(item.Key).Append(" - ").Append(item.Value.Info).AppendLine();

                    _bot.SendTextMessageAsync(_message.Chat.Id, supportedCommands.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error($"{nameof(MessageHanldersController)}.{nameof(ProcessMessage)}:", ex);
                _bot.SendTextMessageAsync(_message.Chat.Id, "Ошибочка вышла, попробуйте ещё раз. Или напишите на почту разработчика punin.v.v@gmail.com");
            }
        }

        public void Register(IMessageHandler _handler)
        {
            foreach (var command in _handler.SupportedCommands)
            {
                var lowerCommand = command.ToLowerInvariant();

                if (m_handlers.ContainsKey(lowerCommand))
                    throw new InvalidOperationException(nameof(Register));

                m_handlers.Add(lowerCommand, _handler);
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
