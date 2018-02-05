using System;
using System.Collections.Generic;
using DigitalScores.MasterEntities;
using DigitalScores.Models;
using System.Data.SqlClient;

namespace DigitalScores.DbManagers
{
    public class UsersDbManager : DbManagerABS
    {
        public UsersDbManager() : base()
        {

        }

        public UsersDbManager(string connectionString) : base(connectionString)
        {

        }

        public override void DeleteRange()
        {
            throw new NotImplementedException();
        }

        public override void DeleteSingle()
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

        public override void Insert()
        {
            throw new NotImplementedException();
        }

        public override void Update(object carrier)
        {
            throw new NotImplementedException();
        }

        public Users GetUserByUsername(string username)
        {
            string sql = "select * from users where username = @username";
            return null;
        }
    }
}