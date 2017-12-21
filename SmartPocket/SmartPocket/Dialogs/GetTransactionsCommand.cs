using SmartPocket.DALC;
using SmartPocket.Dialogs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.Dialogs
{
    public class GetTransactionsCommand : ICommand
    {
        public string Command { get => "переводы"; set { } }
        public string Info { get => "список всех ваших переводов"; set { } }
        public DialogType SwitchTo { get => DialogType.ListTransactions; set { } }
    }
}
