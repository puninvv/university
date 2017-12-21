using SmartPocket.DALC;
using SmartPocket.Dialogs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.Dialogs
{
    public class GetAllUsersCommand : ICommand
    {
        public string Command { get => "пользователи"; set { } }

        public string Info { get => "отобразить список пользователей"; set { } }

        public DialogType SwitchTo { get => DialogType.GetUsers; set { } }
    }
}
