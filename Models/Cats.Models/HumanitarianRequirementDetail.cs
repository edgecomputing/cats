using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class HumanitarianRequirementDetail
    {
        public int HumanitarianRequirementDetailID { get; set; }
        public int HumanitarianRequirementID { get; set; }
        public int WoredaID { get; set; }
        public long NumberOfBeneficiaries { get; set; }
        public int DurationOfAssistance { get; set; }

        public virtual HumanitarianRequirement HumanitarianRequirement { get; set; }
        public virtual AdminUnit Woreda { get; set; }
        public virtual ICollection<CommodityTypeDetail> CommodityTypeDetails { get; set; }
    }
}
