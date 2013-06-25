using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Models
{
    public class ReliefRequisitionInput
    {
        public string Region { get; set; }
        public int RegionId { get; set; }
        public string ProgramName { get; set; }
        public int ProgramId { get; set; }
        public string Round { get; set; }
        public int RoundId { get; set; }
        public string RequisitionNo { get; set; }
        public DateTime RequisitionDate { get; set; }
        public int Year { get; set; }
        public IEnumerable<ReliefRequisionDetailInput> ReliefRequisionDetailInputs { get; set; }


        public class ReliefRequisionDetailInput
        {
            public string Zone { get; set; }
            public string Woreda { get; set; }
            public string FDPName { get; set; }
            public int FDPId { get; set; }
            public int NoBenficiaries { get; set; }
            public decimal TotalAmount { get { return ReliefRequisionDetailLineInputs.Sum(t => t.Amount); } }
            public IEnumerable<ReliefRequisionDetailLineInput> ReliefRequisionDetailLineInputs { get; set; }
            
        }

        public class ReliefRequisionDetailLineInput
        {
            public string CommodityName { get; set; }
            public int CommodityId { get; set; }
            public decimal Amount { get; set; }
        }
    }
}