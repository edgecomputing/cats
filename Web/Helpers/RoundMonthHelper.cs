using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Helpers
{
    public class RoundMonthHelper
    {
        public static string GetMonth(int round)
        {
            string month;

            switch (round)
            {
                case 1:
                    month = round.ToString();
                    break;
                case 2:
                    month = round.ToString();
                    break;
                case 3:
                    month = round.ToString();
                    break;
                case 4:
                    month = round.ToString();
                    break;
                case 5:
                    month = round.ToString();
                    break;
                case 6:
                    month = round.ToString();
                    break;
                case 7:
                    month = round.ToString();
                    break;
                case 8:
                    month = round.ToString();
                    break;
                case 9:
                    month = "Ginbot";
                    break;
                default:
                    month = "Unknown";
                    break;
            }
            return month;
        }
    }
}