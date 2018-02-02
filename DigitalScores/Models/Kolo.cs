using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalScores.Models
{
    public class Kolo
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

        #region
        private int sezonaId;
        #endregion
        public Liga Liga { get; set; }

        public Kolo()
        {

        }
    }
}