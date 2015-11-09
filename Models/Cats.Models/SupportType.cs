using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class SupportType
    {

       //public SupportType()
       //{
       //    this.WoredaStockDistributions=new List<WoredaStockDistribution>();
       //}
       public int SupportTypeID { get; set; }
       public string Description { get; set; }

      // public virtual ICollection<WoredaStockDistribution> WoredaStockDistributions { get; set; } 
    }
}
