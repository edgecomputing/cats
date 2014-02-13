using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Web.Administration.Helpers
{
    public class getGregorianDate
    {
        private DateTime gregDate = DateTime.Now.Date;
        private string[] _date;
        private  DateTime ConvertedDate = DateTime.Now;
       

        public DateTime ReturnGregorianDate(string am_date)
        {
            _date = am_date.Split('-');
            var ethDate = new EthiopianDate(int.Parse(_date[2]), EthiopianDate.GetMonthNo(_date[1]), int.Parse(_date[0]));
            ConvertedDate = ethDate.ToGregorianDate();
            return ConvertedDate;
        }
    }
}