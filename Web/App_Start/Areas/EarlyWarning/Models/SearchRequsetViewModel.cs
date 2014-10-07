using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class SearchRequsetViewModel
    {
        public SearchRequsetViewModel()
        {
            this.DateTo = DateTime.Now;
            this.DateFrom = DateTime.Now.AddMonths(-2);
        }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int? RegionID{ get; set; }
        public int? ProgramID { get; set; }
        public int? StatusID { get; set; }
    }

    public class SearchRequistionViewModel
    {
        public int? RegionID { get; set; }
        public int? ProgramID { get; set; }
        public int? StatusID { get; set; }
    }

    public class RequestStatus
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }
}