﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public RefereeDbManager() : base()
        {

        }

        public RefereeDbManager(string connectionString) : base(connectionString)
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
            string sql = "select * from sudija where id = @id";

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
        public override void Update(object carrier)
        {
            throw new NotImplementedException();
        }
//      Dodavanje novog sudije u bazu
        public override void Insert(object referee)
        {
            Sudija s = referee as Sudija;
            string sql = "insert into Sudija (Ime, Prezime, Email, Telefon, Grad) values (@s.Ime, @s.Prezime, @s.Email, @s.Telefon, @s.Grad)";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = 2 });
                    try
                    {
                       
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
        }
    }
}