using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class TransportContractViewModel
    {
        public TransportContractViewModel()
        {
            TransportOrderDetail=new List<TransportOrderDetailViewModel>();
       }
        
        public int TransportOrderID { get; set; }
        public string TransportOrderNo { get; set; }
        public string ContractNumber { get; set; }
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Requested Dispatch Date")]
        public DateTime RequestedDispatchDate { get; set; }
        public DateTime OrderExpiryDate { get; set; }
        public ICollection<TransportOrderDetailViewModel> TransportOrderDetail { get; set; }
        
    }
}