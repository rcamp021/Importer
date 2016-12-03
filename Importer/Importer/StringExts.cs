using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importer
{
   public static class StringExts
    {

        public static DateTime? SafeParseDate(this string that)
        {
            DateTime test;
            return DateTime.TryParse(that, out test) ? test : (DateTime?) null;
        }


        public static double? SafeParseDouble(this string that)
        {
            double test;
            return double.TryParse(that, NumberStyles.Float, CultureInfo.InvariantCulture, out test) ? test : (double?)null;
        }

       
        public static int? SafeParseInt(this string that)
        {
            int test;
            return int.TryParse(that, NumberStyles.Integer, CultureInfo.InvariantCulture, out test) ? test : (int?)null;
        }
    }
}
