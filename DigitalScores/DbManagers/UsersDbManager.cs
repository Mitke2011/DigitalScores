using System;
using System.Collections.Generic;
using DigitalScores.MasterEntities;
using DigitalScores.Models;
using System.Data.SqlClient;
using System.Data;

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
        private UsersDbManager() : base()
        {

        }

        private UsersDbManager(string connectionString) : base(connectionString)
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
            List<object> result = new List<object>();
            string sql = @"select * from users";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            result.Add(new Users(reader.GetInt32(reader.GetOrdinal("id")))
                            {
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                Ime = reader.GetString(reader.GetOrdinal("ime")),
                                Prezime = reader.GetString(reader.GetOrdinal("prezime")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Grad = reader.GetString(reader.GetOrdinal("grad")),
                                Region = reader.GetString(reader.GetOrdinal("region")),
                                UserPrivilege = GetPrivilege(reader.GetInt32(reader.GetOrdinal("Privilege_Id"))),
                                Telefon = reader.GetString(reader.GetOrdinal("telefon"))
                            });
                        }
                    }
                    catch (SqlException e)
                    {

                        throw e;
                    }

                }
            }

            return result;
        }

        public override object GetSingle(int id)
        {
            Users u = null;
            string sql = @"select * from users where id = @id";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = id });
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
                                UserPrivilege = GetPrivilege(reader.GetInt32(reader.GetOrdinal("Privilege_Id"))),
                                Telefon = reader.GetString(reader.GetOrdinal("telefon"))
                            };
                        }
                    }
                    catch (SqlException e)
                    {

                        throw e;
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
            Users user = carrier as Users;
            string sql = @"update users set
                            username = @username,
                            password = @password,
                            Privilege_Id = @privilege_id,
                            Ime =@ime,
                            Prezime = @prezime,
                            Email = @email,
                            Grad = @grad,
                            Telefon = @telefon,
                            Region = @region
                           WHERE id = @id";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "@Id", Value = user.Id, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "@username", Value = user.Username, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@password", Value = user.Password, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@privilege_id", Value = (int)user.UserPrivilege, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "@ime", Value = user.Ime, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@prezime", Value = user.Prezime, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@email", Value = user.Email, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@grad", Value = user.Grad, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@telefon", Value = user.Telefon, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@region", Value = user.Region, SqlDbType = SqlDbType.NVarChar}
                    });

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {

                        throw e;
                    }
                }
            }
        }

        public Users GetUserByUsername(string username)
        {
            Users user = null;
            string sql = @"select * from users where username = @username";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@username", SqlDbType = SqlDbType.NVarChar, Value = username });

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            user = new Users(username)

                            {
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                UserPrivilege = GetPrivilege(reader.GetInt32(reader.GetOrdinal("privilege_id"))),
                                Ime = reader.GetString(reader.GetOrdinal("ime")),
                                Prezime = reader.GetString(reader.GetOrdinal("prezime")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Grad = reader.GetString(reader.GetOrdinal("grad")),
                                Telefon = reader.GetString(reader.GetOrdinal("telefon")),
                                Region = reader.GetString(reader.GetOrdinal("region"))
                            };
                        }
                    }

                    catch (SqlException e)
                    {
                        throw e;
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
            string sql = @"select COUNT([Username]) from users 
                            where username = @username and 
                                  password = @password and 
                                  privilege_id = @privilege_id and 
                                  ime = @ime and 
                                  prezime = @prezime and 
                                  email = @email and 
                                  grad = @grad and 
                                  telefon = @telefon and 
                                  region = @region";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "@username", Value = user.Username, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@password", Value = user.Password, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@privilege_id", Value = (int)user.UserPrivilege, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "@ime", Value = user.Ime, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@prezime", Value = user.Prezime, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@email", Value = user.Email, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@grad", Value = user.Grad, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@telefon", Value = user.Telefon, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@region", Value = user.Region, SqlDbType = SqlDbType.NVarChar}
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
                catch (SqlException se)
                {

                    throw se;
                }
                return result;
            }
        }

        public override void Insert(object user)
        {
            Users u = user as Users;
            int userPrivilege = (int)u.UserPrivilege;

            string sql = @"insert into users (username, password, privilege_id, ime, prezime, email, grad, telefon, region) 
                                     values (@username, @password, @privilege_id, @ime, @prezime, @email, @grad, @telefon, @region)";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "@username", Value = u.Username, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@password", Value = u.Password, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@privilege_id", Value = (int)userPrivilege, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "@ime", Value = u.Ime, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@prezime", Value = u.Prezime, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@email", Value = u.Email, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@grad", Value = u.Grad, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@telefon", Value = u.Telefon, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@region", Value = u.Region, SqlDbType = SqlDbType.NVarChar}
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

        public List<Users> GetAllDelegates()
        {
            List<Users> listaDelegata = new List<Users>();
            string sql = @"select * from Users where Privilege_id = 2";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Users u = new Users(reader.GetInt32(0))
                            {
                                Ime = reader.GetString(reader.GetOrdinal("Ime")),
                                Prezime = reader.GetString(reader.GetOrdinal("Prezime")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Telefon = reader.GetString(reader.GetOrdinal("Telefon")),
                                Grad = reader.GetString(reader.GetOrdinal("Grad")),
                                UserPrivilege = Privilege.Delegate
                            };
                            listaDelegata.Add(u);
                        }
                    }
                    catch (SqlException ee)
                    {

                        throw ee;
                    }

                }
            }

            return listaDelegata;
        }
    }
}