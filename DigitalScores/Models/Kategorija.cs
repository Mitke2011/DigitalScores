namespace DigitalScores.Models
{
    public class Kategorija
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public Kategorija()
        {

        }

        public Kategorija(int id)
        {
            Id = id;
        }
    }
}