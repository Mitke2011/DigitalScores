namespace DigitalScores.Models
{
    public class Users
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
    
        public string Username { get; set; }
        public string Password { get; set; }
        public int Privilege { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Grad { get; set; }
        public string Telefon { get; set; }
        public string Region { get; set; }

        public Users()
        {

        }

    }
}