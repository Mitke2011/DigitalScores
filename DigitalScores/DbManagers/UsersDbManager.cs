using System;
using System.Collections.Generic;
using DigitalScores.MasterEntities;
using DigitalScores.Models;
using System.Data.SqlClient;

namespace DigitalScores.DbManagers
{
    public class UsersDbManager : DbManagerABS
    {
        static UsersDbManager instance;
        public static UsersDbManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new UsersDbManager();
                }
                return instance;
            }
        }
        public UsersDbManager() : base()
        {

        }

        public UsersDbManager(string connectionString) : base(connectionString)
        {

        }

        public override void DeleteRange(List<object> collection)
        {
            throw new NotImplementedException();
        }

        public override void DeleteSingle(object carrier)
        {
            throw new NotImplementedException();
        }

        public override List<object> GetAll()
        {
            throw new NotImplementedException();
        }

        public override object GetSingle(int id)
        {
            Users u = null;
            string sql = "select * from users where id = @id";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = id });
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            u = new Users(id)
                            {
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                Ime = reader.GetString(reader.GetOrdinal("ime")),
                                Prezime = reader.GetString(reader.GetOrdinal("prezime")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Grad = reader.GetString(reader.GetOrdinal("grad")),
                                Region = reader.GetString(reader.GetOrdinal("region")),
                                Privilege = GetPrivilege(reader.GetInt32(reader.GetOrdinal("Privilege_Id"))),
                                Telefon = reader.GetString(reader.GetOrdinal("telefon"))
                            };
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }

            return u;
        }

        private Privilege GetPrivilege(int v)
        {
            switch (v)
            {
                case 0:
                    return Privilege.SuperAdmin;
                case 1:
                    return Privilege.Admin;
                case 2:
                    return Privilege.Delegate;
                default:
                    break;
            }
            return Privilege.Invalid;
        }

        public override void Update(object carrier)
        {
            throw new NotImplementedException();
        }

        public Users GetUserByUsername(string username)
        {
            Users user = null;
            string sql = "select * from users where username = @username";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@username", SqlDbType = System.Data.SqlDbType.NVarChar, Value = username });

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            user = new Users(username)

                            {
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                Privilege = GetPrivilege(reader.GetInt32(reader.GetOrdinal("privilege_id"))),
                                Ime = reader.GetString(reader.GetOrdinal("ime")),
                                Prezime = reader.GetString(reader.GetOrdinal("prezime")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Grad = reader.GetString(reader.GetOrdinal("grad")),
                                Telefon = reader.GetString(reader.GetOrdinal("telefon")),
                                Region = reader.GetString(reader.GetOrdinal("region"))
                            };
                        }
                    }

                    catch (Exception)
                    {
                        throw;
                    }
                }
            }

            return user;
        }

        public Users VerifyUserByPassword(string username, string password)
        {
            Users u = GetUserByUsername(username);
            if (u != null && string.Equals(u.Password, password))
            {
                return u;
            }
            return null;
        }

        public bool CheckIfUserExists(Users user)
        {
            bool result = false;
            string sql = "select COUNT([Username]) from users where username = @username and password = @password and privilege_id = @privilege_id and ime = @ime and prezime = @prezime and email = @email and grad = @grad and telefon = @telefon and region = @region";
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql,connection))
                {
                    command.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "username", Value = user.Username, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "password", Value = user.Password, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "privilege_id", Value = user.Privilege, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "ime", Value = user.Ime, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "prezime", Value = user.Prezime, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "email", Value = user.Email, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "grad", Value = user.Grad, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "telefon", Value = user.Telefon, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "region", Value = user.Region, SqlDbType = System.Data.SqlDbType.NVarChar}
                    });
                }


                try
                {
                    int counter = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        counter = reader.GetInt32(0);
                    }
                    if (counter > 0)
                    {
                        result = true;
                        return result;
                    }
                }
                catch (Exception se)
                {

                    throw se;
                }
                return result;
            }
        }

        public override void Insert(object user)
        {
            Users u = user as Users;
            int userPrivilege = (int)u.Privilege;

            string sql = "insert into users (username, password, privilege_id, ime, prezime, email, grad, telefon, region) values (@username, @password, @privilege_id, @ime, @prezime, @email, @grad, @telefon, @region)";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "username", Value = u.Username, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "password", Value = u.Password, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "privilege_id", Value = userPrivilege, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "ime", Value = u.Ime, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "prezime", Value = u.Prezime, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "email", Value = u.Email, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "grad", Value = u.Grad, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "telefon", Value = u.Telefon, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "region", Value = u.Region, SqlDbType = System.Data.SqlDbType.NVarChar}
                    });

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException se)
                    {

                        throw se;
                    }
                }
            }
        }
    }
}