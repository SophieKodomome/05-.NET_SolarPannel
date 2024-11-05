namespace materiel
{
    public class Device
    {
        public int IdResidence { get; set; }
        public String Name { get; set; }
        private int power;
        public int startHour;
        public int endHour;

        public Device() { }

        public int Power
        {
            get { return power; }
            set
            {
                if (value >= 0)
                {
                    power = value;
                }
                else
                {
                    throw new ArgumentException("Power must be a non-negative value.");
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
        public Device addIdResidence(int i)
        {
            this.IdResidence = i;
            return this;
        }
        public Device addName(string n)
        {
            this.Name = n;
            return this;
        }
        public Device addPower(int p)
        {
            this.Power = p;
            return this;
        }
        public Device addStartHour(int s)
        {
            this.StartHour = s;
            return this;
        }

        public Device addEndHour(int e)
        {
            this.EndHour = e;
            return this;
        }

    }
}

