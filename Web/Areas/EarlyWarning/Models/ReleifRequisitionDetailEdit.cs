using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.Areas.EarlyWarning.Models
{
    public class ReleifRequisitionDetailEdit
    {

        public int RequisitionDetailID { get; set; }
        public int RequisitionID { get; set; }
        public string Commodity { get; set; }
        public int BenficiaryNo { get; set; }
        public decimal Amount { get; set; }
        public string FDP { get; set; }
        public string Donor { get; set; }
        public ReleifRequisitionDetailEditInput Input { get; set; }
        public class ReleifRequisitionDetailEditInput
        {
            public int Number { get; set; }
            public decimal Amount { get; set; } 
        }
    }
}