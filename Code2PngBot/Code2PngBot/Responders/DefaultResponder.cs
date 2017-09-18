using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Code2PngBot.Responders
{
    public class DefaultResponder : IResponder
    {
        public async void SendResponce(Message _msg, TelegramBotClient _bot)
        {
            await _bot.SendTextMessageAsync(_msg.Chat.Id, "I'm a bot, who can make a .png from your code.\nPlease, write /language and then - your code.\nSupported language: \n/cs\n/cpp\n/java\n/sql\n/xml\n");
        }
    }
}
