using System;
using System.Collections.Generic;
using DigitalScores.MasterEntities;
using DigitalScores.Models;
using System.Data.SqlClient;

namespace DigitalScores.DbManagers
{
    public class RegionDbManager : DbManagerABS
    {
        static RegionDbManager instance;
        public static RegionDbManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new RegionDbManager();
                }
                return instance;
            }
        }
        private RegionDbManager() : base()
        {

        }

        private RegionDbManager(string connectionString) : base(connectionString)
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
            Region r = null;
            string sql = "select * from Region where id = @id";

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
                            r = new Region(id)
                            {
                                Naziv = reader.GetString(reader.GetOrdinal("naziv"))
                            };
                        }
                    }
                    catch (SqlException se)
                    {
                        throw se;
                    }

                }
            }

            return r;
        }

        public override void Insert(object region)
        {
            Region r = region as Region;
            string sql = "insert into Region (Naziv) values (@naziv)";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(
                new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "@naziv", Value = r.Naziv, SqlDbType = System.Data.SqlDbType.NVarChar}
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

        public override void Update(object region)
        {
            Region r = region as Region;
            string sql = @"update region set Naziv=@naziv WHERE id = @Id";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter() { ParameterName = "@Id",Value = r.Id, SqlDbType = System.Data.SqlDbType.Int}
                    });

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<Region> GetRegion()
        {
            List<Region> lista = new List<Region>();
            string sql = @"select * from region";

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


                            Region r = new Region(reader.GetInt32(0))
                            {
                                Naziv = reader.GetString(reader.GetOrdinal("Naziv"))

                            };
                            lista.Add(r);
                        }
                    }
                    catch (Exception ee)
                    {

                        throw ee;
                    }

                }
            }

            return lista;
        }
    }
}