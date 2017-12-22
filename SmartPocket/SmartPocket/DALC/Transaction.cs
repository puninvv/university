using GemBox.Spreadsheet;
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

        public static string SaveToFile(List<Transaction> _transactions, User _user)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            var fileName = _user.FirstName+"_"+_user.LastName+DateTime.Now.ToShortDateString() + ".xls";

            var ds = new DataSet("New_DataSet");
            var dt = new DataTable("New_DataTable");

            //Set the locale for each
            ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
            dt.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();

            foreach (var transaction in _transactions)
            {
                var userFrom = UserDalc.GetUser(transaction.FromUserID);
                var userTo = UserDalc.GetUser(transaction.ToUserId);

                dt.Rows.Add(userFrom.FirstName, userFrom.LastName, userFrom.Info, userTo.FirstName, userTo.LastName, userTo.Info, transaction.DateTime, transaction.Amount, transaction.State);
            }

            ds.Tables.Add(dt);

            var workbook = new ExcelFile();
            var ws = workbook.Worksheets.Add("wsh1");
            ws.InsertDataTable(dt);
            workbook.Save(fileName);
            return fileName;
        }
    }
    
}
