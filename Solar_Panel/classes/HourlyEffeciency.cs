namespace efficiency
{
    public class HourlyEfficiency
    {
        public int IdResidence { get; set; }
        public String Name { get; set; }
        private int percentileEfficiency;
        public int startHour;
        public int endHour;
        public HourlyEfficiency() { }

        public int PercentileEfficiency
        {
            get { return percentileEfficiency; }
            set
            {
                if (value >= 0)
                {
                    percentileEfficiency = value;
                }
                else
                {
                    throw new ArgumentException("Percentile Efficiency must be a non-negative value.");
                }
            }
        }
        public int StartHour
        {
            get { return startHour; }
            set
            {
                if (value >= 0)
                {
                    startHour = value;
                }
                else
                {
                    throw new ArgumentException("Start Hour must be a non-negative value.");
                }
            }
        }
        public int EndHour
        {
            get { return endHour; }
            set
            {
                if (value >= 0)
                {
                    endHour = value;
                }
                else
                {
                    throw new ArgumentException("End Hour must be a non-negative value.");
                }
            }
        }
        public HourlyEfficiency addIdResidence(int i)
        {
            this.IdResidence = i;
            return this;
        }
        public HourlyEfficiency addName(string n)
        {
            this.Name = n;
            return this;
        }
        public HourlyEfficiency addPercentileEfficiency(int p)
        {
            this.PercentileEfficiency = p;
            return this;
        }
        public HourlyEfficiency addStartHour(int s)
        {
            this.StartHour = s;
            return this;
        }

        public HourlyEfficiency addEndHour(int e)
        {
            this.EndHour = e;
            return this;
        }

    }
}

