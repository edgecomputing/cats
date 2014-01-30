using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class UtilizationHeader
    {
        public UtilizationHeader()
        {
            this.UtilizationDetails = new List<UtilizationDetail>();
        }

        public int DistributionId { get; set; }
        public int RequisitionId { get; set; }
        public int WoredaID { get; set; }
        public int PlanId { get; set; }
        public int  Month { get; set; }
        public int Round { get; set; }
        public DateTime DistributionDate { get; set; }
        public int? DistributedBy { get; set; }
        public string Remark { get; set; }

        public int MaleLessThan5Years { get; set; }
        public int FemaleLessThan5Years { get; set; }
        public int MaleBetween5And18Years { get; set; }
        public int FemaleBetween5And18Years { get; set; }
        public int MaleAbove18Years { get; set; }
        public int FemaleAbove18Years { get; set; }

        public virtual ICollection<UtilizationDetail> UtilizationDetails { get; set; }
        public virtual ICollection<DistributionByAgeDetail> DistributionByAgeDetails { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
