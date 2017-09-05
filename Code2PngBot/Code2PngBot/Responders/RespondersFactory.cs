using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Code2PngBot.Responders
{
    public class RespondersFactory
    {
        public static IResponder GetResponder(Message _message)
        {
            if (_message.Type == MessageType.TextMessage && _message.Text.StartsWith("/", StringComparison.InvariantCultureIgnoreCase))
                return new ImageResponder();

            return new DefaultResponder();
        }
    }
}
