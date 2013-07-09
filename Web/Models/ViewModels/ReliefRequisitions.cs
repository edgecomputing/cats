using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Models.ViewModels
{
    public class ReliefRequisitionsViewModel
    {
        public int RequisitionId { get; set; }
        public string CommodityName { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public int Round { get; set; }
        public string RequistionNo { get; set; }
        public decimal Amount { get; set; }
        public int Beneficiaries { get; set; }
        public int HubId { get; set; }
    }
}