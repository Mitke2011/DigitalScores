using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalScores.Models
{
    public class Region
    {
        private int id;
        private string naziv;

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
        public Region()
        {

        }
        public Region(int id)
        {
            this.id = id;
        }
    }
}