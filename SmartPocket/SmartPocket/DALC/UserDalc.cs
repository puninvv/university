using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.DALC
{
    public static class UserDalc
    {
        public static List<User> GetUsersByInfo(string _info)
        {
            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(GetUsersByInfo)}: {nameof(_info)}={_info}");

            List<User> result = new List<User>();

            using (var sqlConnection = new SqlConnection(DBConfig.ConnectionString))
            {
                sqlConnection.Open();

                var cmd = sqlConnection.CreateCommand();

                cmd.CommandText = "[dbo].[UserGetByInfo]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Info", _info);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tmpUser = ReadUserFromReader(reader);
                        if (tmpUser != null)
                            result.Add(tmpUser);
                    }
                };
            }

            return result;
        }

        public static User GetUser(string _telegramUserName)
        {
            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(GetUser)}: {nameof(_telegramUserName)}={_telegramUserName}");

            User result = null;

            using (var sqlConnection = new SqlConnection(DBConfig.ConnectionString))
            {
                sqlConnection.Open();

                var cmd = sqlConnection.CreateCommand();

                cmd.CommandText = "dbo.[UserGetByTelegramUserName]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TelegramUserName", _telegramUserName);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = ReadUserFromReader(reader);
                    }
                };
            }

            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(GetUser)}: {nameof(result)}={result}");

            return result;
        }

        public static User GetUser(Guid _id)
        {
            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(GetUser)}: {nameof(_id)}={_id}");

            User result = null;

            using (var sqlConnection = new SqlConnection(DBConfig.ConnectionString))
            {
                sqlConnection.Open();

                var cmd = sqlConnection.CreateCommand();

                cmd.CommandText = "dbo.UserGetById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Uid", _id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = ReadUserFromReader(reader);
                    }
                };
            }

            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(GetUser)}: {nameof(result)}={result}");

            return result;
        }

        public static User GetUser(int _telegramUserId)
        {
            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(GetUser)}: {nameof(_telegramUserId)}={_telegramUserId}");

            User result = null;

            using (var sqlConnection = new SqlConnection(DBConfig.ConnectionString))
            {
                sqlConnection.Open();

                var cmd = sqlConnection.CreateCommand();

                cmd.CommandText = "dbo.UserGetByTelegramUserId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TelegramUserId", _telegramUserId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = ReadUserFromReader(reader);
                    }
                };
            }

            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(GetUser)}: {nameof(result)}={result}");

            return result;
        }

        public static List<User> GetUsers()
        {
            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(GetUsers)}");

            List<User> result = new List<User>();

            using (var sqlConnection = new SqlConnection(DBConfig.ConnectionString))
            {
                sqlConnection.Open();

                var cmd = sqlConnection.CreateCommand();

                cmd.CommandText = "[dbo].[UserList]";
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tmpUser = ReadUserFromReader(reader);
                        if (tmpUser != null)
                            result.Add(tmpUser);
                    }
                };
            }

            return result;
        }

        private static User ReadUserFromReader(SqlDataReader _reader)
        {
            var result = new User();

            result.Id = (Guid)_reader["Id"];
            result.FirstName = _reader["FirstName"] as string;
            result.LastName = _reader["LastName"] as string;
            result.Info = _reader["Info"] as string;

            var role = _reader["Role"];
            result.Role = role == DBNull.Value ? UserRole.ThirdLevel : (UserRole)(int)role;

            result.TelegramChatId = _reader["TelegramChatId"] == DBNull.Value ? null : (long?)_reader["TelegramChatId"];
            result.TelegramUserId = _reader["TelegramUserId"] == DBNull.Value ? null : (int?)_reader["TelegramUserId"]; ;
            result.TelegramUserName = _reader["TelegramUserName"] as string;
            result.DialogType = (DialogType)(int)_reader["DialogType"];
            result.DialogContext = _reader["DialogContext"] as string;

            return result;
        }


        public static User CreateOrUpdateUser(User _user)
        {
            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(CreateOrUpdateUser)}: {nameof(_user)}={_user}");

            using (var sqlConnection = new SqlConnection(DBConfig.ConnectionString))
            {
                sqlConnection.Open();

                var cmd = sqlConnection.CreateCommand();

                cmd.CommandText = "dbo.[UserCreateOrUpdate]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Uid", _user.Id);
                cmd.Parameters.AddWithValue("@FirstName", _user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", _user.LastName);
                cmd.Parameters.AddWithValue("@Info", _user.Info);
                cmd.Parameters.AddWithValue("@TelegramUserName", _user.TelegramUserName);
                cmd.Parameters.AddWithValue("@TelegramUserId", _user.TelegramUserId);
                cmd.Parameters.AddWithValue("@TelegramChatId", _user.TelegramChatId);
                cmd.Parameters.AddWithValue("@Role", _user.Role);
                cmd.Parameters.AddWithValue("@DialogType", _user.DialogType);
                cmd.Parameters.AddWithValue("@DialogContext", _user.DialogContext);


                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _user.Id = (Guid)reader[0];
                    }
                };

                Logger.Log.Info($"{nameof(UserDalc)}.{nameof(CreateOrUpdateUser)}: {nameof(_user)}={_user}");

                return _user;
            }
        }

        public static void DropUser(Guid _id)
        {
            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(DropUser)}: {nameof(_id)}={_id}");

            using (var sqlConnection = new SqlConnection(DBConfig.ConnectionString))
            {
                sqlConnection.Open();

                var cmd = sqlConnection.CreateCommand();

                cmd.CommandText = "dbo.UserDrop";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", _id);

                cmd.ExecuteScalar();
            }

            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(DropUser)}: dropped");
        }
    }
}
