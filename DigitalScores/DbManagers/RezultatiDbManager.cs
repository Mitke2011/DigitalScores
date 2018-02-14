using System;
using System.Collections.Generic;
using DigitalScores.MasterEntities;
using DigitalScores.Models;
using System.Data.SqlClient;

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

        public override void Update(object carrier)
        {
            throw new NotImplementedException();
        }

        public override void Insert(object rez)
        {
            Rezultati r = rez as Rezultati;

            string sql = "insert into rezultati" +
                " (rezultat_Q1_D, rezultat_Q2_D, rezultat_Q3_D, rezultat_Q4_D, rezultat_OT1_D, rezultat_OT2_D, rezultat_H1_D, rezultat_H2_D," +
                " rezultat_Q1_G, rezultat_Q2_G, rezultat_Q3_G, rezultat_Q4_G, rezultat_OT1_G, rezultat_OT2_G, rezultat_H1_G, rezultat_H2_G) " +
                "values (@rezultat_Q1_D, @rezultat_Q2_D, @rezultat_Q3_D, @rezultat_Q4_D, @rezultat_OT1_D, @rezultat_OT2_D, @rezultat_H1_D, @rezultat_H2_D, " +
                "@rezultat_Q1_G, @rezultat_Q2_G, @rezultat_Q3_G, @rezultat_Q4_G, @rezultat_OT1_G, @rezultat_OT2_G, @rezultat_H1_G, @rezultat_H2_G)";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "rezultat_Q1_D", Value = r.RezultatQ1D, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q2_D", Value = r.RezultatQ2D, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q3_D", Value = r.RezultatQ3D, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q4_D", Value = r.RezultatQ4D, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_OT1_D", Value = r.RezultatOT1D, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_OT2_D", Value = r.RezultatOT2D, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_H1_D", Value = r.RezultatH1D, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_H2_D", Value = r.RezultatH2D, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q1_G", Value = r.RezultatQ1D, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q2_G", Value = r.RezultatQ2D, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q3_G", Value = r.RezultatQ3D, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_Q4_G", Value = r.RezultatQ4D, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_OT1_G", Value = r.RezultatOT1D, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_OT2_G", Value = r.RezultatOT2D, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_H1_G", Value = r.RezultatH1D, SqlDbType = System.Data.SqlDbType.Int},
                    new SqlParameter(){ ParameterName = "rezultat_H2_G", Value = r.RezultatH2D, SqlDbType = System.Data.SqlDbType.Int}
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
            throw new NotImplementedException();
        }
    }
}