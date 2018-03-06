using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DigitalScores.MasterEntities;
using DigitalScores.Models;

namespace DigitalScores.DbManagers
{
    public class UtakmicaDbManager : DbManagerABS
    {

        static UtakmicaDbManager instance;
        public static UtakmicaDbManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new UtakmicaDbManager();
                }
                return instance;
            }
        }
        public UtakmicaDbManager() : base()
        {

        }

        public UtakmicaDbManager(string connectionString) : base(connectionString)
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

        /* public  List<DigitalScores.Models.Utakmice> GetGames()
         {
             List<DigitalScores.Models.Utakmice> listaUtakmica = new List<DigitalScores.Models.Utakmice>();
             string sql = "select kolo.Id as Kolo, u.Id as Id, kd.Naziv as KlubDomacin, kg.Naziv as KlubGost from Utakmice u join Klub kd on (u.Klub_Domacin_Id = kd.Id)" + 
                 "join Klub kg on (u.Klub_Gost_Id = kg.Id)" +
                 "join Kolo kolo on (u.Kolo_Id = kolo.Id)"+
                 "where kolo.Tekuce = 1";

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
                             KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("")))
                             Utakmice u = new Utakmice()
                             {

                                 KoloUtakmice = reader.GetInt32(reader.GetOrdinal("Kolo")),
                                 KlubDomacin= reader.GetString(reader.GetOrdinal("KlubDomacin")),
                                 KlubGost = reader.GetString(reader.GetOrdinal("KlubGost")),
                             };
                             listaUtakmica.Add(u);
                         }
                     }
                     catch (Exception ee)
                     {

                         throw ee;
                     }

                 }
             }

             return listaUtakmica;
         }*/

        public override object GetSingle(int id)
        {
            Utakmice result = null;
            string sql = "select * from utakmice where id = @id";

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
                            result = new Utakmice(id)
                            {
                                KlubDomacin = (Klub)KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_domacin_id"))),
                                KlubGost = (Klub)KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_gost_id"))),
                                KoloUtakmice = (Kolo)KoloDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Kolo_Id"))),
                                LigaUtakmice = (Liga)LigaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Liga_Id"))),
                                Sezona = (Sezona)SezonaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Sezona_Id")))
                            };
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }

            return result;
        }

        public override void Insert(object utakmica)
        {
            Utakmice u = utakmica as Utakmice;
            string sql = "insert into Utakmice (Kolo_Id, Klub_Domacin_Id, Klub_Gost_Id, Sudija1_Id, Sudija2_Id, Delegat_Id, Liga_Id, Hala_Id, Sezona_Id, Napomena_Delegata) values (@kolo_id, @klDomacin_id, @klGost_Id, @sudija1_Id, @sudija2_Id, @delegat_Id, @liga_Id, @hala_Id, @sezona_Id, @napomena_delegata)";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(
                new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "@kolo_id", Value = u.koloID, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "@klDomacin_id", Value = u.klubDomacinId, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "@klGost_Id", Value = u.klubGostId, SqlDbType =System.Data.SqlDbType.Int },
                    new SqlParameter(){ ParameterName = "@sudija1_Id", Value = u.sudija1Id, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "@sudija2_Id", Value = u.sudija2Id, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "@delegat_Id", Value = u.delegatId, SqlDbType =System.Data.SqlDbType.Int },
                    new SqlParameter(){ ParameterName = "@liga_Id", Value =  u.ligaId, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "@hala_Id", Value = u.halaId, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "@sezona_Id", Value = u.sezonaId, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "@napomena_delegata", Value = u.NapomenaDelegata, SqlDbType = System.Data.SqlDbType.NVarChar}
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

        public override void Update(object carrier)
        {
            throw new NotImplementedException();
        }

        public override List<object> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Utakmice> GetGames()
        {
            List<Utakmice> listaUtakmica = new List<Utakmice>();
            string sql = @"select * from Utakmice u
                join Kolo kolo on (u.Kolo_Id = kolo.Id)
                where kolo.Tekuce = 1";

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
                            KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_domacin_id")));

                            Utakmice u = new Utakmice(reader.GetInt32(0))
                            {
                                KlubDomacin = (Klub)KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_domacin_id"))),
                                KlubGost = (Klub)KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_gost_id"))),
                                KoloUtakmice = (Kolo)KoloDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Kolo_Id")))
                                //KoloUtakmice = reader.GetInt32(reader.GetOrdinal("Kolo")),
                                //KlubDomacin = reader.GetString(reader.GetOrdinal("KlubDomacin")),
                                //KlubGost = reader.GetString(reader.GetOrdinal("KlubGost")),
                            };
                            listaUtakmica.Add(u);
                        }
                    }
                    catch (Exception ee)
                    {

                        throw ee;
                    }

                }
            }

            return listaUtakmica;
        }

        public List<Utakmice> GetGamesByLeagueDelegate(int ligaId, int delegateId)
        {
            List<Utakmice> listaUtakmica = new List<Utakmice>();
            string sql = @"select * from Utakmice u
                join Kolo kolo on (u.Kolo_Id = kolo.Id)
                where u.Liga_Id = @liga_id and Delegat_Id = @delegateId
                and kolo.Tekuce = 1";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter() { ParameterName = "@liga_id", SqlDbType = System.Data.SqlDbType.Int, Value = ligaId },
                        new SqlParameter() { ParameterName = "@delegateId", SqlDbType = System.Data.SqlDbType.Int, Value = delegateId }
                    });
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_domacin_id")));

                            Utakmice u = new Utakmice(reader.GetInt32(0))
                            {
                                KlubDomacin = (Klub)KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_domacin_id"))),
                                KlubGost = (Klub)KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_gost_id"))),
                                KoloUtakmice = (Kolo)KoloDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Kolo_Id"))),
                                DelegatUtakmice = (Users)UsersDbManager.Current.GetSingle(delegateId),
                                LigaUtakmice = (Liga)LigaDbManager.Current.GetSingle(ligaId)
                            };
                            listaUtakmica.Add(u);
                        }
                    }
                    catch (Exception ee)
                    {

                        throw ee;
                    }

                }
            }
            return listaUtakmica;
        }


        public List<Utakmice> GetGamesByLeagueAdmin(int ligaId)
        {
            List<Utakmice> listaUtakmica = new List<Utakmice>();
            string sql = @"select * from Utakmice 
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
                            KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_domacin_id")));

                            Utakmice u = new Utakmice(reader.GetInt32(0))
                            {
                                KlubDomacin = (Klub)KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_domacin_id"))),
                                KlubGost = (Klub)KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_gost_id"))),
                                KoloUtakmice = (Kolo)KoloDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Kolo_Id")))
                                //KoloUtakmice = reader.GetInt32(reader.GetOrdinal("Kolo")),
                                //KlubDomacin = reader.GetString(reader.GetOrdinal("KlubDomacin")),
                                //KlubGost = reader.GetString(reader.GetOrdinal("KlubGost")),
                            };
                            listaUtakmica.Add(u);
                        }
                    }
                    catch (Exception ee)
                    {

                        throw ee;
                    }

                }
            }
            return listaUtakmica;
        }
    }

}