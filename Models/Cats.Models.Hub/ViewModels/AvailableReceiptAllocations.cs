using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cats.Models.Hub.ViewModels
{
    public class AvailableReceiptAllocations
    {

        public Int32 ReceiptAllocationID { get; set; }

        public String SINumber { get; set; }

        public Int32 CommodityID { get; set; }

        public String ProjectNumber { get; set; }

        public Decimal QuantityInMT { get; set; }

        public Int32 CommoditySourceID { get; set; }

        public Boolean IsClosed { get; set; }

    }
}

