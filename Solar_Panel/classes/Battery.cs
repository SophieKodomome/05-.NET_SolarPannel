using System;

namespace material
{
    public class Battery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private int pricePerWatt;
        private double aPlat;
        public Battery() { }
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
        
        public double APlat
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
        public Battery addId(int i)
        {
            this.Id = i;
            return this;
        }
        public Battery addName(string n)
        {
            this.Name = n;
            return this;
        }
        public Battery addPricePerWatt(int p)
        {
            this.PricePerWatt = p;
            return this;
        }
        public Battery addAPlat(double a)
        {
            this.aPlat = a;
            return this;
        }

    }
}

