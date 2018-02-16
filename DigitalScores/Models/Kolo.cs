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

        public int Tekuce { get; set; }

        #region
        public int sezonaId { get; set; }
        public int ligaId { get; set;}
        #endregion
        public Liga KoloLiga { get; set; }

        public Sezona KoloSezona { get; set; }

        public Kolo()
        {

        }
        public Kolo(int id)
        {
            this.id = id;
        }
    }
}