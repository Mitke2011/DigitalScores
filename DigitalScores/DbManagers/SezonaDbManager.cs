using System;
using System.Collections.Generic;
using DigitalScores.MasterEntities;
using System.Data.SqlClient;
using DigitalScores.Models;
using System.Data;

namespace DigitalScores.DbManagers
{
    public class SezonaDbManager : DbManagerABS
    {
        static SezonaDbManager instance;

        public static SezonaDbManager Current
        {

            get
            {
                if (instance == null)
                {
                    instance = new SezonaDbManager();
                }
                return instance;
            }
        }
        public SezonaDbManager() : base()
        {

        }
        public SezonaDbManager(string connectionString) : base(connectionString)
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
            string sql = "select * from Sezone";
            List<object> result = new List<object>();
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {                    
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Sezona(reader.GetInt32(reader.GetOrdinal("id")))
                        {
                            Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                            Liga = (Liga)LigaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Liga_Id")))
                        });
                    }
                }

            }

            return result;
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
            Sezona s = carrier as Sezona;
            string sql = @"insert into sezone (Naziv, Liga_Id) 
                                        values (@naziv,@ligaId)";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(
                        new SqlParameter[]
                        {                            
                            new SqlParameter() { ParameterName = "@naziv", Value = s.Naziv, DbType = DbType.Int32 },
                            new SqlParameter() { ParameterName = "@ligaId", Value = s.Liga.Id, DbType = DbType.Int32 }
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
            Sezona s = carrier as Sezona;
            string sql = @"update sezone set 
                           Naziv = @naziv,
                            Liga_Id = @ligaId
                            where id = @Id";


            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(
                        new SqlParameter[]
                        {
                            new SqlParameter() { ParameterName = "@Id", Value = s.Id, DbType = DbType.Int32 } ,
                            new SqlParameter() { ParameterName = "@naziv", Value = s.Naziv, DbType = DbType.Int32 },
                            new SqlParameter() { ParameterName = "@ligaId", Value = s.Liga.Id, DbType = DbType.Int32 }
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
                                Naziv = reader.GetString(reader.GetOrdinal("Naziv"))
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

        public void SetActiveSeason(int seasonId)
        {
            string sql = @"update sezone set tekuca = 1
                            where id = @seasonId";

            string sqlResetOthers = @"update sezone set tekuca = 0
                                      where id !=@seasonId";
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@seasonId", Value = seasonId, DbType = DbType.Int32 });

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