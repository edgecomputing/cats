using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class ReliefRequisition
    {
        
        public ReliefRequisition()
        {
            this.ReliefRequisitionDetials=new List<ReliefRequisitionDetail>();
        }

        public int ReliefRequisitionID { get; set; }
        public int CommodityID { get; set; }
        public int RegionID { get; set; }
        public int ZoneID { get; set; }
        public int Round { get; set; }
        public string RequisitionNo { get; set; }
        public int RequestedBy { get; set; }
        public DateTime RequisitionDate { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int Status { get; set; }
        public int ProgramID { get; set; }

        public virtual Commodity Commodity { get; set; }
        public virtual AdminUnit AdminUnit { get; set; }
        public virtual AdminUnit AdminUnit1 { get; set; }
        public virtual Program Program { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }
        public virtual ICollection<ReliefRequisitionDetail> ReliefRequisitionDetials { get; set; }
        
    }
}
