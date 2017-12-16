using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.DALC
{
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

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
