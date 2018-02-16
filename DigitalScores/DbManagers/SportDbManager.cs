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
            throw new NotImplementedException();
        }

        public override void DeleteRange(List<object> list)
        {
            throw new NotImplementedException();
        }

        public override List<object> GetAll()
        {
            throw new NotImplementedException();
        }

        public override object GetSingle(int id)
        {
            string sql = "select * from Sport where id = @id";
            Sport result = null;
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.Int });
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
            throw new NotImplementedException();
        }

        public override void Insert(object carrier)
        {
            throw new NotImplementedException();
        }
    }
}