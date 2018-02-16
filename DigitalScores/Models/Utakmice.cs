namespace DigitalScores.Models
{
    public class Utakmice
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

        //koristice se samo za db lookup
        #region Db lookup fields
        public int koloID { get; set; }
        public int klubDomacinId { get; set; }
        public int klubGostId { get; set; }
        public int sudija1Id { get; set; }
        public int sudija2Id { get; set; }
        public int delegatId { get; set; }
        public int ligaId { get; set; }
        public int halaId { get; set; }
        public int sezonaId { get; set; }
        #endregion

        public Kolo KoloUtakmice { get; set; }
        public Klub KlubDomacin { get; set; }
        public Klub KlubGost { get; set; }
        public Sudija Sudija1 { get; set; }
        public Sudija Sudija2 { get; set; }
        public Users DelegatUtakmice { get; set; }
        public Liga LigaUtakmice { get; set; }
        public Hala SportskaHala { get; set; }
        public Sezona Sezona { get; set; }
        public string NapomenaDelegata { get; set; }
        public string KlDomacin { get; set; }
        public string KlGost { get; set; }

        public Utakmice()
        {

        }
        public Utakmice(int id)
        {
            this.id = id;
        }

    }
}