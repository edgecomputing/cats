using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Helpers
{
    public static class DateHelpers
    {
        public static string ToCTSPreferedDateFormat(this DateTime date, string lang)
        {
            if (lang.ToLower() == "gc")
            {
                IFormatProvider provider = new CultureInfo("en-GB");
                return date.ToString("dd-MMM-yyyy", provider);

            }
            else
            {
                EthiopianDate ethiopianDate = new EthiopianDate(date);
                return ethiopianDate.ToLongDateString();
            }

        }
        public static string CTSPreferedDateLabel(this string label)
        {
            return string.Format("{0}( {1})", label, UserAccountHelper.UserCalendarPreference());
        }
      
        public static string FormatDateFromString(this HtmlHelper helper, string dateAsString)
        {
            DateTime theRealDate = Convert.ToDateTime((dateAsString));
            return ToCTSPreferedDateFormat(theRealDate, "EC");
        }

        public static Decimal ToPreferedWeightMeasurment(this Decimal quantity, string weightMeasurment)
        {

            if (weightMeasurment.Equals("qn"))
            {
                return quantity * 10;
            }
            return quantity;

        }
    }
}