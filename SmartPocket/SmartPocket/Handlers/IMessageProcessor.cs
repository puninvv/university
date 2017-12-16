using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace SmartPocket.Handlers
{
    interface IMessageProcessor
    {
        void ProcessMessage(Telegram.Bot.Types.Message _message, ITelegramBotClient _bot);
    }
}
