using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class StockStatusViewModel
    {
        public int hubID { get; set; }
        public string CommodityName { get; set; }
        public decimal PhysicalStock { get; set; }
        public decimal FreeStock { get; set; }
    }
    
    public class TransactionViewModel {
        public string transactionID { get; set; }
        
    }
}
