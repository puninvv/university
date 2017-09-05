using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace Code2PngBot.Responders
{
    public interface IResponder
    {
        void SendResponce(Message _msg, TelegramBotClient _bot);
    }
}
