﻿namespace DigitalScores.Models
{
    public class Liga
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

        public Liga()
        {

        }
    }
}