using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cats.Models.Hub
{
   public class StatusReportBySI_Result
   {
       [Key]
       public string SINumber { get; set; }
       public string Vessel { get; set; }
       public decimal? AllocatedToHub { get; set; }
       public decimal? dispatchedBalance { get; set; }
       public decimal? fullyCommitedBalance { get; set; }
       public string Donor { get; set; }
       public decimal? UncommitedStock { get; set; }
       public decimal? TotalStockOnHand { get; set; }
       public string CommodityName { get; set; }
       public string ProgramName { get; set; }
       public string ProjectCode { get; set; }
       public decimal? ReceivedBalance { get; set; }
       public int? ProgramID { get; set; }
       public int? CommodityID { get; set; }
       public int? ShippingInstructionID { get; set; }
       public int? ProjectCodeID { get; set; }





    }
}
