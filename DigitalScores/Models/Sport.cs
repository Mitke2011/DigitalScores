using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalScores.Models
{
    public class Sport
    {
        private int id;
        public int Id
        {
            get
            {
                return this.id;
            }
            set { }
        }

        public string Naziv { get; set; }        

        public Sport()
        {

        }
    }
}