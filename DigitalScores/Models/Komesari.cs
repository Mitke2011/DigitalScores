namespace DigitalScores.Models
{
    public class Komesari
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

        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
    
        #region DBLookUp Fields
        public int LigaId { get; set; }
        #endregion

        public Liga Liga { get; set; }

        public Komesari()
        {
                
        }
        public Komesari(int id)
        {
            this.id = id;
        }

    }
}