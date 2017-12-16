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
    }
    
}
