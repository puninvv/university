using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartPocket.Handlers
{
    interface IMessageProcessor
    {
        bool ProcessMessage(Message _message, ITelegramBotClient _bot, DALC.User _user);
    }
}
