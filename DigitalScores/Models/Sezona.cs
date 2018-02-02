namespace DigitalScores.Models
{
    public class Sezona
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

        public Sezona()
        {

        }
    }
}