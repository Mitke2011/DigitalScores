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
    public class KlubStatsDbManager : DbManagerABS
    {
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

        public List<KluboviStats> GetAllStatsForLeagueAndSeason(int seasonId)
        {
            List<KluboviStats> result = new List<KluboviStats>();
            string sql = "select * from KluboviStats where sezona_Id = @sezonaId";

            using (connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() {ParameterName = "@sezonaId", Value = seasonId, SqlDbType = SqlDbType.Int });
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new KluboviStats(reader.GetInt32(reader.GetOrdinal("id")))
                        {
                            BodoviPobeda = reader.GetInt32(reader.GetOrdinal("Bodovi_Pobeda")),
                            BodoviPoraz = reader.GetInt32(reader.GetOrdinal("Bodovi_Poraz")),
                            KlubForStat = (Klub)KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_Id"))),
                            OdigranoUtakmica = reader.GetInt32(reader.GetOrdinal("Odigrano_Utakmica")),
                            PostignutiPoeni = reader.GetInt32(reader.GetOrdinal("Postignuti_Poeni")),
                            PrimljeniPoeni = reader.GetInt32(reader.GetOrdinal("Primljeni_Poeni")),
                            UkupnoBodova = reader.GetInt32(reader.GetOrdinal("Ukupno_Bodova")),
                            SezonaStats = (Sezona)SezonaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Sezona_Id")))
                        });
                    }
                }
            }
            return result;
        }

        public override object GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public KluboviStats GetStatsForSingleKlub(int clubId, int sezonaId)
        {
            KluboviStats result = null;
            string sql = "select * from KluboviStats where sezona_Id = @sezonaId and klub_id = @clubId";

            using (connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@sezonaId", Value = sezonaId, SqlDbType = SqlDbType.Int });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@clubId", Value = clubId, SqlDbType = SqlDbType.Int });

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        result = new KluboviStats(reader.GetInt32(reader.GetOrdinal("id")))
                        {
                            BodoviPobeda = reader.GetInt32(reader.GetOrdinal("Bodovi_Pobeda")),
                            BodoviPoraz = reader.GetInt32(reader.GetOrdinal("Bodovi_Poraz")),
                            KlubForStat = (Klub)KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_Id"))),
                            OdigranoUtakmica = reader.GetInt32(reader.GetOrdinal("Odigrano_Utakmica")),
                            PostignutiPoeni = reader.GetInt32(reader.GetOrdinal("Postignuti_Poeni")),
                            PrimljeniPoeni = reader.GetInt32(reader.GetOrdinal("Primljeni_Poeni")),
                            UkupnoBodova = reader.GetInt32(reader.GetOrdinal("Ukupno_Bodova")),
                            SezonaStats = (Sezona)SezonaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Sezona_Id")))
                        };
                    }
                }
            }
            return result;
        }

        public override void Insert(object carrier)
        {
            KluboviStats ks = carrier as KluboviStats;
            string sql = @"insert into KluboviStats ([Klub_Id],[Primljeni_Poeni], [Postignuti_Poeni],[Odigrano_Utakmica], [Bodovi_Pobeda],[Bodovi_Poraz],[Ukupno_Bodova],[Sezona_Id])
                                             VALUES (@Klub_Id,@Primljeni_Poeni, @Postignuti_Poeni, @Odigrano_Utakmica, @Bodovi_Pobeda, @Bodovi_Poraz, @Ukupno_Bodova, @Sezona_Id)";
            try
            {
                using (connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (command = new SqlCommand(sql,connection))
                    {
                        command.Parameters.AddRange(new SqlParameter[] 
                        {
                            new SqlParameter() {ParameterName = "@Klub_Id", Value = ks.KlubForStat.Id, SqlDbType = SqlDbType.Int },
                            new SqlParameter() {ParameterName = "@Primljeni_Poeni", Value = ks.PrimljeniPoeni, SqlDbType = SqlDbType.Int },
                            new SqlParameter() {ParameterName = "@Postignuti_Poeni", Value = ks.PostignutiPoeni, SqlDbType = SqlDbType.Int },
                            new SqlParameter() {ParameterName = "@Odigrano_Utakmica", Value = ks.OdigranoUtakmica, SqlDbType = SqlDbType.Int },
                            new SqlParameter() {ParameterName = "@Bodovi_Pobeda", Value = ks.BodoviPobeda, SqlDbType = SqlDbType.Int },
                            new SqlParameter() {ParameterName = "@Bodovi_Poraz", Value = ks.BodoviPoraz, SqlDbType = SqlDbType.Int },
                            new SqlParameter() {ParameterName = "@Ukupno_Bodova", Value = ks.UkupnoBodova, SqlDbType = SqlDbType.Int },
                            new SqlParameter() {ParameterName = "@Sezona_Id", Value = ks.SezonaStats.Id, SqlDbType = SqlDbType.Int },
                        });
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException se)
            {
                throw se;
            }
            
        }

        public override void Update(object carrier)
        {
            KluboviStats ks = carrier as KluboviStats;
            string sql = @"update KluboviStats SET [Klub_Id] = @Klub_Id,
                                                   [Primljeni_Poeni] = @Primljeni_Poeni,
                                                   [Postignuti_Poeni] = @Postignuti_Poeni, 
                                                   [Odigrano_Utakmica] = @Odigrano_Utakmica,
                                                   [Bodovi_Pobeda] = @Bodovi_Pobeda,
                                                   [Bodovi_Poraz] = @Bodovi_Poraz,
                                                   [Ukupno_Bodova] = @Ukupno_Bodova,
                                                   [Sezona_Id] = @Sezona_Id
                                                WHERE id = @id";
            try
            {
                using (connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter() {ParameterName = "@id", Value = ks.Id, SqlDbType = SqlDbType.Int },
                            new SqlParameter() {ParameterName = "@Klub_Id", Value = ks.KlubForStat.Id, SqlDbType = SqlDbType.Int },
                            new SqlParameter() {ParameterName = "@Primljeni_Poeni", Value = ks.PrimljeniPoeni, SqlDbType = SqlDbType.Int },
                            new SqlParameter() {ParameterName = "@Postignuti_Poeni", Value = ks.PostignutiPoeni, SqlDbType = SqlDbType.Int },
                            new SqlParameter() {ParameterName = "@Odigrano_Utakmica", Value = ks.OdigranoUtakmica, SqlDbType = SqlDbType.Int },
                            new SqlParameter() {ParameterName = "@Bodovi_Pobeda", Value = ks.BodoviPobeda, SqlDbType = SqlDbType.Int },
                            new SqlParameter() {ParameterName = "@Bodovi_Poraz", Value = ks.BodoviPoraz, SqlDbType = SqlDbType.Int },
                            new SqlParameter() {ParameterName = "@Ukupno_Bodova", Value = ks.UkupnoBodova, SqlDbType = SqlDbType.Int },
                            new SqlParameter() {ParameterName = "@Sezona_Id", Value = ks.SezonaStats.Id, SqlDbType = SqlDbType.Int },
                        });
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException se)
            {
                throw se;
            }
        }
    }
}