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
            string sql = "select * from Sezone where id = @id";
            Sezona result = null;
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.Int });
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
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

        public List<Sezona> GetSeasonByLeague(int ligaId)
        {

            List<Sezona> listaSezona = new List<Sezona>();
            string sql = @"select * from Sezone 
                where Liga_Id = @liga_id";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@liga_id", SqlDbType = System.Data.SqlDbType.Int, Value = ligaId });
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {


                            Sezona s = new Sezona(reader.GetInt32(0))
                            {
                                Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                                //KoloUtakmice = reader.GetInt32(reader.GetOrdinal("Kolo")),
                                //KlubDomacin = reader.GetString(reader.GetOrdinal("KlubDomacin")),
                                //KlubGost = reader.GetString(reader.GetOrdinal("KlubGost")),
                            };
                            listaSezona.Add(s);
                        }
                    }
                    catch (Exception ee)
                    {

                        throw ee;
                    }

                }
            }
            return listaSezona;
        }
    }
}