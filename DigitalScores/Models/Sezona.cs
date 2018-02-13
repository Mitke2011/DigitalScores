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

        #region DbLookup fields
        private int ligaId;
        #endregion
        public Liga Liga { get; set; }

        public Sezona()
        {

        }
        public Sezona(int id)
        {
            this.Id = id;
        }
    }
}