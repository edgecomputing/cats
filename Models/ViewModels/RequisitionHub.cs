using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cats.Models.ViewModels
{
    public class RequisitionHub
    {
        

        public int ReliefRequistionId { get; set; }
        public int RegionID { get; set; }
        public int ProgramId { get; set; }
        public int RoundId { get; set; }
        public DateTime RequistionDate { get; set; }
        public int Year { get; set; }
        public String ReferenceNumber { get; set; }
        public string Remark { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<ReliefRequisitionDetail> ReliefRequisitionDetails { get; set; }
        public virtual Round Round { get; set; }
        public virtual AdminUnit AdminUnit { get; set; }
        public virtual Program Program { get; set; }
        public List<string> Hubs { get; set; }

    }
}
