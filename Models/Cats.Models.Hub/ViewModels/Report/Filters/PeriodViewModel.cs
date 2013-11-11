using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hubs.ViewModels.Report
{
    public class PeriodViewModel
    {

        /// <summary>
        /// A period could specify either single or multiple instances of the following types
        /// 
        /// 1. Daily
        /// 2. Weekly
        /// 3. Monthly
        /// 4. Quarterly
        /// 5. Annual
        /// </summary>
        /// 

        public bool IsRange { get; set; }
        public bool IsForever { get; set; }
        public PeriodType PeriodType { get; set; }
        // For Single Selection of Date
        public DateTime SelectedDate { get; set; }

        // For Range selection of Dates
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        //TODO: For a single Selection of Week.

        //TODO: For range selection of Weeks
 
        // Single Month Selection
        public int Year { get; set; }
        public int Month { get; set; }
        public int Quarter { get; set; }

        // Range selection of Months
        public int StartYear { get; set; }
        public int StartMonth { get; set; }

        
        public int EndYear { get; set; }
        public int EndMonth { get; set; }

        // Range Selection of Quarter
        public int StartQuarter { get; set; }
        public int EndQuarter { get; set; }

        public int PeriodId { get; set; }
        public string PeriodName { get; set; }
    }
}
