using System;
using System.Collections.Generic;
using DigitalScores.MasterEntities;
using System.Data.SqlClient;
using DigitalScores.Models;
using System.Data;

namespace DigitalScores.DbManagers
{
    public class KlubDbManager : DbManagerABS
    {
        static KlubDbManager instance;
        public static KlubDbManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new KlubDbManager();
                }
                return instance;
            }
        }

        private KlubDbManager() : base()
        {

        }
        private KlubDbManager(string connectionString) : base(connectionString)
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
            string sql = "select * from klub where id = @id";
            Klub result = null;
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.Int });
                    SqlDataReader reader = command.ExecuteReader();
                    
                    if (reader.HasRows)
                    {
                        result = new Klub(id)
                        {
                            Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                            Trener = reader.GetString(reader.GetOrdinal("Trener")),
                            KlubSport =(Sport) SportDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("sport_id"))),
                            LigaKlub = (Liga)LigaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("ligaId")))
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