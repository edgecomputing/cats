using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class HumanitarianRequirement
    {
        public int HumanitarianRequirementID { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public DateTime CreatedDate { get; set; }
        public int RationID { get; set; }

        public virtual Ration Ration { get; set; }
        public virtual ICollection<HumanitarianRequirementDetail> HumanitarianRequirementDetails { get; set; }

    }
}
