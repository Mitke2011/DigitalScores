using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DigitalScores.MasterEntities;
using DigitalScores.Models;

namespace DigitalScores.DbManagers
{
    public class RefereeDbManager : DbManagerABS
    {
        static RefereeDbManager instance;
        public static RefereeDbManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new RefereeDbManager();
                }
                return instance;
            }
        }
        private RefereeDbManager() : base()
        {

        }

        private RefereeDbManager(string connectionString) : base(connectionString)
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
            Sudija s = null;
            string sql = "select * from sudije where id = @id";

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
                            s = new Sudija(id)
                            {
                                
                                Ime = reader.GetString(reader.GetOrdinal("ime")),
                                Prezime = reader.GetString(reader.GetOrdinal("prezime")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Grad = reader.GetString(reader.GetOrdinal("grad")),
                                Telefon = reader.GetString(reader.GetOrdinal("telefon"))
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
        public override void Update(object sudija)
        {
            Sudija s = sudija as Sudija;
            string sql = @"update Sudije set Ime=@ime, Prezime=@prezime, Email = @email, Telefon = @telefon, Grad = @grad WHERE id = @sudijaId";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter() { ParameterName = "@sudijaId",Value = s.Id, SqlDbType = System.Data.SqlDbType.Int},
                        new SqlParameter() { ParameterName = "@Ime",Value = s.Ime, SqlDbType = System.Data.SqlDbType.NVarChar},
                        new SqlParameter() { ParameterName = "@prezime",Value = s.Prezime, SqlDbType = System.Data.SqlDbType.NVarChar},
                        new SqlParameter() { ParameterName = "@email",Value = s.Email, SqlDbType = System.Data.SqlDbType.NVarChar},
                        new SqlParameter() { ParameterName = "@telefon",Value = s.Telefon, SqlDbType = System.Data.SqlDbType.NVarChar},
                        new SqlParameter() { ParameterName = "@grad ",Value = s.Grad, SqlDbType = System.Data.SqlDbType.NVarChar}

                    });

                    command.ExecuteNonQuery();
                }
            }
        }
//      Dodavanje novog sudije u bazu
        public override void Insert(object referee)
        {
            Sudija s = referee as Sudija;
            string sql = "insert into Sudije (Ime, Prezime, Email, Telefon, Grad) values (@ime, @prezime, @email, @telefon, @grad)";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(
                new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "@ime", Value = s.Ime, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@prezime", Value = s.Prezime, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@email", Value = s.Email, SqlDbType =System.Data.SqlDbType.NVarChar },
                    new SqlParameter(){ ParameterName = "@telefon", Value = s.Telefon, SqlDbType = System.Data.SqlDbType.NVarChar},
                    new SqlParameter(){ ParameterName = "@grad", Value = s.Grad, SqlDbType = System.Data.SqlDbType.NVarChar}
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

        public List<Sudija> GetAllReferee() {
            List<Sudija> listaSudija = new List<Sudija>();
            string sql = @"select * from Sudije";

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


                            Sudija s = new Sudija(reader.GetInt32(0))
                            {
                                Ime = reader.GetString(reader.GetOrdinal("Ime")),
                                Prezime = reader.GetString(reader.GetOrdinal("Prezime")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Telefon = reader.GetString(reader.GetOrdinal("Telefon")),
                                Grad = reader.GetString(reader.GetOrdinal("Grad")),
                                ImeiPrezime = reader.GetString(reader.GetOrdinal("Ime")) + " " + reader.GetString(reader.GetOrdinal("Prezime"))
                                //KlubGost = reader.GetString(reader.GetOrdinal("KlubGost")),
                            };
                            listaSudija.Add(s);
                        }
                    }
                    catch (Exception ee)
                    {

                        throw ee;
                    }

                }
            }

            return listaSudija;
        }


    }
}