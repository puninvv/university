using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.DALC
{
    class DBTest
    {
        public static void Test()
        {
            Logger.Log.Info("Entering application.");
            try
            {
                var user1 = UserDalc.GetUser("punin");
                if (user1 != null)
                    UserDalc.DropUser(user1.Id.Value);

                var user2 = UserDalc.GetUser("punin2");
                if (user2 != null)
                    UserDalc.DropUser(user2.Id.Value);

                var user3 = UserDalc.GetUser("punin3");
                if (user3 != null)
                    UserDalc.DropUser(user3.Id.Value);

                user1 = new User()
                {
                    FirstName = "Viktor",
                    LastName = "Punin",
                    Id = null,
                    Info = "STC c# developer",
                    TelegramUserName = "puninvv",
                    Role = UserRole.ZeroLevel,
                };

                user2 = new User()
                {
                    FirstName = "Viktor1",
                    LastName = "Punin1",
                    Id = null,
                    Info = "STC c# developer",
                    TelegramUserName = "puninvv1",
                    Role = UserRole.ZeroLevel,
                };

                user3 = new User()
                {
                    FirstName = "Viktor2",
                    LastName = "Punin2",
                    Id = null,
                    Info = "STC c# developer",
                    TelegramUserName = "puninvv2",
                    Role = UserRole.ZeroLevel,
                };

                user1 = UserDalc.CreateOrUpdateUser(user1);
                user2 = UserDalc.CreateOrUpdateUser(user2);
                user2 = UserDalc.CreateOrUpdateUser(user3);

                var trId1 = TransactionDalc.CreateTransaction(user1.Id.Value, user2.Id.Value, 100000);
                var trId2 = TransactionDalc.CreateTransaction(user1.Id.Value, user3.Id.Value, 100000);

                var transactions = TransactionDalc.GetTransactionsOfUser(user1.Id.Value, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), true, true);
                foreach (var transaction in transactions)
                    Logger.Log.Info(transaction);

                TransactionDalc.SetTransactionState(trId1.Value, TransactionState.Aborted);
                TransactionDalc.SetTransactionState(trId2.Value, TransactionState.Confirmed);

                transactions = TransactionDalc.GetTransactionsOfUser(user1.Id.Value, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), true, true);
                foreach (var transaction in transactions)
                    Logger.Log.Info(transaction);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
            }

            Logger.Log.Info("Exiting application.");
        }
    }
}
