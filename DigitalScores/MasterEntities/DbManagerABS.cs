using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;

namespace DigitalScores.MasterEntities
{
    public abstract class DbManagerABS
    {
        public SqlConnection connection;
        public SqlCommand command;
                
        private string connectionString;

        protected string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }


        public abstract void Insert();
        public abstract void DeleteSingle();
        public abstract void DeleteRange();
        public abstract List<object> GetAll();
        public abstract object GetSingle(int id);
        public abstract void Update(object carrier);

        public DbManagerABS()
        {
            var element = ConfigurationManager.ConnectionStrings["ServerConnection"];

            if (element != null)
            {
                this.ConnectionString = element.ConnectionString;
            }
            else
            {
                this.ConnectionString = ConfigurationManager.ConnectionStrings["LocalConnection"].ConnectionString;
            }
        }

        public DbManagerABS(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
    }
}