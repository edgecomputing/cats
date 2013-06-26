using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class ReliefRequisitionDetailEdit
    {
        public int ReliefRequisitionDetailId { get; set; }
        public int ReliefRequistionId { get; set; }
        public string Fdp { get; set; }
        public string Zone { get; set; }
        public string Wereda { get; set; }
        public decimal Grain { get; set; }
        public decimal Pulse { get; set; }
        public decimal Oil { get; set; }
        public decimal CSB { get; set; }
        public int Beneficiaries { get; set; }

        public ReliefRequisitionDetailEditInput Input { get; set; } 
        public class ReliefRequisitionDetailEditInput
        {
            public int Number { get; set; }
            public decimal Grain { get; set; }
            public decimal Pulse { get; set; }
            public decimal Oil { get; set; }
            public decimal CSB { get; set; }
            public int Beneficiaries { get; set; }
        }
    }
}