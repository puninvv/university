using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.Dialogs.Commands
{
    class NewUserCommand : ICommand
    {
        public string Command => "создай пользователя";
        public string Info => "содание нового пользователя";
        public IUserDialog SwitchTo => new CreateUserDialog();
    }

    class NewTransaction : ICommand
    {
        public string Command => "отправил";
        public string Info => "содание транзакции";
        public IUserDialog SwitchTo => new CreateUserDialog();
    }
}
