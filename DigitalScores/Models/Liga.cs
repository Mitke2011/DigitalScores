using System.ComponentModel.DataAnnotations;

namespace DigitalScores.Models
{
    public class Liga
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

        [Display(Name ="Naziv Lige")]
        public string Naziv { get; set; }
        public int kategorijaId { get; set; }

        public Liga()
        {

        }
        public Liga(int id)
        {
            this.Id = id;
        }

        [Display(Name ="Kategorija")]
        public Kategorija LigaKategorija { get; set; }
    }
}