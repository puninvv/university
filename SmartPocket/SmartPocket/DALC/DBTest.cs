using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.DALC
{
    class DBTest
    {
        private void Test()
        {
            Logger.InitLogger();

            Logger.Log.Info("Entering application.");
            try
            {
                var user1 = UserDalc.GetUser("punin");
                if (user1 != null)
                    UserDalc.DropUser(user1.Id);

                var user2 = UserDalc.GetUser("punin2");
                if (user2 != null)
                    UserDalc.DropUser(user2.Id);

                var user3 = UserDalc.GetUser("punin3");
                if (user3 != null)
                    UserDalc.DropUser(user3.Id);

                user1 = UserDalc.CreateUser("punin", "1232", "Пунин Виктор Витальевич", "Нихуяшечки", true, true);
                user2 = UserDalc.CreateUser("punin2", "1232", "Пунин Виктор Витальевич2", "Нихуяшечки", true, true);
                user2 = UserDalc.CreateUser("punin3", "1232", "Пунин Виктор Витальевич3", "Нихуяшечки", true, true);

                var trId1 = TransactionDalc.CreateTransaction(user1.Id, user2.Id, 100000);
                var trId2 = TransactionDalc.CreateTransaction(user1.Id, user3.Id, 100000);

                var transactions = TransactionDalc.GetTransactionsOfUser(user1.Id, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), true, true);
                foreach (var transaction in transactions)
                    Logger.Log.Info(transaction);

                TransactionDalc.AbortTransaction(trId1.Value);
                TransactionDalc.ConfirmTransaction(trId2.Value);

                transactions = TransactionDalc.GetTransactionsOfUser(user1.Id, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), true, true);
                foreach (var transaction in transactions)
                    Logger.Log.Info(transaction);

                Logger.Log.Info(TransactionDalc.GetTransactionById(trId1.Value));
                Logger.Log.Info(TransactionDalc.GetTransactionById(trId2.Value));
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
            }

            Logger.Log.Info("Exiting application.");
        }
    }
}
