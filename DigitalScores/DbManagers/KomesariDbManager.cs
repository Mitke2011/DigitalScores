using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using DigitalScores.MasterEntities;
using DigitalScores.Models;

namespace DigitalScores.DbManagers
{
    public class KomesariDbManager : DbManagerABS
    {
        static KomesariDbManager instance;
        public static KomesariDbManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new KomesariDbManager();
                }
                return instance;
            }
        }
        private KomesariDbManager() : base()
        {

        }

        private KomesariDbManager(string connectionString) : base(connectionString)
        {

        }

        public override void DeleteRange(List<object> collection)
        {
            throw new NotImplementedException();
        }

        public override void DeleteSingle(object Sudija)
        {
            throw new NotImplementedException();
        }

        public override List<object> GetAll()
        {
            throw new NotImplementedException();
        }

        public override object GetSingle(int id)
        {
            Komesari s = null;
            string sql = "select * from komesari where id = @id";

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
                            s = new Komesari(id)
                            {

                                Ime = reader.GetString(reader.GetOrdinal("ime")),
                                Prezime = reader.GetString(reader.GetOrdinal("prezime")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Telefon = reader.GetString(reader.GetOrdinal("telefon")),
                                Liga = (Liga)KomesariDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Liga_Id")))   
                            };
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }

            return s;
        }
        public override void Update(object carrier)
        {
            throw new NotImplementedException();
        }
        //      Dodavanje novog sudije u bazu
        public override void Insert(object komesar)
        {
            Komesari k = komesar as Komesari;
            string sql = "insert into Komesari (Ime, Prezime, Email, Telefon, Liga_Id) values (@ime, @prezime, @email, @telefon, @liga_id)";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(
                new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "@ime", Value = k.Ime, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@prezime", Value = k.Prezime, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@email", Value = k.Email, SqlDbType =System.Data.SqlDbType.NVarChar },
                    new SqlParameter(){ ParameterName = "@telefon", Value = k.Telefon, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@liga_id", Value = k.LigaId, SqlDbType = System.Data.SqlDbType.Int}
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
    }
}