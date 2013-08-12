using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class RegionalRequestViewModel
    { 
        [ScaffoldColumn(false)]
        public int RegionalRequestID { get; set; }
       // [UIHint("AdminUnitEditor")]
        public int RegionID { get; set; }
         public string ReferenceNumber { get; set; }
        public string Region { get; set; }
       // [UIHint("ProgramEditor")]
        public int ProgramId { get; set; }

        public int RationID { get; set; }
        public string Ration { get; set; }
        public string Program { get; set; }
        public int Month { get; set; }
        public DateTime RequistionDate { get; set; }
       // [ScaffoldColumn(false)]
        public string RequestDateEt { get; set; }
        public int Year { get; set; }
       
       
       // [ScaffoldColumn(false)]
        public int StatusID { get; set; }
       
        public string   Status { get; set; } 
        public string Remark { get; set; }
       
    }
}