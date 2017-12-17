using SmartPocket.DALC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.Dialogs.Commands
{
    public interface ICommand
    {
        string Command { get; set; }
        string Info { get; set; }
        DialogType SwitchTo { get; set; }
    }
}
