﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.Handlers
{
    interface IMessageHandler : IMessageProcessor
    {
        List<string> SupportedCommands { get; }
        string Info { get; }
    }
}
