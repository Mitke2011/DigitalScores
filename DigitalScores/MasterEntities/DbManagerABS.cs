using System.Collections.Generic;
using System.Data.SqlClient;
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


        public abstract void DeleteSingle(object carrier);
        public abstract void DeleteRange(List<object> list);
        public abstract List<object> GetAll();
        public abstract object GetSingle(int id);
        public abstract void Update(object carrier);
        public abstract void Insert(object carrier);

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