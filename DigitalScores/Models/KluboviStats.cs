using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalScores.Models
{
    public class KluboviStats
    {
        private int id;
        public int Id
        {
            get
            {
                return this.id;
            }
            private set
            {
                this.id = value;
            }
        }

        public int PrimljeniPoeni { get; set; }
        public int PostignutiPoeni { get; set; }
        public int OdigranoUtakmica { get; set; }
        public int BodoviPobeda { get; set; }
        public int BodoviPoraz { get; set; }
        public int UkupnoBodova { get; set; }

        #region DbLookup Fields
        public int sportId;
        public int sezonaId;
        #endregion
        public Sport sport { get; set; }
        public Sezona sezona { get; set; }

        public KluboviStats()
        {

        }

    }
}