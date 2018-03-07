using System;
using System.Collections.Generic;
using DigitalScores.MasterEntities;
using System.Data.SqlClient;
using DigitalScores.Models;
using System.Data;


namespace DigitalScores.DbManagers
{
    public class KategorijaDbManager : DbManagerABS
    {
        static KategorijaDbManager instance;
        public static KategorijaDbManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new KategorijaDbManager();
                }
                return instance;
            }
        }

        public KategorijaDbManager():base()
        {

        }
        public KategorijaDbManager(string connectionString):base(connectionString)
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
            string sql = "select * from Kategorije where id = @id";
            Kategorija result = null;
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.Int });
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        result = new Kategorija(id)
                        {
                            Naziv = reader.GetString(reader.GetOrdinal("Naziv"))
                        };
                    }
                }

            }

            return result;
        }

        public override void Update(object kategorija)
        {

            Kategorija k = kategorija as Kategorija;
            string sql = @"update Kategorije set Naziv=@naziv WHERE id = @katId";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter() { ParameterName = "@katId",Value = k.Id, SqlDbType = SqlDbType.Int},
                        new SqlParameter() { ParameterName = "@naziv",Value = k.Naziv, SqlDbType = SqlDbType.NVarChar},
                      });

                    command.ExecuteNonQuery();
                }
            }
        }

        public override void Insert(object kategorija)
        {
            Kategorija k = kategorija as Kategorija;
            string sql = "insert into Kategorije (Naziv)values (@naziv)";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(
                new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "@naziv", Value = k.Naziv, SqlDbType = SqlDbType.NVarChar},
                   
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

        public List<Kategorija> GetKategorije() {
            List<DigitalScores.Models.Kategorija> listaKategorija = new List<DigitalScores.Models.Kategorija>();
            string sql = @"select * from Kategorije";

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


                            Kategorija k = new Kategorija(reader.GetInt32(0))
                            {
                                Naziv = reader.GetString(reader.GetOrdinal("Naziv"))
                               
                            };
                            listaKategorija.Add(k);
                        }
                    }
                    catch (Exception ee)
                    {

                        throw ee;
                    }

                }
            }

            return listaKategorija;
        }
    }
}