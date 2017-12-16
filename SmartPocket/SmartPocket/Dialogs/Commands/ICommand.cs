using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.Dialogs.Commands
{
    interface ICommand
    {
        string Command { get;}
        string Info { get; }

        IUserDialog SwitchTo { get; }
    }
}
