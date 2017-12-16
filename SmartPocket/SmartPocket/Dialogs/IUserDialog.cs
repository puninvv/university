using SmartPocket.DALC;
using SmartPocket.Dialogs.Commands;
using SmartPocket.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.Dialogs
{
    interface IUserDialog : IMessageHandler
    {
        DialogType DialogType { get; }
        string Greeting { get; }
    }
}
