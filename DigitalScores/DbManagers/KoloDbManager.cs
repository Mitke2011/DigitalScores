using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using DigitalScores.MasterEntities;
using DigitalScores.Models;
using System.Data;

namespace DigitalScores.DbManagers
{
    public class KoloDbManager : DbManagerABS
    {
        static KoloDbManager instance;
        public static KoloDbManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new KoloDbManager();
                }
                return instance;
            }
        }

        public KoloDbManager() : base()
        {

        }
        public KoloDbManager(string connectionString) : base(connectionString)
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
            string sql = "select * from Kolo where id = @id";
            Kolo result = null;
            using (connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.Int });
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        result = new Kolo(id)
                        {
                            Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                            Tekuce = reader.GetInt32(reader.GetOrdinal("Tekuce")),
                            KoloSezona = (Sezona)SezonaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("sezona_id"))),
                            KoloLiga = (Liga)LigaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Liga_id")))
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