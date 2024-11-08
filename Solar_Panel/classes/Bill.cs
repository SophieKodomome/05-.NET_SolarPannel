using System;
using System.Collections.Generic;

namespace house
{
    public class Bill
    {
        private int dayTimeHighestConsumption;
        private int highestConsumption;
        private int nightTimeConsumption;
        private int dayTimePowerNeed;
        private int batteryStorageLevel;

        public Bill() { }
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
                    throw new ArgumentException("DayTimeHighestConsumption must be a non-negative value.");
                }
            }
        }

        public int HighestConsumption
        {
            get { return highestConsumption; }
            set
            {
                if (value >= 0)
                {
                    highestConsumption = value;
                }
                else
                {
                    throw new ArgumentException("HighestConsumption must be a non-negative value.");
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
                    throw new ArgumentException("NightTimeConsumption must be a non-negative value.");
                }
            }
        }

        public int DayTimePowerNeed
        {
            get { return dayTimePowerNeed; }
            set
            {
                if (value >= 0)
                {
                    dayTimePowerNeed = value;
                }
                else
                {
                    throw new ArgumentException("DayTimePowerNeed must be a non-negative value.");
                }
            }
        }

        public int BatteryStorageLevel
        {
            get { return batteryStorageLevel; }
            set
            {
                if (value >= 0)
                {
                    batteryStorageLevel = value;
                }
                else
                {
                    throw new ArgumentException("BatteryStorageLevel must be a non-negative value.");
                }
            }
        }

        public Bill AddDayTimeHighestConsumption(int dayConsumption)
        {
            this.DayTimeHighestConsumption = dayConsumption;
            return this;
        }

        public Bill AddHighestConsumption(int highest)
        {
            this.HighestConsumption = highest;
            return this;
        }

        public Bill AddNightTimeConsumption(int nightConsumption)
        {
            this.NightTimeConsumption = nightConsumption;
            return this;
        }

        public Bill AddDayTimePowerNeed(int powerNeed)
        {
            this.DayTimePowerNeed = powerNeed;
            return this;
        }

        public Bill AddBatteryStorageLevel(int batteryLevel)
        {
            this.BatteryStorageLevel = batteryLevel;
            return this;
        }
    }
}
