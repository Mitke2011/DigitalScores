using System;
using System.Collections.Generic;
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
        public override void Update(object komesar)
        {

            Komesari k = komesar as Komesari;
            string sql = @"update Komesari set Ime=@ime, Prezime=@prezime, Email =@email, Telefon = @telefon, Liga_Id =@liga_id WHERE id = @KomesarId";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter() { ParameterName = "@KomesarId",Value = k.Id, SqlDbType = System.Data.SqlDbType.Int},
                        new SqlParameter() { ParameterName = "@ime",Value = k.Ime, SqlDbType = System.Data.SqlDbType.NVarChar},
                        new SqlParameter() { ParameterName = "@prezime",Value = k.Prezime, SqlDbType = System.Data.SqlDbType.NVarChar},
                        new SqlParameter() { ParameterName = "@email",Value = k.Email,SqlDbType = System.Data.SqlDbType.NVarChar},
                        new SqlParameter() { ParameterName = "@telefon",Value = k.Telefon, SqlDbType = System.Data.SqlDbType.NVarChar},
                        new SqlParameter() { ParameterName = "@liga_Id",Value = k.LigaId, SqlDbType = System.Data.SqlDbType.NVarChar}
                    });

                    command.ExecuteNonQuery();
                }
            }
        }
        //      Dodavanje novog Komesara u bazu
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

        public List<DigitalScores.Models.Komesari> GetAllKomesari()
        {
            List<DigitalScores.Models.Komesari> listaKomesara = new List<DigitalScores.Models.Komesari>();
            string sql = @"select * from Komesari";

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


                            Komesari k = new Komesari(reader.GetInt32(0))
                            {
                                Ime = reader.GetString(reader.GetOrdinal("Ime")),
                                Prezime = reader.GetString(reader.GetOrdinal("Prezime")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Telefon = reader.GetString(reader.GetOrdinal("Telefon")),
                                Liga = (Liga)LigaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Liga_Id"))),
                                //KlubGost = reader.GetString(reader.GetOrdinal("KlubGost")),
                            };
                            listaKomesara.Add(k);
                        }
                    }
                    catch (Exception ee)
                    {

                        throw ee;
                    }

                }
            }

            return listaKomesara;
        }

        public bool CheckIfKomesarExists(Komesari komesar)
        {
            bool result = false;
            string sql = "select COUNT([Email]) from Komesari where Email = @email";
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "Email", Value = komesar.Email, SqlDbType = System.Data.SqlDbType.NVarChar},

                    });
                }


                try
                {
                    int counter = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        counter = reader.GetInt32(0);
                    }
                    if (counter > 0)
                    {
                        result = true;
                        return result;
                    }
                }
                catch (Exception se)
                {

                    throw se;
                }
                return result;
            }
        }
    }
}