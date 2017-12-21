using SmartPocket.DALC;
using SmartPocket.Dialogs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.Dialogs
{
    public class AddUserCommand : ICommand
    {
        public string Command { get => "создай пользователя"; set { } }
        public string Info { get => "создание пользователя"; set { } }
        public DialogType SwitchTo { get => DialogType.AddUser; set { } }
    }
}
