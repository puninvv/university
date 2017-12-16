using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.Dialogs.Commands
{
    class ToRootDialogCommand : ICommand
    {
        public string Command => "отмена";
        public string Info => "возвращение к самому началу";
        public IUserDialog SwitchTo => new RootDialog();
    }
}
