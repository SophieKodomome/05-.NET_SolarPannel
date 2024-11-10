using System;
using material;

namespace house
{
    public class Bill
    {
        private int dayTimeHighestConsumption;
        private int highestConsumption;
        private int nightTimeConsumption;
        private int dayTimePowerNeed;
        public double batteryStorageLevel;
        public int solarPanelPrice;
        public double batteryPrice;

        private List<int> hourlyConsumption=new List<int>();
        private List<int> resoldRest=new List<int>();

        private List<int> priceResoldRest=new List<int>();
        public double InstallationFee{get;set;}

        public double TotalPrice{get;set;}
        public double MonthlyPrice{get;set;}

        public SolarPanel SolarPanel { get; set; }
        public Battery Battery { get; set; }
        public Bill() { }

        public List<int> HourlyConsumption
        {
            get{return hourlyConsumption;}
            set{
                hourlyConsumption=value;
            }
        }

        public List<int> Resoldrest
        {
            get{return resoldRest;}
            set{
                resoldRest=value;
            }
        }

        public List<int> PriceResoldrest
        {
            get{return priceResoldRest;}
            set{
                priceResoldRest=value;
            }
        }
        public double BatteryStorageLevel
        {
            get 
            { 
                return Battery != null ? nightTimeConsumption * Battery.APlat : 0.0;
            }
            private set
            {
                if (Battery != null)
                {
                    batteryStorageLevel = nightTimeConsumption * Battery.APlat;
                }
            }
        }

        public double BatteryPrice
        {
            get 
            { 
                return Battery != null ? BatteryStorageLevel * Battery.PricePerWatt : 0.0;
            }
            private set
            {
                if (Battery != null)
                {
                    batteryPrice = BatteryStorageLevel * Battery.PricePerWatt;
                }
            }
        }

        public int SolarPanelPrice
        {
            get
            {
                return SolarPanel != null ? DayTimePowerNeed * SolarPanel.PricePerWatt : 0;
            }
            private set
            {
                if (SolarPanel != null)
                {
                    solarPanelPrice = DayTimePowerNeed * SolarPanel.PricePerWatt;
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

        public Bill AddBatteryStorageLevel(double batteryLevel)
        {
            this.batteryStorageLevel = batteryLevel;
            return this;
        }

        public Bill AddSolarPanel(SolarPanel s)
        {
            this.SolarPanel = s;
            return this;
        }

        public Bill AddBattery(Battery b)
        {
            this.Battery = b;
            return this;
        }

        public Bill SetTotalPrice()
        {
            this.TotalPrice=this.SolarPanelPrice+this.BatteryPrice;
            return this;
        }
        public Bill SetMonthlyPrice()
        {
            this.TotalPrice=this.SolarPanelPrice+this.BatteryPrice+this.InstallationFee;
            this.MonthlyPrice=this.TotalPrice/12;
            return this;
        }

        public Bill SetInstallationFee(){
            this.InstallationFee=this.SolarPanelPrice+this.BatteryPrice;
            this.InstallationFee=this.InstallationFee*0.2;

            return this;
        }

        public Bill SetPriceResoldRest()
        {
            int price=10;
            foreach (var singleRest in resoldRest)
            {
                Console.WriteLine("Rest"+singleRest);
                this.priceResoldRest.Add(singleRest*price);
                Console.WriteLine("Price"+singleRest*price);
            }
            return this;
        }
    }
}
