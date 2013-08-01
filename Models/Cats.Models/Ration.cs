using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models
{
    
        public partial class Ration
        {

            //public int RationID { get; set; }
            //public int CommodityID { get; set; }
            //public decimal Amount { get; set; }
            
            public int RationID { get; set; }
            public Nullable<System.DateTime> CreatedDate { get; set; }
            public Nullable<int> CreatedBy { get; set; }
            public Nullable<System.DateTime> UpdatedDate { get; set; }
            public Nullable<int> UpdatedBy { get; set; }
            public bool IsDefaultRation { get; set; }
            public string RefrenceNumber { get; set; }
            public virtual  ICollection<RationDetail> RationDetails { get; set; }
        }
  
}
