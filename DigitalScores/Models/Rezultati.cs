﻿namespace DigitalScores.Models
{
    public class Rezultati
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

        public int RezultatQ1D  { get; set; }
        public int RezultatQ2D { get; set; }
        public int RezultatQ3D { get; set; }
        public int RezultatQ4D { get; set; }
        public int RezultatOT1D { get; set; }
        public int RezultatOT2D { get; set; }
        public int RezultatH1D { get; set; }
        public int RezultatH2D { get; set; }
        public int RezultatQ1G { get; set; }
        public int RezultatQ2G { get; set; }
        public int RezultatQ3G { get; set; }
        public int RezultatQ4G { get; set; }
        public int RezultatOT1G { get; set; }
        public int RezultatOT2G { get; set; }
        public int RezultatH1G { get; set; }
        public int RezultatH2G { get; set; }
        public int RezultatKonacniD { get; set; }
        public int RezultatKonacniG { get; set; }

        #region DbLookup Fields
        public int UtakmicaId { get; set; }
        #endregion
        public Utakmice RezultatUtakmica { get; set; }

        public Rezultati()
        {

        }
        public Rezultati(int id)
        {
            this.id = id;
        }

    }
}