using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalScores.MasterEntities
{
    public abstract class DbManagerABS
    {
        public abstract void Insert();
        public abstract void DeleteSingle();
        public abstract void DeleteRange();
        public abstract List<object> GetAll();
        public abstract object GetSingle(int id);
        public abstract void Update(object carrier);
    }
}