using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalScores.Models
{
    public class Klub
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
        public string Naziv { get; set; }
        public string Grad { get; set; }
        public string Trener { get; set; }

        //LicencaPDF je lokacija PDF datoteke na sistemu 
        public string LicencaPDF { get; set; }

        #region DbLookup fields
        private int sportId;
        #endregion
        public Sport Sport { get; set; }

        public Klub()
        {

        }
        #region hghgh
        #endregion
    }
}