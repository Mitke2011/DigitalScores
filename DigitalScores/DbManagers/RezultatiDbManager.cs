using System;
using System.Collections.Generic;
using DigitalScores.MasterEntities;
using DigitalScores.Models;
using System.Data.SqlClient;
using System.Data;

namespace DigitalScores.DbManagers
{
    public class RezultatiDbManager : DbManagerABS
    {
        static RezultatiDbManager instance;
        public static RezultatiDbManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new RezultatiDbManager();
                }
                return instance;
            }
        }
        private RezultatiDbManager() : base()
        {

        }

        private RezultatiDbManager(string connectionString) : base(connectionString)
        {

        }

        public override void DeleteRange(List<object> collection)
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

        public List<Rezultati> GetAllScoresForGameRange(int[] gameIdRange)
        {
            string condition = string.Join(",", gameIdRange); ;

            List<Rezultati> res = new List<Rezultati>();
            string sql = string.Format("select * from rezultati where Utakmica_Id in ({0})",condition);

            using (connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql,connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        res.Add(new Rezultati(reader.GetInt32(reader.GetOrdinal("id")))
                        {
                            RezultatQ1D = reader.GetInt32(reader.GetOrdinal("rezultat_Q1_D")),
                            RezultatQ2D = reader.GetInt32(reader.GetOrdinal("rezultat_Q2_D")),
                            RezultatQ3D = reader.GetInt32(reader.GetOrdinal("rezultat_Q3_D")),
                            RezultatQ4D = reader.GetInt32(reader.GetOrdinal("rezultat_Q4_D")),
                            RezultatOT1D = reader.GetInt32(reader.GetOrdinal("rezultat_OT1_D")),
                            RezultatOT2D = reader.GetInt32(reader.GetOrdinal("rezultat_OT2_D")),
                            RezultatH1D = reader.GetInt32(reader.GetOrdinal("rezultat_H1_D")),
                            RezultatH2D = reader.GetInt32(reader.GetOrdinal("rezultat_H2_D")),
                            RezultatQ1G = reader.GetInt32(reader.GetOrdinal("rezultat_Q1_G")),
                            RezultatQ2G = reader.GetInt32(reader.GetOrdinal("rezultat_Q2_G")),
                            RezultatQ3G = reader.GetInt32(reader.GetOrdinal("rezultat_Q3_G")),
                            RezultatQ4G = reader.GetInt32(reader.GetOrdinal("rezultat_Q4_G")),
                            RezultatOT1G = reader.GetInt32(reader.GetOrdinal("rezultat_OT1_G")),
                            RezultatOT2G = reader.GetInt32(reader.GetOrdinal("rezultat_OT2_G")),
                            RezultatH1G = reader.GetInt32(reader.GetOrdinal("rezultat_H1_G")),
                            RezultatH2G = reader.GetInt32(reader.GetOrdinal("rezultat_H2_G")),
                            RezultatKonacniD = reader.GetInt32(reader.GetOrdinal("Rezultat_Konacni_D")),
                            RezultatKonacniG = reader.GetInt32(reader.GetOrdinal("Rezultat_Konacni_G")),
                            RezultatUtakmica = (Utakmice)UtakmicaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Utakmica_Id")))
                        });
                    }
                }
            }


            return res;
        }

        public override void Update(object carrier)
        {
            Rezultati r = carrier as Rezultati;

            string sql = @"update rezultati set
                                                rezultat_Q1_D =@rezultat_Q1_D, 
                                                rezultat_Q2_D = @rezultat_Q2_D, 
                                                rezultat_Q3_D = @rezultat_Q3_D, 
                                                rezultat_Q4_D = @rezultat_Q4_D, 
                                                rezultat_OT1_D = @rezultat_OT1_D, 
                                                rezultat_OT2_D = @rezultat_OT2_D, 
                                                rezultat_H1_D = @rezultat_H1_D, 
                                                rezultat_H2_D = @rezultat_H2_D,
                                                rezultat_Q1_G = @rezultat_Q1_G, 
                                                rezultat_Q2_G = @rezultat_Q2_G, 
                                                rezultat_Q3_G = @rezultat_Q3_G, 
                                                rezultat_Q4_G = @rezultat_Q4_G, 
                                                rezultat_OT1_G = @rezultat_OT1_G, 
                                                rezultat_OT2_G = @rezultat_OT2_G, 
                                                rezultat_H1_G = @rezultat_H1_G, 
                                                rezultat_H2_G = @rezultat_H2_G, 
                                                Rezultat_Konacni_D = @Rezultat_Konacni_G, 
                                                Rezultat_Konacni_G = @Rezultat_Konacni_D, 
                                                Utakmica_Id = @Utakmica_Id
                                            WHERE ID = @id";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "@id", Value = r.Id, SqlDbType = SqlDbType.Int},
                   new SqlParameter(){ ParameterName = "rezultat_Q1_D", Value = r.RezultatQ1D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q2_D", Value = r.RezultatQ2D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q3_D", Value = r.RezultatQ3D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q4_D", Value = r.RezultatQ4D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_OT1_D", Value = r.RezultatOT1D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_OT2_D", Value = r.RezultatOT2D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_H1_D", Value = r.RezultatH1D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_H2_D", Value = r.RezultatH2D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q1_G", Value = r.RezultatQ1G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q2_G", Value = r.RezultatQ2G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q3_G", Value = r.RezultatQ3G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q4_G", Value = r.RezultatQ4G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_OT1_G", Value = r.RezultatOT1G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_OT2_G", Value = r.RezultatOT2G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_H1_G", Value = r.RezultatH1G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_H2_G", Value = r.RezultatH2G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "Rezultat_Konacni_D", Value = r.RezultatKonacniD, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "Rezultat_Konacni_G", Value = r.RezultatKonacniG, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "Utakmica_Id", Value = r.UtakmicaId, SqlDbType = SqlDbType.Int}
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

        public override void Insert(object rez)
        {
            Rezultati r = rez as Rezultati;

            string sql = @"insert into rezultati
                (rezultat_Q1_D, rezultat_Q2_D, rezultat_Q3_D, rezultat_Q4_D, rezultat_OT1_D, rezultat_OT2_D, rezultat_H1_D, rezultat_H2_D,
                rezultat_Q1_G, rezultat_Q2_G, rezultat_Q3_G, rezultat_Q4_G, rezultat_OT1_G, rezultat_OT2_G, rezultat_H1_G, rezultat_H2_G, Rezultat_Konacni_D, Rezultat_Konacni_G, Utakmica_Id) 
                values (@rezultat_Q1_D, @rezultat_Q2_D, @rezultat_Q3_D, @rezultat_Q4_D, @rezultat_OT1_D, @rezultat_OT2_D, @rezultat_H1_D, @rezultat_H2_D, 
                @rezultat_Q1_G, @rezultat_Q2_G, @rezultat_Q3_G, @rezultat_Q4_G, @rezultat_OT1_G, @rezultat_OT2_G, @rezultat_H1_G, @rezultat_H2_G, @Rezultat_Konacni_G, @Rezultat_Konacni_D, @Utakmica_Id)";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "rezultat_Q1_D", Value = r.RezultatQ1D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q2_D", Value = r.RezultatQ2D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q3_D", Value = r.RezultatQ3D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q4_D", Value = r.RezultatQ4D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_OT1_D", Value = r.RezultatOT1D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_OT2_D", Value = r.RezultatOT2D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_H1_D", Value = r.RezultatH1D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_H2_D", Value = r.RezultatH2D, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q1_G", Value = r.RezultatQ1G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q2_G", Value = r.RezultatQ2G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q3_G", Value = r.RezultatQ3G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q4_G", Value = r.RezultatQ4G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_OT1_G", Value = r.RezultatOT1G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_OT2_G", Value = r.RezultatOT2G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_H1_G", Value = r.RezultatH1G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_H2_G", Value = r.RezultatH2G, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "Rezultat_Konacni_D", Value = r.RezultatKonacniD, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "Rezultat_Konacni_G", Value = r.RezultatKonacniG, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "Utakmica_Id", Value = r.UtakmicaId, SqlDbType = SqlDbType.Int}
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

        public override object GetSingle(int id)
        {
            Rezultati res = null;
            string sql = "select * from rezultati where id =@id";

            using (connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql,connection))
                {
                    command.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@id", Value = id, SqlDbType = SqlDbType.Int
                    });

                   SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        res = new Rezultati(reader.GetInt32(reader.GetOrdinal("id")))
                        {
                            RezultatQ1D = reader.GetInt32(reader.GetOrdinal("rezultat_Q1_D")),
                            RezultatQ2D = reader.GetInt32(reader.GetOrdinal("rezultat_Q2_D")),
                            RezultatQ3D = reader.GetInt32(reader.GetOrdinal("rezultat_Q3_D")),
                            RezultatQ4D = reader.GetInt32(reader.GetOrdinal("rezultat_Q4_D")),
                            RezultatOT1D = reader.GetInt32(reader.GetOrdinal("rezultat_OT1_D")),
                            RezultatOT2D = reader.GetInt32(reader.GetOrdinal("rezultat_OT2_D")),
                            RezultatH1D = reader.GetInt32(reader.GetOrdinal("rezultat_H1_D")),
                            RezultatH2D = reader.GetInt32(reader.GetOrdinal("rezultat_H2_D")),
                            RezultatQ1G = reader.GetInt32(reader.GetOrdinal("rezultat_Q1_G")),
                            RezultatQ2G = reader.GetInt32(reader.GetOrdinal("rezultat_Q2_G")),
                            RezultatQ3G = reader.GetInt32(reader.GetOrdinal("rezultat_Q3_G")),
                            RezultatQ4G = reader.GetInt32(reader.GetOrdinal("rezultat_Q4_G")),
                            RezultatOT1G = reader.GetInt32(reader.GetOrdinal("rezultat_OT1_G")),
                            RezultatOT2G = reader.GetInt32(reader.GetOrdinal("rezultat_OT2_G")),
                            RezultatH1G = reader.GetInt32(reader.GetOrdinal("rezultat_H1_G")),
                            RezultatH2G = reader.GetInt32(reader.GetOrdinal("rezultat_H2_G")),
                            RezultatKonacniD = reader.GetInt32(reader.GetOrdinal("Rezultat_Konacni_D")),
                            RezultatKonacniG = reader.GetInt32(reader.GetOrdinal("Rezultat_Konacni_G")),
                            RezultatUtakmica = (Utakmice)UtakmicaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Utakmica_Id")))
                        };
                    }
                }
            }

            return res;

        }
    }
}