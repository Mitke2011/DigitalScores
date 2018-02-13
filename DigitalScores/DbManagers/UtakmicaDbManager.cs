using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using DigitalScores.MasterEntities;
using DigitalScores.Models;

namespace DigitalScores.DbManagers
{
    public class UtakmicaDbManager : DbManagerABS
    {

        static UtakmicaDbManager instance;
        public static UtakmicaDbManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new UtakmicaDbManager();
                }
                return instance;
            }
        }
        public UtakmicaDbManager() : base()
        {

        }

        public UtakmicaDbManager(string connectionString) : base(connectionString)
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

       /* public  List<DigitalScores.Models.Utakmice> GetGames()
        {
            List<DigitalScores.Models.Utakmice> listaUtakmica = new List<DigitalScores.Models.Utakmice>();
            string sql = "select kolo.Id as Kolo, u.Id as Id, kd.Naziv as KlubDomacin, kg.Naziv as KlubGost from Utakmice u join Klub kd on (u.Klub_Domacin_Id = kd.Id)" + 
                "join Klub kg on (u.Klub_Gost_Id = kg.Id)" +
                "join Kolo kolo on (u.Kolo_Id = kolo.Id)"+
                "where kolo.Tekuce = 1";

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
                            KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("")))
                            Utakmice u = new Utakmice()
                            {
                                
                                KoloUtakmice = reader.GetInt32(reader.GetOrdinal("Kolo")),
                                KlubDomacin= reader.GetString(reader.GetOrdinal("KlubDomacin")),
                                KlubGost = reader.GetString(reader.GetOrdinal("KlubGost")),
                            };
                            listaUtakmica.Add(u);
                        }
                    }
                    catch (Exception ee)
                    {

                        throw ee;
                    }

                }
            }

            return listaUtakmica;
        }*/

        public override object GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public override void Insert(object carrier)
        {
            throw new NotImplementedException();
        }

        public override void Update(object carrier)
        {
            throw new NotImplementedException();
        }

        public override List<object> GetAll()
        {
            throw new NotImplementedException();
        }

     public  List<DigitalScores.Models.Utakmice> GetGames()
         {
             List<DigitalScores.Models.Utakmice> listaUtakmica = new List<DigitalScores.Models.Utakmice>();
             string sql = "select * from Utakmice u" +
                 " join Kolo kolo on (u.Kolo_Id = kolo.Id)" +
                 " where kolo.Tekuce = 1";

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
                             KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_domacin_id")));

                            Utakmice u = new Utakmice()
                             {
                                 KlubDomacin = (Klub)KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_domacin_id"))),
                                 KlubGost = (Klub)KlubDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Klub_gost_id"))),
                                 KoloUtakmice = (Kolo)KoloDbManager.Current.GetSingle(reader.GetInt32(reader.GetOrdinal("Kolo_Id")))
                                 //KoloUtakmice = reader.GetInt32(reader.GetOrdinal("Kolo")),
                                 //KlubDomacin = reader.GetString(reader.GetOrdinal("KlubDomacin")),
                                 //KlubGost = reader.GetString(reader.GetOrdinal("KlubGost")),
                             };
                             listaUtakmica.Add(u);
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