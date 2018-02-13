using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public override void Update(object carrier)
        {
            throw new NotImplementedException();
        }

        public override void Insert(object carrier)
        {
            throw new NotImplementedException();
        }
    }
}