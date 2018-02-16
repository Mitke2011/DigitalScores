namespace DigitalScores.Models
{
    public class Klub
    {
        private int id;
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public string Naziv { get; set; }
        public string Grad { get; set; }
        public string Trener { get; set; }
        public Liga LigaKlub{get;set;}
        //LicencaPDF je lokacija PDF datoteke na sistemu 
        public string LicencaPDF { get; set; }

        #region DbLookup fields
        private int sportId;
        #endregion
        public Sport KlubSport { get; set; }

        public Klub()
        {

        }
        public Klub(int id)
        {
            this.Id = id;
        }
    }
}