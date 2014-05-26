using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class HrdDonorCoverage
    {
       public int HRDDOnorCoverageID { get; set; }
       public int DonorID { get; set; }
       public int HRDID { get; set; }
       public DateTime CreatedDate { get; set; }
       public string Remark { get; set; }
       public int? PartitionId { get; set; }

       public virtual Donor Donor { get; set; }
       public virtual HRD Hrd { get; set; }
       public ICollection<HrdDonorCoverageDetail> HrdDonorCoverageDetails { get; set; }

    }
public class HrdDonorCoverageDetail
{
    public int HRDDonorCoverageDetailID { get; set; }
    public int HRDDonorCoverageID { get; set; }
    public int WoredaID { get; set; }

    public virtual AdminUnit AdminUnit { get; set; }
    public virtual HrdDonorCoverage HrdDonorCoverage { get; set; }
}
}
