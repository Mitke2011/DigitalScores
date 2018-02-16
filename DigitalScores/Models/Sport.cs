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
            private set { this.id = value; }
        }

        public string Naziv { get; set; }        

        public Sport()
        {

        }
        public Sport(int id)
        {
            this.id = id;
        }
    }
}