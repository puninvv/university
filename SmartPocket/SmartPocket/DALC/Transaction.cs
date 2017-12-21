using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.DALC
{
    public enum TransactionState : short
    {
        Created,
        Confirmed,
        Aborted
    }

    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid FromUserID { get; set; }
        public Guid ToUserId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }
        public TransactionState State { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static string ToCsv(List<Transaction> _transactions, string _delimiter, string _lineEndings)
        {
            var sb = new StringBuilder();

            foreach (var transaction in _transactions)
            {
                var userFrom = UserDalc.GetUser(transaction.FromUserID);
                var userTo = UserDalc.GetUser(transaction.ToUserId);

                sb.Append(userFrom.FirstName).Append(_delimiter).Append(userFrom.LastName).Append(_delimiter).Append(userFrom.Info).Append(_delimiter);
                sb.Append(userTo.FirstName).Append(_delimiter).Append(userTo.LastName).Append(_delimiter).Append(userTo.Info).Append(_delimiter);
                sb.Append(transaction.DateTime).Append(_delimiter);
                sb.Append(transaction.State.ToString()).Append(_delimiter);
                sb.Append(transaction.Amount).Append(_lineEndings);
            }

            return sb.ToString();
        }
    }
    
}
