using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.Helpers
{
    public class RequestHelper
    {
        public static List<Month> GetMonthList()
        {
            var months = new List<Month>();
            months.Add(new Month(0,"--- Month ---"));
            months.Add(new Month(1, "January"));
            months.Add(new Month(2, "February"));
            months.Add(new Month(3, "March"));
            months.Add(new Month(4, "April"));
            months.Add(new Month(5, "May"));
            months.Add(new Month(6, "June"));
            months.Add(new Month(7, "July"));
            months.Add(new Month(8, "August"));
            months.Add(new Month(9, "September"));
            months.Add(new Month(10, "October"));
            months.Add(new Month(11, "November"));
            months.Add(new Month(12, "December"));
            return months;
        }
        public static string MonthName(int id)
        {
            var monthList = GetMonthList();
           return  monthList.Find(t => t.Id == id).Name;
        }

    }
}