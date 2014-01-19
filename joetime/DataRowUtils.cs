using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace joetime
{
    public static class DataRowUtils
    {
        public static Decimal DecimalAt(this DataRow row, int col)
        {
            return Decimal.Parse(row[col].ToString());
        }

        public static Decimal? DecimalOrNullAt(this DataRow row, int col)
        {
            if (row[col] == null || row[col].ToString() == "") return null;
            return row.DecimalAt(col);
        }

        
        
        public static Int32 IntAt(this DataRow row, int col)
        {
            return Int32.Parse(row[col].ToString());
        }

        public static Int32? IntOrNullAt(this DataRow row, int col, string nullEquivelant = "")
        {
            if (row[col] == null 
                || row[col].ToString() == "" 
                || row[col].ToString().ToLower() == nullEquivelant.ToLower()) return null;
            return row.IntAt(col);
        }



        public static Double DoubleAt(this DataRow row, int col)
        {
            return Double.Parse(row[col].ToString());
        }

        public static Double? DoubleOrNullAt(this DataRow row, int col)
        {
            if (row[col] == null || row[col].ToString() == "") return null;
            return row.DoubleAt(col);
        }
    }
}
