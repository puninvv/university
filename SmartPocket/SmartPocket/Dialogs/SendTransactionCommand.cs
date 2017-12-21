using SmartPocket.DALC;
using SmartPocket.Dialogs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.Dialogs
{
    public class SendTransactionCommand : ICommand
    {
        public string Command { get => "отправь"; set { } }
        public string Info { get => "отправить кому-то денег"; set { } }
        public DialogType SwitchTo { get => DialogType.SendTransaction; set { } }
    }
}
