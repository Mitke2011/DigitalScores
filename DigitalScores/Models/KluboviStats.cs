namespace DigitalScores.Models
{
    public class KluboviStats
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

        public Klub KlubForStat { get; set; }
        public int PrimljeniPoeni { get; set; }
        public int PostignutiPoeni { get; set; }
        public int OdigranoUtakmica { get; set; }
        public int BodoviPobeda { get; set; }
        public int BodoviPoraz { get; set; }
        public int UkupnoBodova { get; set; }

        #region DbLookup Fields
        public int sezonaId;
        #endregion
        public Sezona SezonaStats { get; set; }

        public KluboviStats()
        {

        }

        public KluboviStats(int id)
        {
            this.id = id;
        }

    }
}