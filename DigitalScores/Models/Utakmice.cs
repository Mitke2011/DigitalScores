using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        private int koloID;
        private int klubId;
        private int sudija1Id;
        private int sudija2Id;
        private int delegatId;
        private int ligaId;
        private int halaId;
        private int sezonaId;
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

        public Utakmice()
        {

        }

    }
}