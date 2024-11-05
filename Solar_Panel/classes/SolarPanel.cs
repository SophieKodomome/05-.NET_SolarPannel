namespace materiel
{
    public class SolarPanel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        private int pricePerWatt;
        public SolarPanel() { }
        public int PricePerWatt
        {
            get { return pricePerWatt; }
            set
            {
                if (value >= 0)
                {
                    pricePerWatt = value;
                }
                else
                {
                    throw new ArgumentException("Price must be a non-negative value.");
                }
            }
        }
        public SolarPanel addId(int i)
        {
            this.Id = i;
            return this;
        }
        public SolarPanel addName(string n)
        {
            this.Name = n;
            return this;
        }
        public SolarPanel addPower(int p)
        {
            this.PricePerWatt = p;
            return this;
        }

    }
}

