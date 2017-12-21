using SmartPocket.DALC;
using SmartPocket.Dialogs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.Dialogs
{
    class DialogsFactory
    {
        public static IUserDialog CreateFrom(DialogType _type)
        {
            switch (_type)
            {
                case DialogType.GetUsers:
                    return new GetAllUsersDialog();
                case DialogType.AddUser:
                    return new AddUserDialog();
                case DialogType.SendTransaction:
                    return new SendTransactionDialog();
                case DialogType.ListTransactions:
                    return new GetTransactionsDialog();
                default:
                    return new RootDialog();
            }
        }
    }
}
