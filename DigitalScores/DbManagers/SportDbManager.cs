using System;
using System.Collections.Generic;
using DigitalScores.MasterEntities;
using System.Data.SqlClient;
using DigitalScores.Models;
using System.Data;


namespace DigitalScores.DbManagers
{
    public class SportDbManager : DbManagerABS
    {
        static SportDbManager instance;

        public static SportDbManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new SportDbManager();
                }
                return instance;
            }
        }
        public SportDbManager() : base()
        {

        }
        public SportDbManager(string connectionString) : base(connectionString)
        {

        }

        public override void DeleteSingle(object carrier)
        {
            string sql = @"delete from Sport where id = @id";
            Sport sp = (Sport)carrier;
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = sp.Id, DbType = DbType.Int32 });
                    command.ExecuteNonQuery();
                }
            }
        }

        public override void DeleteRange(List<object> list)
        {
            string idCollection = "";
            foreach (var item in list)
            {
                idCollection = (item as Sport).Id.ToString() + ",";
            }

            idCollection = idCollection.Remove(idCollection.LastIndexOf(','));
            string sql = @"delete from Sport where id = @ids";
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@ids", Value = idCollection, DbType = DbType.Int32 });
                    command.ExecuteNonQuery();
                }
            }
        }

        public override List<object> GetAll()
        {
            string sql = @"select * from Sport";
            List<object> result = new List<object>();
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Sport(reader.GetInt32(reader.GetOrdinal("id")))
                        {
                            Naziv = reader.GetString(reader.GetOrdinal("Naziv"))
                        });
                    }
                }

            }

            return result;
        }

        public override object GetSingle(int id)
        {
            string sql = @"select * from Sport where id = @id";
            Sport result = null;
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = id, DbType = DbType.Int32 });
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        result = new Sport(id)
                        {
                            Naziv = reader.GetString(reader.GetOrdinal("Naziv"))
                        };
                    }
                }

            }

            return result;
        }

        public override void Update(object carrier)
        {
            string sql = @"update Sport set Naziv = @Naziv where id = @id";
            Sport sp = (Sport)carrier;
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[] {
                        new SqlParameter() { ParameterName = "@id", Value = sp.Id, DbType = DbType.Int32 },
                        new SqlParameter() { ParameterName = "@Naziv", Value = sp.Naziv, DbType = DbType.String }
                    });
                    command.ExecuteNonQuery();
                }
            }
        }

        public override void Insert(object carrier)
        {
            string sql = @"insert into Sport (Naziv) values (@Naziv)";
            Sport sp = (Sport)carrier;
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@Naziv", Value = sp.Naziv, DbType = DbType.String });
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}