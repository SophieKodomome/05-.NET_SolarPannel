using System;
using System.Collections.Generic;

namespace efficiency
{
    public class Semester
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public List<HourlyEfficiency> Hours{get;set;}
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Semester() { }
        public Semester addId(int i)
        {
            this.Id = i;
            return this;
        }
        public Semester addName(string n)
        {
            this.Name = n;
            return this;
        }
        public Semester addStartDate(DateTime s)
        {
            this.StartDate = s;
            return this;
        }

        public Semester addEndDate(DateTime e)
        {
            this.EndDate = e;
            return this;
        }
        public Semester addHours(List<HourlyEfficiency> h)
        {
            this.Hours = h;
            return this;
        }

    }
}

