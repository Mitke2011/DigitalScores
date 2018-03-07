using System;
using System.Collections.Generic;
using DigitalScores.MasterEntities;
using System.Data.SqlClient;
using DigitalScores.Models;
using System.Data;

namespace DigitalScores.DbManagers
{
    public class KlubDbManager : DbManagerABS
    {
        static KlubDbManager instance;
        public static KlubDbManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new KlubDbManager();
                }
                return instance;
            }
        }

        private KlubDbManager() : base()
        {

        }
        private KlubDbManager(string connectionString) : base(connectionString)
        {

        }
        public override void DeleteRange(List<object> collection)
        {            
            string idCollection = "";
            foreach (var item in collection)
            {
                idCollection = (item as Klub).Id.ToString()+",";
            }

            idCollection = idCollection.Remove(idCollection.LastIndexOf(','));
            string sql = "delete from Klub where id in(@KlubId)";
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@KlubId", Value = idCollection, DbType = DbType.Int32 });
                    command.ExecuteNonQuery();
                }
            }
        }

        public override void DeleteSingle(object carrier)
        {
            Klub data = carrier as Klub;
            string sql = "delete from Klub where id =@KlubId";
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql,connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@KlubId" , Value = data.Id,DbType = DbType.Int32});
                    command.ExecuteNonQuery();
                }
            }
        }

        public override List<object> GetAll()
        {
            string sql = "select * from klub";
            List<object> result = new List<object>();
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Klub item = new Klub(reader.GetInt32(reader.GetOrdinal("id")))
                        {
                            Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                            LigaKlub = (Liga)LigaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("ligaId"))),
                            Grad = reader.GetString(reader.GetOrdinal("Grad")),
                            Trener = reader.GetString(reader.GetOrdinal("Trener")),
                            KlubSport = (Sport)SportDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("sport_id"))),
                            LicencaPDF = reader.GetString(reader.GetOrdinal("licencapdf"))
                        };
                        result.Add(item);
                    }
                }

            }
            return result;
        }

        public override object GetSingle(int id)
        {
            string sql = "select * from klub where id = @id";
            Klub result = null;
            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.Int });
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        result = new Klub(id)
                        {
                            Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                            LigaKlub = (Liga)LigaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("ligaId"))),
                            Grad = reader.GetString(reader.GetOrdinal("Grad")),
                            Trener = reader.GetString(reader.GetOrdinal("Trener")),
                            KlubSport = (Sport)SportDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("sport_id"))),
                            LicencaPDF = reader.GetString(reader.GetOrdinal("licencapdf"))
                        };
                    }
                }

            }

            return result;
        }

        public override void Insert(object carrier)
        {
            Klub data = carrier as Klub;
            string sql = @"insert into Klub 
                           (Naziv,Trener,LigaId,Grad,Sport_id,Licencapdf) 
                     values(@Naziv,@Trener,@LigaID,@Grad,@sportid,@licenca)";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter() { ParameterName = "@Naziv",Value = data.Naziv, DbType = DbType.String},
                        new SqlParameter() { ParameterName = "@Trener",Value = data.Trener, DbType = DbType.String},
                        new SqlParameter() { ParameterName = "@Grad",Value = data.Grad, DbType = DbType.String},
                        new SqlParameter() { ParameterName = "@LigaID",Value = data.LigaKlub.Id, DbType = DbType.Int32},
                        new SqlParameter() { ParameterName = "@sportid",Value = data.KlubSport.Id, DbType = DbType.Int32},
                        new SqlParameter() { ParameterName = "@licenca",Value = data.LicencaPDF, DbType = DbType.String}
                    });
                    command.ExecuteNonQuery();
                }
            }
        }

        public override void Update(object carrier)
        {
            Klub data = carrier as Klub;
            string sql = @"update Klub 
                            set 
                            Naziv=@Naziv,
                            Trener=@Trener,
                            LigaId =@LigaID, 
                            Sport_id = @sportid,
                            Licencapdf =@licenca
                            WHERE iD = @KlubId";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter() { ParameterName = "@KlubId",Value = data.Id, DbType = DbType.Int32},
                        new SqlParameter() { ParameterName = "@Naziv",Value = data.Naziv, DbType = DbType.String},
                        new SqlParameter() { ParameterName = "@Trener",Value = data.Trener, DbType = DbType.String},
                        new SqlParameter() { ParameterName = "@LigaID",Value = data.LigaKlub.Id, DbType = DbType.Int32},
                        new SqlParameter() { ParameterName = "@sportid",Value = data.KlubSport.Id, DbType = DbType.Int32},
                        new SqlParameter() { ParameterName = "@licenca",Value = data.LicencaPDF, DbType = DbType.String}
                    });
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Klub> GetClubsByLeague(int ligaId)
        {
            List<Klub> listaKlubova = new List<Klub>();
            string sql = @"select * 
                           from Klub 
                           where LigaId = @liga_id";

            using (connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@liga_id", SqlDbType = SqlDbType.Int, Value = ligaId });
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Klub k = new Klub(reader.GetInt32(0))
                            {
                                Naziv = reader.GetString(reader.GetOrdinal("Naziv")),
                                LigaKlub = (Liga)LigaDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("ligaId"))),
                                Grad = reader.GetString(reader.GetOrdinal("Grad")),
                                Trener = reader.GetString(reader.GetOrdinal("Trener")),
                                KlubSport = (Sport)SportDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("sport_id"))),
                                LicencaPDF = reader.GetString(reader.GetOrdinal("licencapdf"))
                            };
                            listaKlubova.Add(k);
                        }
                    }
                    catch (Exception ee)
                    {

                        throw ee;
                    }

                }
            }
            return listaKlubova;
        }
    }
}