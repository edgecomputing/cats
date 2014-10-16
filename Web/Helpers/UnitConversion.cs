using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Security;

namespace Cats.Helpers
{
    public static class UnitConversion
    {
         public static double Translate(this HtmlHelper html, double amount, string unit = "MT")
        {
             string currentUnit = unit;

             // Get current unit setting for the user.
            // NOTE: Since we might call this method from public views where we might not have a signed-in
            //       user, we must check for possible errors.
            try
            {
                var user = (UserIdentity)HttpContext.Current.User.Identity;
                currentUnit = user.Profile.PreferedWeightMeasurment;
            }
            catch (Exception)
            {
                currentUnit = unit;
            }

            // If the current unit is 'Metric Tone' then return the  value (the passed value)            
            if (currentUnit == "MT")
                return amount;

            // For the other unit (KG)  multiply by 1000


             return amount*1000;
        }

        
        public static double ToMetricTone(this HtmlHelper html,double amountInQuintal)
        {
            return amountInQuintal/10;
        }
        public static double ToQuintal(this HtmlHelper html, double amountInMT)
        {
            return amountInMT*10;
        }
    }
}