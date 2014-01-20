using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace joetime
{
    public static class JoeUtils
    {
        public static void YearMonthCheck(int year, int month)
        {
            if (year < 1990 || year > 2025 || month < 1 || month > 12)
                throw new InvalidOperationException(String.Format("Date seems invalid (M/Y): {0}/{1}", month, year));
        }
    }
}
