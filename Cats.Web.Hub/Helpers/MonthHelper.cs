using System.Collections.Generic;
using System.Linq;

namespace Cats.Web.Hub.Helpers
{
    public class MonthHelper
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
        public static Month GetMonth(List<int?> months )
        {
            var allmonths = new Month();
            if (months != null)
            {
                foreach (var month in months)
                {
                    var monthList = GetMonthList();
                    allmonths = monthList.Find(t => t.Id == month);
                }
                return allmonths;
            }
            return null;
        }

    }

    public class Month
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Month()
        {
        }

        public Month(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}