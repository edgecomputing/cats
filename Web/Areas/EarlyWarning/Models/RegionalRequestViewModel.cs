using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class RegionalRequestViewModel
    {
        public int RegionalRequestID { get; set; }
        public int RegionID { get; set; }
        public string Region { get; set; }
        public int ProgramId { get; set; }
        public string Program { get; set; }
        public int Round { get; set; }
        public DateTime RequistionDate { get; set; }
        public string RequestDateEt { get; set; }
        public int Year { get; set; }
        public string ReferenceNumber { get; set; }
        public string Remark { get; set; }
        public int StatusID { get; set; }
        public string   Status { get; set; }
    }
}