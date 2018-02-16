using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DigitalScores.MasterEntities;
using DigitalScores.Models;
using System.Data.SqlClient;

namespace DigitalScores.DbManagers
{
    public class HalaDbManager : DbManagerABS
    {
        static HalaDbManager instance;
        public static HalaDbManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new HalaDbManager();
                }
                return instance;
            }
        }
        private HalaDbManager() : base()
        {

        }

        private HalaDbManager(string connectionString) : base(connectionString)
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
            Hala h = null;
            string sql = "select * from Hala where id = @id";

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
                            h = new Hala(id)
                            {

                                Naziv = reader.GetString(reader.GetOrdinal("ime")),
                                Grad = reader.GetString(reader.GetOrdinal("prezime"))
                            };
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }

            return h;
        }

        public override void Insert(object hala)
        {
            Hala h = hala as Hala;
            string sql = "insert into Hala (Naziv, Grad) values (@naziv, @grad)";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(
                new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "@naziv", Value = h.Naziv, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@grad", Value = h.Grad, SqlDbType = System.Data.SqlDbType.NVarChar}
                    
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
        public List<Hala> GetHale() {
            List<DigitalScores.Models.Hala> listaHala = new List<DigitalScores.Models.Hala>();
            string sql = @"select * from Hala";

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


                            Hala h = new Hala(reader.GetInt32(0))
                            {
                                Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                                Grad = reader.GetString(reader.GetOrdinal("Grad")),
                               
                            };
                            listaHala.Add(h);
                        }
                    }
                    catch (Exception ee)
                    {

                        throw ee;
                    }

                }
            }

            return listaHala;
        }
    }
}