
using System;
using System.Globalization;

namespace Cats.Web.Hub.Infrastructure
{
    public class EthiopianDate
    {
        public int Year;
        public int Month;
        public int Day;
        const double jdEpoch = 1724220.5;
        private DateTime _GregDate;

        /// <summary>
        /// Gets the now.
        /// </summary>
        public static EthiopianDate Now
        {
            get
            {
                return new EthiopianDate();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EthiopianDate"/> class.
        /// </summary>
        public EthiopianDate()
        {
            //Get today's date in the class
            Year = DateTime.Now.Year;
            Month = DateTime.Now.Month;
            Day = DateTime.Now.Day;

            GregorianToEthiopian(ref Year, ref Month, ref Day);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EthiopianDate"/> class.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        public EthiopianDate(DateTime dateTime)
        {
            //Get today's date in the class
            Year = dateTime.Year;
            Month = dateTime.Month;
            Day = dateTime.Day;
            _GregDate = dateTime;
            GregToEthiopian();
        }

        /// <summary>
        /// Gregorians to julian date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static double GregorianToJulianDate(DateTime date)
        {
            //return date.ToOADate() + 2415018.5;

           
            double year, month, day;
            year = date.Year;
            month = date.Month;
            day = date.Day;

            if (year < 0) { year++; } // No year zero
            // Jean Meeus algorithm, "Astronomical Algorithms", 1991
            if (month < 3) {
            month += 12;
            year--;
            }
            var a = Math.Floor(year / 100);
            var b = 2 - a + Math.Floor(a / 4);
            var value = Math.Floor(365.25 * (year + 4716)) + Math.Floor(30.6001 * (month + 1)) + day + b - 1524.5;
            return value;
        }

        /// <summary>
        /// Ethiopians to julian.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <returns></returns>
        public static double EthiopianToJulian(int year, int month, int day)
        {
            if (year < 0) { year++; } // No year zero
            var value = day + ((month - 1)*30) + ((year - 1)*365) + (Math.Floor((double) (year/4))) + jdEpoch - 1;
		    return value;
        }

        /// <summary>
        /// Gregs to ethiopian.
        /// </summary>
        private void GregToEthiopian()
        {
            double jd = GregorianToJulianDate(_GregDate);
            var c = Math.Floor(jd) + 0.5 - jdEpoch;
            var year = Math.Floor((c - Math.Floor((c + 366) / 1461)) / 365) + 1;
            if (year <= 0) { year--; } // No year zero

            c = Math.Floor(jd) + 0.5 -  EthiopianToJulian((int)year, 1, 1);
            var month = Math.Floor(c / 30) + 1;
            var day = (c - ((month - 1) * 30)) + 1;

            this.Day =(int) day;
            this.Month = (int) month;
            this.Year = (int) year;
            //return this.newDate(year, month, day);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EthiopianDate"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        public EthiopianDate(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        /// <summary>
        /// Determines whether [is leap year].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is leap year]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsLeapYear()
        {
            return (Year%4 == 3 || Year % 4 == -1);
        }

        /// <summary>
        /// Receives format in Ethiopian DD/MM/YYYY
        /// </summary>
        /// <param name="ddMMyyyy"></param>
        public EthiopianDate(string ddMMyyyy)
        {
            string[] dateArr = ddMMyyyy.Split('/');
            Year = int.Parse(dateArr[0]);
            Month = int.Parse(dateArr[1]);
            Day = int.Parse(dateArr[2]);
        }

        /// <summary>
        /// Toes the date string.
        /// </summary>
        /// <returns></returns>
        public string ToDateString()
        {
            string date = Day + "/" + Month + "/" + Year;
            return date;
        }

        /// <summary>
        /// Toes the long date string.
        /// </summary>
        /// <returns></returns>
        public string ToLongDateString()
        {
            string date = string.Format("{1} {0} - {3} ({2})", Day, GetMonthName(), GetMonthNameEN() , Year);
            return date;
        }

        /// <summary>
        /// Input is gregorian calendar.
        /// </summary>
        /// <param name="gregorianYear"></param>
        /// <param name="gregorianMonth"></param>
        /// <param name="gregorianDay"></param>
        public void SetDate(int gregorianYear, int gregorianMonth, int gregorianDay)
        {
            Year = gregorianYear;
            Month = gregorianMonth;
            Day = gregorianDay;
            GregorianToEthiopian(ref Year, ref Month, ref Day);
        }


        /// <summary>
        /// Gets the name of the month.
        /// </summary>
        /// <returns></returns>
        public string GetMonthName()
        {
            switch (Month)
            {
                case 1:
                    return "መስከረም";
                case 2:
                    return "ጥቅምት";
                case 3:
                    return "ህዳር";
                case 4:
                    return "ታህሳስ";
                case 5:
                    return "ጥር";
                case 6:
                    return "የካቲት";
                case 7:
                    return "መጋቢት";
                case 8:
                    return "ሚያዚያ";
                case 9:
                    return "ግንቦት";
                case 10:
                    return "ሰኔ";
                case 11:
                    return "ሐምሌ";
                case 12:
                    return "ነሃሴ";
                case 13:
                    return "ጳጉሜ";
            }
            return null;
        }

        public string GetMonthNameEN()
        {
            switch (Month)
            {
                case 1:
                    return "Meskerem";
                case 2:
                    return "Tikimt";
                case 3:
                    return "Hidar";
                case 4:
                    return "Tahsas";
                case 5:
                    return "Tirr";
                case 6:
                    return "Yekatit";
                case 7:
                    return "Megabit";
                case 8:
                    return "Miazia";
                case 9:
                    return "Ginbot";
                case 10:
                    return "Senie";
                case 11:
                    return "Hamlie";
                case 12:
                    return "Nehasie";
                case 13:
                    return "Pagume";
            }
            return null;
        }


        public static string GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "መስከረም";
                case 2:
                    return "ጥቅምት";
                case 3:
                    return "ህዳር";
                case 4:
                    return "ታህሳስ";
                case 5:
                    return "ጥር";
                case 6:
                    return "የካቲት";
                case 7:
                    return "መጋቢት";
                case 8:
                    return "ሚያዚያ";
                case 9:
                    return "ግንቦት";
                case 10:
                    return "ሰኔ";
                case 11:
                    return "ሐምሌ";
                case 12:
                    return "ነሃሴ";
                case 13:
                    return "ጳጉሜ";
            }
            return null;
        }


        /// <summary>
        /// Just get the day in the Ethiopian Calandar.
        /// </summary>
        /// <param name="gregorianYear"></param>
        /// <param name="gregorianMonth"></param>
        /// <param name="gregorianDay"></param>
        /// <returns></returns>
        private static int GetEthiopianDay(int gregorianYear, int gregorianMonth, int gregorianDay)
        {
            int ethDay = gregorianDay;
            GregorianToEthiopian(ref gregorianYear, ref gregorianMonth, ref ethDay);
            return ethDay;
        }

        /// <summary>
        /// Returns Ethiopian year as an output.
        /// Takes the gregorian date as input.
        /// </summary>
        /// <param name="gregorianYear"></param>
        /// <param name="gregorianMonth"></param>
        /// <returns></returns>
        private static int GetEthiopianYear(int gregorianYear, int gregorianMonth)
        {
            return (gregorianMonth >= 9 && gregorianMonth <= 12) ? gregorianYear - 7 : gregorianYear - 8;
        }


        /// <summary>
        /// Checks the leap year.
        /// </summary>
        /// <returns></returns>
        public bool CheckLeapYear()
        {
            if ((Year % 4) == 0 && (Year % 100) != 0)
                return true;

            return false;
        }

        /// <summary>
        /// Gets the start of fiscal year.
        /// </summary>
        public EthiopianDate StartOfFiscalYear
        {
            get { return new EthiopianDate(((Month > 10) ? Year : Year - 1), 11, 1); }
        }

        /// <summary>
        /// Gets the end of fiscal year.
        /// </summary>
        public EthiopianDate EndOfFiscalYear
        {
            get { return new EthiopianDate((Month > 10) ? Year + 1 : Year, 10, 30); }
        }

        /// <summary>
        /// Gets the fiscal year.
        /// </summary>
        public int FiscalYear
        {
            get { return EndOfFiscalYear.Year; }
        }

        /// <summary>
        /// Toes the gregorian date.
        /// </summary>
        /// <returns></returns>
        public DateTime ToGregorianDate()
        {
            return EthiopianToGregorian(this.ToDateString());
        }

        /// <summary>
        /// Checks the leap year.
        /// </summary>
        /// <param name="gregorianYear">The gregorian year.</param>
        /// <param name="gregorianMonth">The gregorian month.</param>
        /// <returns></returns>
        protected static bool CheckLeapYear(int gregorianYear, int gregorianMonth)
        {
            int yr = GetEthiopianYear(gregorianYear, gregorianMonth);

            if ((yr % 4) == 0 && (yr % 100) != 0)
                return true;

            return false;
        }


        /// <summary>
        /// Ethiopians to fixed.
        /// </summary>
        /// <param name="ethiopianYear">The ethiopian year.</param>
        /// <param name="ethiopianMonth">The ethiopian month.</param>
        /// <param name="ethiopianDay">The ethiopian day.</param>
        /// <returns></returns>
        private static int EthiopianToFixed(int ethiopianYear, int ethiopianMonth, int ethiopianDay)
        {
            return ((((0xaeb + (0x16d * (ethiopianYear - 1))) + ((int)Math.Floor((double)(((double)ethiopianYear) / 4.0)))) + (30 * (ethiopianMonth - 1))) + ethiopianDay);
        }

        /// <summary>
        /// Fixes to ethiopian.
        /// </summary>
        /// <param name="fixedDate">The fixed date.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        private static void FixedToEthiopian(int fixedDate, ref int year, ref int month, ref int day)
        {
            year = (int)Math.Floor((double)(((double)((4 * (fixedDate - 0xaec)) + 0x5b7)) / 1461.0));
            month = ((int)Math.Floor((double)(((double)(fixedDate - EthiopianToFixed(year, 1, 1))) / 30.0))) + 1;
            day = (fixedDate + 1) - EthiopianToFixed(year, month, 1);
        }

        /// <summary>
        /// Gregorians to fixed.
        /// </summary>
        /// <param name="gregorianYear">The gregorian year.</param>
        /// <param name="gregorianMonth">The gregorian month.</param>
        /// <param name="gregorianDay">The gregorian day.</param>
        /// <returns></returns>
        private static int GregorianToFixed(int gregorianYear, int gregorianMonth, int gregorianDay)
        {
            return (((((((0x16d * (gregorianYear - 1)) + ((int)Math.Floor((double)(((double)(gregorianYear - 1)) / 4.0)))) - ((int)Math.Floor((double)(((double)(gregorianYear - 1)) / 100.0)))) + ((int)Math.Floor((double)(((double)(gregorianYear - 1)) / 400.0)))) + ((int)Math.Floor((double)(((double)((0x16f * gregorianMonth) - 0x16a)) / 12.0)))) + ((gregorianMonth <= 2) ? 0 : (CheckLeapYear(gregorianYear, gregorianMonth) ? -1 : -2))) + gregorianDay);
        }


        /// <summary>
        /// Ethiopians the day of week.
        /// </summary>
        /// <param name="ethiopianYear">The ethiopian year.</param>
        /// <param name="ethiopianMonth">The ethiopian month.</param>
        /// <param name="ethiopianDay">The ethiopian day.</param>
        /// <returns></returns>
        public DayOfWeek EthiopianDayOfWeek(int ethiopianYear, int ethiopianMonth, int ethiopianDay)
        {
            //The input is in the ethiopian calendar.

            int i = (Math.Abs(EthiopianToFixed(ethiopianYear, ethiopianMonth, 1)) % 7);
            switch (i)
            {
                case 0:
                    return DayOfWeek.Sunday;

                case 1:
                    return DayOfWeek.Monday;

                case 2:
                    return DayOfWeek.Tuesday;

                case 3:
                    return DayOfWeek.Wednesday;

                case 4:
                    return DayOfWeek.Thursday;

                case 5:
                    return DayOfWeek.Friday;

                case 6:
                    return DayOfWeek.Saturday;
            }
            return DayOfWeek.Sunday;
        }


        /// <summary>
        /// Gregorians to ethiopian.
        /// </summary>
        /// <param name="gregorianYear">The gregorian year.</param>
        /// <param name="gregorianMonth">The gregorian month.</param>
        /// <param name="gregorianDay">The gregorian day.</param>
        public static void GregorianToEthiopian(ref int gregorianYear, ref int gregorianMonth, ref int gregorianDay)
        {
            FixedToEthiopian(GregorianToFixed(gregorianYear, gregorianMonth, gregorianDay), ref gregorianYear, ref gregorianMonth, ref gregorianDay);
        }

        /// <summary>
        /// Gregorians to ethiopian.
        /// </summary>
        /// <param name="gregorianDate">The gregorian date.</param>
        /// <returns></returns>
        public static string GregorianToEthiopian(DateTime gregorianDate)
        {
            if (gregorianDate.Year == 1)//When the date is null, the year comes as 0001
                return "Unknown";
            int gregorianYear, gregorianMonth, gregorianDay;
            gregorianYear = gregorianDate.Year;
            gregorianMonth = gregorianDate.Month;
            gregorianDay = gregorianDate.Day;

            FixedToEthiopian(GregorianToFixed(gregorianYear, gregorianMonth, gregorianDay), ref gregorianYear, ref gregorianMonth, ref gregorianDay);
            return gregorianDay + "/" + gregorianMonth + "/" + gregorianYear; //by now the date is ethiopian
        }

        /// <summary>
        /// Gregorians to ethiopian.
        /// </summary>
        /// <param name="hour">The hour.</param>
        /// <returns></returns>
        private static int GregorianToEthiopian(int hour)
        {
            return (((0x18 + hour) - 6) % 0x18);
        }

        /// <summary>
        /// Gets the day of week.
        /// </summary>
        /// <returns></returns>
        public DayOfWeek GetDayOfWeek()
        {
            return EthiopianDayOfWeek(Year, Month, Day);
        }

        /// <summary>
        /// Gets the day of week int.
        /// </summary>
        /// <returns></returns>
        public int GetDayOfWeekInt()
        {
            return (int)EthiopianDayOfWeek(Year, Month, Day);
        }

        /// <summary>
        /// Ethiopians to gregorian.
        /// </summary>
        /// <param name="hour">The hour.</param>
        /// <returns></returns>
        public static int EthiopianToGregorian(int hour)
        {
            return (((0x18 + hour) + 6) % 0x18);
        }

        /// <summary>
        /// Ethiopians to gregorian.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        public static void EthiopianToGregorian(ref int year, ref int month, ref int day)
        {
            FixedToGregorian(EthiopianToFixed(year, month, day), ref year, ref month, ref day);
        }

        /// <summary>
        /// Ethiopians to gregorian.
        /// </summary>
        /// <param name="ethiopianDateDDMMYYYY">The ethiopian date DDMMYYYY.</param>
        /// <returns></returns>
        public static DateTime EthiopianToGregorian(string ethiopianDateDDMMYYYY)
        {
            string[] dateParts = ethiopianDateDDMMYYYY.Split('/');
            int day = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int year = int.Parse(dateParts[2]);

            FixedToGregorian(EthiopianToFixed(year, month, day), ref year, ref month, ref day);
            DateTime dt = new DateTime(year, month, day);
            return dt;
        }

        /// <summary>
        /// Fixeds to gregorian.
        /// </summary>
        /// <param name="fixedDate">The fixed date.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        private static void FixedToGregorian(int fixedDate, ref int year, ref int month, ref int day)
        {
            GregorianCalendar gregorianCalendar = new GregorianCalendar();
            year = GregorianYearFromFixed(fixedDate);
            int num2 = fixedDate - GregorianToFixed(year, 1, 1);
            int num = (fixedDate < GregorianToFixed(year, 3, 1)) ? 0 : (gregorianCalendar.IsLeapYear(year) ? 1 : 2);
            month = (int)Math.Floor((double)(((double)((12 * (num2 + num)) + 0x175)) / 367.0));
            day = (fixedDate - GregorianToFixed(year, month, 1)) + 1;
        }

        /// <summary>
        /// Gregorians the year from fixed.
        /// </summary>
        /// <param name="fixedDate">The fixed date.</param>
        /// <returns></returns>
        private static int GregorianYearFromFixed(int fixedDate)
        {
            int num = fixedDate - 1;
            int num2 = (int)Math.Floor((double)(((double)num) / 146097.0));
            int num3 = num % 0x23ab1;
            int num4 = (int)Math.Floor((double)(((double)num3) / 36524.0));
            int num5 = num3 % 0x8eac;
            int num6 = (int)Math.Floor((double)(((double)num5) / 1461.0));
            int num7 = num5 % 0x5b5;
            int num8 = (int)Math.Floor((double)(((double)num7) / 365.0));
            int num9 = (((400 * num2) + (100 * num4)) + (4 * num6)) + num8;
            return (((num4 == 4) || (num8 == 4)) ? num9 : (num9 + 1));
        }
    }

}
