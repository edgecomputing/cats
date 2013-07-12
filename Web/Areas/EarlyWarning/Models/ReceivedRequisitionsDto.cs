using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class ReceivedRequisitionsDto
    {

       
        public int ProgramId { get; set; }
        public string Program { get; set; }
        public int Round { get; set; }
        public DateTime RequistionDate { get; set; }
        public int Year { get; set; }
        public String ReferenceNumber { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public string Region { get; set; }
        public string Create { get; set; }
    }
}