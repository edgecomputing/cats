using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Areas.Procurement.Models;
using Cats.Models.Hubs;

namespace Cats.Areas.Logistics.Models
{
    public class TransportOrderDispatchViewModel
    {
        public TransportOrderViewModel TransportOrderViewModel { get; set; }
        public List<DispatchViewModel> DispatchViewModels { get; set; }
        public List<DispatchViewModel> DispatchViewModelsWithGRN { get; set; } 
    }
}