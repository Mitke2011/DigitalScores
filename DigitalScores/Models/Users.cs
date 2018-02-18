using System.ComponentModel.DataAnnotations;

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
        [Display(Name ="Nivo Privilegija")]
        public Privilege UserPrivilege { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Grad { get; set; }
        public string Telefon { get; set; }
        public string Region { get; set; }
        public string ImeiPrezime { get; set; }

        public Users()
        {

        }

        public Users(int id)
        {
            this.Id = id;
        }

        public Users(string username)
        {
            this.Username = username;
        }
                
    }

    public enum Privilege
    {
        SuperAdmin,
        Admin,
        Delegate,
        Invalid
    }
}