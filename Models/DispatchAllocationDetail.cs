using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class DispatchAllocationDetail
    {
        public DispatchAllocationDetail()
        {
            //this.RequisitionDetailLines = new List<RequisitionDetailLine>();
            //this.AllocationDetailLines = new List<AllocationDetailLine>();
        }
        [Key]
        public int DiapatcheAllocationDetailId { get; set; }
        public int ReliefRequisitionDetailId { get; set; }
        public int ReliefRequistionId { get; set; }
        public int Fdpid { get; set; }
        public decimal Grain { get; set; }
        public decimal Pulse { get; set; }
        public decimal Oil { get; set; }
        public decimal CSB { get; set; }
        public int Beneficiaries { get; set; }
        public string ProjectID { get; set; }
        public string SINumber { get; set; }
        public string Zone { get; set; }
        public string Wereda { get; set; }
        public string Commodity { get; set; }
        public float Quantity { get; set; }
        public string Hub { get; set; }

        #region Navigation Properties
        public virtual ReliefRequisition ReliefRequistion { get; set; }
        public virtual FDP Fdp { get; set; }
        //public virtual ICollection<RequisitionDetailLine> RequisitionDetailLines { get; set; }
        //public virtual ICollection<AllocationDetailLine> AllocationDetailLines { get; set; }
        #endregion
    }
    
}
