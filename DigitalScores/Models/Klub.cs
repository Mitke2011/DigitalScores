﻿using System.ComponentModel.DataAnnotations;

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
            set
            {
                this.id = value;
            }
        }
        public string Naziv { get; set; }
        public string Grad { get; set; }
        public string Trener { get; set; }
        [Display(Name = "Liga")]
        public Liga LigaKlub { get; set; }
        //LicencaPDF je lokacija PDF datoteke na sistemu 
        [Display(Name ="Licenca Kluba")]
        public string LicencaPDF { get; set; }

        #region DbLookup fields
        private int sportId;
        #endregion
        [Display(Name ="Sport")]
        public Sport KlubSport { get; set; }

        public Klub()
        {

        }
        public Klub(int id)
        {
            this.Id = id;
        }
    }
}