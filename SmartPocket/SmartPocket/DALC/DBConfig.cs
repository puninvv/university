using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.DALC
{
    public class DBConfig
    {
        public static string ConnectionString { get => $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}\\UsersDB.mdf"; }
    }
}
