using SmartPocket.DALC;
using SmartPocket.Dialogs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.Dialogs
{
    public class ReceiveTransactionCommand : ICommand
    {
        public string Command { get => "получил"; set { } }
        public string Info { get => "создать входящий извне перевод"; set { } }
        public DialogType SwitchTo { get => DialogType.ReceiveTransaction; set { } }
    }
}
