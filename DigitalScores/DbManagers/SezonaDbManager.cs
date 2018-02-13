using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DigitalScores.MasterEntities;
using System.Data.SqlClient;
using DigitalScores.Models;
using System.Data;

namespace DigitalScores.DbManagers
{
    public class SezonaDbManager : DbManagerABS
    {
        static SezonaDbManager instance;

        public static SezonaDbManager Current {

            get
            {
                if (instance==null)
                {
                    instance = new SezonaDbManager();
                }
                return instance;
            }
        }
        public SezonaDbManager():base()
        {

        }
        public SezonaDbManager(string connectionString):base(connectionString)
        {

        }
        public override void DeleteRange(List<object> list)
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
            string sql = "select * from Lige where id = @id";
            Sezona result = null;
            using (connection = new SqlConnection())
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.Int });
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        result = new Sezona(id)
                        {
                            Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                            Liga = (Liga)LigaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Liga_Id")))
                        };
                    }
                }

            }

            return result;
        }

        public override void Insert(object carrier)
        {
            throw new NotImplementedException();
        }

        public override void Update(object carrier)
        {
            throw new NotImplementedException();
        }
    }
}