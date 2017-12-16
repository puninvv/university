using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.DALC
{
    public enum DialogType : int
    {
        NewUser,
        Default,
        CreateUser,
        DeleteUser,
        CreateTransaction,
        CommitTransaction,
        AbortTransaction,
        GetUserInfo,
        GetAllUsers
    }

    public enum UserRole : int
    {
        ZeroLevel,
        FirstLevel,
        SecondLevel,
        ThirdLevel
    }

    public class User
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Info { get; set; }

        public UserRole Role { get; set; }

        public string TelegramUserName { get; set; }
        public int? TelegramUserId { get; set; } 
        public int? TelegramChatId { get; set; } 
        public DialogType DialogType { get; set; }
        public string DialogContext { get; set; }

        public string ToStringMinInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine(FirstName);
            sb.AppendLine(LastName);
            sb.AppendLine(Info);
            sb.AppendLine(TelegramUserName);
            sb.AppendLine();

            return sb.ToString();
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
