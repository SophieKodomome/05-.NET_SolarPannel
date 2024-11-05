using System;
using System.Collections.Generic; // Use List<T> instead of ArrayList
using material; // Assuming "materiel" is the correct namespace for Device

namespace house
{
    public class Residence
    {
        public int Id { get; set;}
        public string Name { get; set; }
        private List<Device> listDevices;
        public Residence() {}
        public List<Device> Devices
        {
            get{return listDevices;}
            set{listDevices=value;}
        }

        public Residence addId(int i)
        {
            this.Id = i;
            return this;
        }

        public Residence addName(string n)
        {
            this.Name = n;
            return this;
        }

        public Residence addDevices(List<Device> l)
        {
            this.listDevices=l;
            return this;
        }
    }
}
