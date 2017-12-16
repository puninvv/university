using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.DALC
{
    public static class TransactionDalc
    {
        public static List<Transaction> GetTransactionsOfUser(Guid _userId, DateTime _from, DateTime _to, bool _onlyOutput, bool _onlyConfirmed)
        {
            Logger.Log.Info($"{nameof(TransactionDalc)}.{nameof(GetTransactionsOfUser)}: {nameof(_userId)}={_userId} {nameof(_from)}={_from} {nameof(_to)}={_to} {nameof(_onlyConfirmed)}={_onlyConfirmed}");

            var result = new List<Transaction>();

            using (var sqlConnection = new SqlConnection(DBConfig.ConnectionString))
            {
                sqlConnection.Open();

                var cmd = sqlConnection.CreateCommand();

                cmd.CommandText = "dbo.UsersTransactionsGet";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", _userId);
                cmd.Parameters.AddWithValue("@TimeStart", _from);
                cmd.Parameters.AddWithValue("@TimeEnd", _to);
                cmd.Parameters.AddWithValue("@OnlyOutput", _onlyOutput);
                cmd.Parameters.AddWithValue("@OnlyConfirmed", _onlyConfirmed);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var transaction = new Transaction();
                        transaction.Id = (Guid)reader["Id"];
                        transaction.FromUserID = (Guid)reader["FromUserId"];
                        transaction.ToUserId = (Guid)reader["ToUserId"];
                        transaction.Amount = (decimal)reader["Amount"];
                        transaction.State = (TransactionState)(short)reader["State"];
                        transaction.DateTime = (DateTime)reader["DateTime"];

                        result.Add(transaction);
                    }
                };
            }

            Logger.Log.Info($"{nameof(TransactionDalc)}.{nameof(GetTransactionsOfUser)}: {nameof(result)}.{nameof(result.Count)}={result.Count}");

            return result;
        }

        public static Transaction GetTransactionById(Guid _transactionId)
        {
            Logger.Log.Info($"{nameof(TransactionDalc)}.{nameof(GetTransactionById)}: {nameof(_transactionId)}={_transactionId}");

            using (var sqlConnection = new SqlConnection(DBConfig.ConnectionString))
            {
                sqlConnection.Open();

                var cmd = sqlConnection.CreateCommand();

                cmd.CommandText = "dbo.TransactionGetById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", _transactionId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var transaction = new Transaction();

                        transaction.Id = (Guid)reader["Id"];
                        transaction.FromUserID = (Guid)reader["FromUserId"];
                        transaction.ToUserId = (Guid)reader["ToUserId"];
                        transaction.Amount = (decimal)reader["Amount"];
                        transaction.State = (TransactionState)(short)reader["Confirmed"];
                        transaction.DateTime = (DateTime)reader["DateTime"];

                        return transaction;
                    }
                };
            }

            return null;
        }

        public static Guid? CreateTransaction(Guid _idFrom, Guid _idTo, decimal _amount)
        {
            Logger.Log.Info($"{nameof(TransactionDalc)}.{nameof(CreateTransaction)}: {nameof(_idFrom)}={_idFrom} {nameof(_idTo)}={_idTo} {nameof(_amount)}={_amount}");

            Guid? result = null;

            using (var sqlConnection = new SqlConnection(DBConfig.ConnectionString))
            {
                sqlConnection.Open();

                var cmd = sqlConnection.CreateCommand();

                cmd.CommandText = "[dbo].[TransactionCreate]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdFrom", _idFrom);
                cmd.Parameters.AddWithValue("@IdTo", _idTo);
                cmd.Parameters.AddWithValue("@Amount", _amount);

                result = cmd.ExecuteScalar() as Guid?;
            }

            Logger.Log.Info($"{nameof(TransactionDalc)}.{nameof(CreateTransaction)}: {nameof(result)}={result}");
            return result;
        }

        public static void SetTransactionState(Guid _transactionId, TransactionState _state)
        {
            Logger.Log.Info($"{nameof(TransactionDalc)}.{nameof(SetTransactionState)}: {nameof(_transactionId)}={_transactionId}, {nameof(_state)}={_state}");

            using (var sqlConnection = new SqlConnection(DBConfig.ConnectionString))
            {
                sqlConnection.Open();

                var cmd = sqlConnection.CreateCommand();

                cmd.CommandText = "dbo.[UpdateTransactionState]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", _transactionId);
                cmd.Parameters.AddWithValue("@State", _state);

                cmd.ExecuteNonQuery();

            }

            Logger.Log.Info($"{nameof(TransactionDalc)}.{nameof(SetTransactionState)}: success");
        }
    }
}
