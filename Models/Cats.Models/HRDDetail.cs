using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class HRDDetail
    {
        //public HRDDetail()
        //{
        //    this.HRDCommodityDetails=new List<HRDCommodityDetail>();
        //}
        public int HRDDetailID { get; set; }
        public int HRDID { get; set; }
        public int WoredaID { get; set; }
        public int NumberOfBeneficiaries { get; set; }
        public int DurationOfAssistance { get; set; }
        public int StartingMonth { get; set; }
        public int? PartitionId { get; set; }
        
        public virtual HRD HRD { get; set; }
        public virtual AdminUnit AdminUnit { get; set; }
        //public virtual ICollection<HRDCommodityDetail> HRDCommodityDetails { get; set; }
    }
}
