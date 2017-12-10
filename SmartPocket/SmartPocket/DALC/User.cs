using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.DALC
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Info { get; set; }
        public bool IsAdmin { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("(").Append(nameof(Id)).Append(":").Append(Id).Append(")");
            sb.Append("(").Append(nameof(FullName)).Append(":").Append(FullName).Append(")");
            sb.Append("(").Append(nameof(Info)).Append(":").Append(Info).Append(")");
            sb.Append("(").Append(nameof(IsAdmin)).Append(":").Append(IsAdmin).Append(")");
            sb.Append("(").Append(nameof(Login)).Append(":").Append(Login).Append(")");
            sb.Append("(").Append(nameof(Password)).Append(":").Append(Password).Append(")");
            sb.Append("(").Append(nameof(IsActive)).Append(":").Append(IsActive).Append(")");

            return sb.ToString();
        }
    }

    public static class UserDalc
    {
        public static Guid? AuthorizeUser(string _login, string _password)
        {
            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(AuthorizeUser)}: checking {nameof(_login)}={_login} {nameof(_password)}={_password}");

            using (var sqlConnection = new SqlConnection(DBConfig.ConnectionString))
            {
                sqlConnection.Open();
                var cmd = sqlConnection.CreateCommand();

                cmd.CommandText = "dbo.UserAuthorize";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Login", _login);
                cmd.Parameters.AddWithValue("@Password", _password);

                Guid? result = null;

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = reader["Id"] as Guid?;
                    }
                }

                Logger.Log.Info($"{nameof(UserDalc)}.{nameof(AuthorizeUser)}: {nameof(result)}={result}");

                return result;
            }

        }

        public static User GetUser(string _login)
        {
            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(GetUser)}: {nameof(_login)}={_login}");

            User result = null;

            using (var sqlConnection = new SqlConnection(DBConfig.ConnectionString))
            {
                sqlConnection.Open();

                var cmd = sqlConnection.CreateCommand();

                cmd.CommandText = "dbo.UserGet";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Login", _login);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new User();
                        result.Login = reader["Login"] as string;
                        result.IsActive = (bool)reader["IsActive"];
                        result.FullName = reader["FullName"] as string;
                        result.Info = reader["Info"] as string;
                        result.IsAdmin = (bool)reader["IsAdmin"];
                        result.Id = (Guid)reader["Id"];
                        result.Password = reader["Password"] as string;
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

                cmd.CommandText = "dbo.UserGet";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", _id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new User();
                        result.Login = reader["Login"] as string;
                        result.IsActive = (bool)reader["IsActive"];
                        result.FullName = reader["FullName"] as string;
                        result.Info = reader["Info"] as string;
                        result.IsAdmin = (bool)reader["IsAdmin"];
                        result.Id = (Guid)reader["Id"];
                        result.Password = reader["Password"] as string;
                    }
                };
            }

            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(GetUser)}: {nameof(result)}={result}");

            return result;
        }



        public static User CreateUser(string _login, string _pass, string _fullName, string _info, bool _isAdmin, bool _isActive)
        {
            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(CreateUser)}: {nameof(_login)}={_login} {nameof(_pass)}={_pass} {nameof(_fullName)}={_fullName} {nameof(_info)}={_info} {nameof(_isActive)}={_isActive} {nameof(_isAdmin)}={_isAdmin}");

            User result = null;

            using (var sqlConnection = new SqlConnection(DBConfig.ConnectionString))
            {
                sqlConnection.Open();

                var cmd = sqlConnection.CreateCommand();

                cmd.CommandText = "dbo.UserCreate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Login", _login);
                cmd.Parameters.AddWithValue("@Password", _pass);
                cmd.Parameters.AddWithValue("@FullName", _fullName);
                cmd.Parameters.AddWithValue("@Info", _info);
                cmd.Parameters.AddWithValue("@IsAdmin", _isAdmin);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new User();
                        result.Id = (Guid)reader["Id"];
                        result.Login = _login;
                        result.Password = _pass;
                        result.FullName = _fullName;
                        result.IsActive = _isActive;
                        result.Info = _info;
                        result.IsAdmin = _isAdmin;
                    }
                };
            }

            Logger.Log.Info($"{nameof(UserDalc)}.{nameof(CreateUser)}: {nameof(result)}={result}");

            return result;
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
