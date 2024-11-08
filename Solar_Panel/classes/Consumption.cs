using System;

namespace material
{
    public class consumption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private int pricePerWatt;
        private int aPlat;
        public consumption() { }
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
        
        public int APlat
        {
            get { return aPlat; }
            set
            {
                if (value >= 0)
                {
                    aPlat = value;
                }
                else
                {
                    throw new ArgumentException("A plat must be a non-negative value.");
                }
            }
        }

    }
}

