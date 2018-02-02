namespace DigitalScores.Models
{
    public class Sudija
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
        public string Grad { get; set; }

        public Sudija()
        {

        }


    }
}