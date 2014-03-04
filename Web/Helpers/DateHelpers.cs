using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Security;

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

        //public static string ToCTSPreferedDateFormat(this Nullable<DateTime> date, string lang)
        //{
        //    if (date != null)
        //    {
        //        DateTime d = date;

        //        if (lang.ToLower() == "gc")
        //        {
        //            IFormatProvider provider = new CultureInfo("en-GB");
        //            return d.ToString("dd-MMM-yyyy", provider);
        //        }
        //        else
        //        {
        //            EthiopianDate ethiopianDate = new EthiopianDate(date);
        //            return ethiopianDate.ToLongDateString();
        //        }
        //    }
        //    else
        //    {
        //        return String.Empty;
        //    }
        //}

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
        
        public static decimal ToPreferedWeightUnit(this decimal amount, string unit = "MT")
        {
            string currentUnit;

            // Get current unit setting for the user.
            // NOTE: Since we might call this method from public views where we might not have a signed-in
            //       user, we must check for possible errors.
            try
            {
                
                var user = UserAccountHelper.GetCurrentUser();                
                currentUnit = user.PreferedWeightMeasurment;
            }
            catch (Exception)
            {
                currentUnit = unit;
            }

            // If the current unit is 'Metric Tone' then return the  value (the passed value)            
            if (currentUnit.ToUpper().Trim() == "MT")
                return amount;

            // For the other unit (quintal)  multiply by 10

            return amount * 10;
        }

        public static decimal GetPreferedRation(this decimal amount)
        {
            string currentUnit;
            try
            {
                var user = UserAccountHelper.GetCurrentUser();
                currentUnit = user.PreferedWeightMeasurment;
            }
            catch (Exception)
            {

                return amount;
            }


            if (currentUnit.ToUpper().Trim() == "MT")
                return amount / 1000;
            return amount/100;
        }
    }
}