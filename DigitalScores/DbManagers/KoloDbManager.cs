using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DigitalScores.MasterEntities;
using DigitalScores.Models;
using System.Data;
using DigitalScores.DbManagers;

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
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.Int });
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        result = new Kolo(id)
                        {
                           
                           Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                           Tekuce = reader.GetInt32(reader.GetOrdinal("Tekuce")),
                           KoloSezona = (Sezona)SezonaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Sezona_id"))),
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

        public override void Insert(object kolo)
        {
            Kolo k = kolo as Kolo;
            string sql = "insert into Kolo (Naziv, Sezona_Id, Liga_Id, Tekuce) values (@naziv, @sezona_id, @liga_id, 0)";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(
                new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "@naziv", Value = k.Naziv, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@sezona_id", Value = k.sezonaId, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@liga_id", Value = k.ligaId, SqlDbType =SqlDbType.NVarChar },
                  
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

        public List<Kolo> GetRoundByLeague(int ligaId) {

            List<Kolo> listaKola = new List<Kolo>();
            string sql = @"select * from Kolo 
                where Liga_Id = @liga_id";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@liga_id", SqlDbType = SqlDbType.Int, Value = ligaId });
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {


                            Kolo k = new Kolo(reader.GetInt32(0))
                            {
                                Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                                Tekuce = reader.GetInt32(reader.GetOrdinal("Tekuce")),
                                //KoloUtakmice = reader.GetInt32(reader.GetOrdinal("Kolo")),
                                //KlubDomacin = reader.GetString(reader.GetOrdinal("KlubDomacin")),
                                //KlubGost = reader.GetString(reader.GetOrdinal("KlubGost")),
                            };
                            listaKola.Add(k);
                        }
                    }
                    catch (Exception ee)
                    {

                        throw ee;
                    }

                }
            }
            return listaKola;
        }

        public List<Kolo> GetRounds()
        {
            List<DigitalScores.Models.Kolo> listaKola = new List<DigitalScores.Models.Kolo>();
            string sql = @"select * from Kolo";

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


                            Kolo k = new Kolo(reader.GetInt32(0))
                            {
                                Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                                KoloSezona = (Sezona)SezonaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal ("Sezona_Id"))),
                                KoloLiga = (Liga)LigaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Liga_Id"))),
                                Tekuce = reader.GetInt32(reader.GetOrdinal("Tekuce"))

                            };
                            listaKola.Add(k);
                        }
                    }
                    catch (Exception ee)
                    {

                        throw ee;
                    }

                }
            }

            return listaKola;
        }

        public void SetActiveRound(int koloId)
        {
            string sql = @"update Kolo set tekuce = 1
                            where id = @koloId";

            string sqlResetOthers = @"update Kolo set tekuce = 0
                                      where id !=@koloId";
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@koloId", Value = koloId, DbType = DbType.Int32 });

                    try
                    {
                        command.ExecuteNonQuery();

                        command.CommandText = sqlResetOthers;
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