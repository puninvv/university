using SmartPocket.DALC;
using SmartPocket.Dialogs.Commands;
using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;
using Newtonsoft.Json;
using SmartPocket.Serialization;
using System.Text;

namespace SmartPocket.Dialogs
{
    public interface IUserDialog
    {
        string Greeting { get; set; }

        List<ICommand> SupportedCommands { get; set; }

        void ProcessMessage(Message _message, ITelegramBotClient _bot, DALC.User _user);
    }
}
