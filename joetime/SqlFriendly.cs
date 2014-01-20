using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace joetime
{
    public static class SqlFriendly
    {
        public static string ToStringSql(this string val)
        {
            // double apostrophe
            return val.Trim().Replace("'", "''");
        }

        public static string ToStringSqlNullable(this decimal? val)
        {
            if (!val.HasValue) return "NULL";
            else return val.Value.ToString();
        }

        public static string ToStringSqlNullable(this double? val)
        {
            if (!val.HasValue) return "NULL";
            else return val.Value.ToString();
        }

        public static string ToStringSqlNullable(this int? val)
        {
            if (!val.HasValue) return "NULL";
            else return val.Value.ToString();
        }

    
    }
}
