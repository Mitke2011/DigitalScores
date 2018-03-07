using System;
using System.Collections.Generic;
using DigitalScores.MasterEntities;
using System.Data.SqlClient;
using DigitalScores.Models;
using System.Data;

namespace DigitalScores.DbManagers
{
    public class LigaDbManager : DbManagerABS
    {
        static LigaDbManager instance;

        public static LigaDbManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new LigaDbManager();
                }
                return instance;
            }
        }
        private LigaDbManager() : base()
        {

        }

        private LigaDbManager(string connectionString) : base(connectionString)
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

        public List<Liga> FindLeagueByNameAndCat(string nazivLige, string kategorijaLige, int userRegionId)
        {
            List<Liga> result = new List<Liga>();
            string whereSection = string.Format(@" where l.regionId = {0} ", userRegionId);

            if (nazivLige != string.Empty || kategorijaLige != string.Empty)
            {
                whereSection = GenerateWhereSection(nazivLige, kategorijaLige, userRegionId);
            }           

            string sql = string.Format(@"select l.Id as LigaId, l.Naziv as LigaNaziv, k.Id as KatId, k.Naziv as katNaziv
                           from Lige l 
                           inner join Kategorije k on l.Kategorija = k.Id
                           {0}", whereSection);

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Liga((int)reader["LigaId"])
                        {
                            Naziv = reader["LigaNaziv"].ToString(),
                            LigaKategorija = new Kategorija((int)reader["KatId"]) { Naziv = reader["katNaziv"].ToString() },
                            LigaRegion = (Region)RegionDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("regionId")))
                        });
                    }
                }
            }
            return result;
        }

        private string GenerateWhereSection(string nazivLige, string kategorijaLige, int userRegionId)
        {
            string whereSection = "";
              if (nazivLige != string.Empty)
                {
                    if (kategorijaLige == string.Empty)
                    {
                        whereSection = string.Format(@" where l.Naziv like '%{0}%' and l.regionId = {1}", nazivLige,userRegionId);
                    }
                    else
                    {
                        whereSection = string.Format(@" where l.Naziv like '%{0}%' and k.Naziv like '%{1}%' and l.regionId = {2}", nazivLige, kategorijaLige, userRegionId);
                    }

                }
                else
                {
                    if (kategorijaLige != string.Empty)
                    {
                        whereSection = string.Format(@" where k.Naziv like('%{0}%') and l.regionId = {1}", kategorijaLige, userRegionId);
                    }
                }
            

            return whereSection;
        }

        public override object GetSingle(int id)
        {
            string sql = "select * from Lige where id = @id";
            Liga result = null;
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.Int });
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        result = new Liga(id)
                        {
                            Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                            LigaKategorija = (Kategorija)KategorijaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Kategorija"))),
                            LigaRegion = (Region)RegionDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("regionId")))

                        };
                    }
                }

            }

            return result;
        }

        public override void Insert(object liga)
        {
            Liga l = liga as Liga;
            string sql = "insert into Lige (Naziv,Kategorija,regionid) values (@naziv, @katId,@region)";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(
                new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "@katId", Value = l.kategorijaId, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "@naziv", Value = l.Naziv, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@region", Value = l.LigaRegion.Id, SqlDbType = SqlDbType.Int}

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

        public override void Update(object liga)
        {
            Liga l = liga as Liga;
            string sql = @"update Lige set Naziv=@naziv, regionId = @region, kategorija=@katId WHERE id = @ligaId";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter() { ParameterName = "@ligaId",Value = l.Id, SqlDbType = SqlDbType.Int},
                        new SqlParameter() { ParameterName = "@naziv",Value = l.Naziv, SqlDbType = SqlDbType.NVarChar},
                        new SqlParameter() { ParameterName = "@katId",Value = l.kategorijaId, SqlDbType = SqlDbType.Int},
                        new SqlParameter(){ ParameterName = "@region", Value = l.LigaRegion.Id, SqlDbType = SqlDbType.Int }
                    });

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Liga> GetLeaguesByCategory(int kategorijaId, int userRegionId)
        {

            List<Liga> listaUtakmica = new List<Liga>();
            string sql = "select * from Lige where Kategorija = @kategorijaId and regionId = @region";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        command.Parameters.AddRange(
                            new SqlParameter[] {
                                new SqlParameter() { ParameterName = "@region", Value = userRegionId, SqlDbType = SqlDbType.Int },
                                new SqlParameter() { ParameterName = "@kategorijaId", SqlDbType = SqlDbType.Int, Value = kategorijaId }
                                });

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Liga l = new Liga(reader.GetInt32(0))
                            {
                                Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                                LigaKategorija = (Kategorija)KategorijaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Kategorija"))),
                                LigaRegion = (Region)RegionDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("regionId")))
                            };
                            listaUtakmica.Add(l);
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

        public List<Liga> GetLeagues(int userRegionId)
        {

            List<Liga> listaUtakmica = new List<Liga>();
            string sql = "select * from Lige where regionId = @region";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@region", Value = userRegionId, SqlDbType = SqlDbType.Int });
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {


                            Liga l = new Liga(reader.GetInt32(0))
                            {
                                Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                                LigaKategorija = (Kategorija)KategorijaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Kategorija"))),
                                LigaRegion = (Region)RegionDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("regionId")))

                            };
                            listaUtakmica.Add(l);
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