using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartPocket.Handlers
{
    class CreateUserHandler : IMessageHandler
    {
        public List<string> SupportedCommands => new List<string>() { "создай" };

        public string Info => "Создать юзера";

        public void ProcessMessage(Message _message, ITelegramBotClient _bot)
        {
            throw new NotImplementedException();
        }
    }
}
