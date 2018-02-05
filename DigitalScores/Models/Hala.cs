namespace DigitalScores.Models
{
    public class Hala
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

        public Hala()
        {
        
        }
        public Hala(int id)
        {
            this.id = id;
        }
    }
}