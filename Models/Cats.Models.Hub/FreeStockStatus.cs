using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Hubs
{
    public class FreeStockStatus
    {
        [Key]
        public string SINumber { get; set; }
        //public string Vessel { get; set; }
        public decimal? AllocatedToHub { get; set; }
        //public decimal? dispatchedBalance { get; set; }
        public decimal? CommitedBalance { get; set; }
        //public string Donor { get; set; }
        public decimal? UncommitedStock { get; set; }
        public decimal? TotalStockOnHand { get; set; }
        public string CommodityName { get; set; }
        //public string ProgramName { get; set; }
        public string ProjectCode { get; set; }
        public decimal? ReceivedBalance { get; set; }
        //public int? ProgramID { get; set; }
        public int? CommodityID { get; set; }
        //public int? ShippingInstructionID { get; set; }
        //public int? ProjectCodeID { get; set; }

        public decimal? PercentageReceived { get; set; }
    }
}
