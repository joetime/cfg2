using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfglib
{
    public class MonthYear
    {
        public int Month { get; set; }
        public int Year { get; set; }

        public MonthYear(DateTime date)
        {
            Month = date.Month;
            Year = date.Year;
        }

        public MonthYear(string datestring)
        {
            DateTime date = DateTime.Parse(datestring);
            Month = date.Month;
            Year = date.Year;
        }

        public override string ToString()
        {
            return Year.ToString() + "-" + Month.ToString("00");
        }
    }
}
