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
        public static User GetUser(string _telegramUserName)
        {
            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(GetUser)}: {nameof(_telegramUserName)}={_telegramUserName}");

            User result = null;

            using (var sqlConnection = new SqlConnection(DBConfig.ConnectionString))
            {
                sqlConnection.Open();

                var cmd = sqlConnection.CreateCommand();

                cmd.CommandText = "dbo.UserGet";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TelegramUserName", _telegramUserName);

                using (var reader = cmd.ExecuteReader())
                {
                    result = ReadUserFromReader(reader);
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

        public static User GetUser(uint _telegramUserId)
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

        private static User ReadUserFromReader(SqlDataReader _reader)
        {
            if (_reader.Read())
            {
                var result = new User();

                result.Id = (Guid)_reader["Id"];
                result.FirstName = _reader["FirstName"] as string;
                result.LastName = _reader["LastName"] as string;
                result.Info = _reader["Info"] as string;
                result.Role = (UserRole)(int)_reader["Role"];
                result.TelegramChatId = (int?)_reader["TelegramChatId"];
                result.TelegramUserId = (int?)_reader["TelegramUserId"];
                result.TelegramUserName = _reader["TelegramUserName"] as string;

                return result;
            }

            return null;
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
