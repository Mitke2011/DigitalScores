﻿using System;
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
                            LigaKategorija = (Kategorija)KategorijaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Kategorija")))
                        };
                    }
                }

            }

            return result;
        }

        public override void Insert(object carrier)
        {
            throw new NotImplementedException();
        }

        public override void Update(object carrier)
        {
            throw new NotImplementedException();
        }

        public List<Liga> GetLeaguesByCategory(int kategorijaId) {
                     
                List<Liga> listaUtakmica = new List<Liga>();
                string sql = "select * from Lige where Kategorija = @kategorijaId";

                using (connection = new SqlConnection(this.ConnectionString))
                {
                    connection.Open();

                    using (command = new SqlCommand(sql, connection))
                    {
                        try
                        {
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@kategorijaId", SqlDbType = System.Data.SqlDbType.Int, Value = kategorijaId });
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {


                                Liga l = new Liga(reader.GetInt32(0))
                                {
                                    Naziv = reader.GetString(reader.GetOrdinal("Naziv")),    
                                    LigaKategorija = (Kategorija)KategorijaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Kategorija")))

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

        public List<Liga> GetLeagues()
        {

            List<Liga> listaUtakmica = new List<Liga>();
            string sql = "select * from Lige";

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


                            Liga l = new Liga(reader.GetInt32(0))
                            {
                                Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                                LigaKategorija = (Kategorija)KategorijaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Kategorija")))

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