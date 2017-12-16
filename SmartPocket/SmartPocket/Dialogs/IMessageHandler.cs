using SmartPocket.Dialogs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.Handlers
{
    interface IMessageHandler : IMessageProcessor
    {
        List<ICommand> SupportedCommands { get; }
    }
}
