using System;
using System.Collections.Generic; // Use List<T> instead of ArrayList
using material; // Assuming "materiel" is the correct namespace for Device

namespace house
{
    public class Residence
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private List<Device> listDevices;
        private int dayTimeHighestConsumption;
        private int highestConsumption;
        private int nightTimeConsumption;
        public Residence() { }
        public List<Device> Devices
        {
            get { return listDevices; }
            set { listDevices = value; }
        }

        public int HighestConsumption
        {
            get{ return highestConsumption; }
            set
            {
                if(value>=0)
                {
                    highestConsumption = value;
                }
                else
                {
                    throw new ArgumentException("Day Consumption hour must be a non-negative value.");
                }
            }
        }
        public int DayTimeHighestConsumption
        {
            get { return dayTimeHighestConsumption; }
            set
            {
                if (value >= 0)
                {
                    dayTimeHighestConsumption = value;
                }
                else
                {
                    throw new ArgumentException("Day Consumption hour must be a non-negative value.");
                }
            }
        }

        public int NightTimeConsumption
        {
            get { return nightTimeConsumption; }
            set
            {
                if (value >= 0)
                {
                    nightTimeConsumption = value;
                }
                else
                {
                    throw new ArgumentException("Night Consumption must be a non-negative value.");
                }
            }
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
            this.listDevices = l;
            return this;
        }

        //public Residence
        public Residence addDayTimeHighestConsumption(int d)
        {
            this.dayTimeHighestConsumption=d;
            return this;
        }
        public Residence addNightTimeConsumption(int n)
        {
            this.nightTimeConsumption=n;
            return this;
        }

        public Residence addHighestConsumption(int h)
        {
            this.highestConsumption=h;
            return this;
        }
    }
}
