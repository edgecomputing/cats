using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class HRDDetail
    {
        public int HRDDetailID { get; set; }
        public int HRDID { get; set; }
        public int WoredaID { get; set; }
        public long NumberOfBeneficiaries { get; set; }
        public int DurationOfAssistance { get; set; }
        public int StartingMonth { get; set; }

        public virtual HRD HRD { get; set; }
        public virtual AdminUnit AdminUnit { get; set; }
        public virtual ICollection<CommodityTypeDetail> CommodityTypeDetails { get; set; }
    }
}
