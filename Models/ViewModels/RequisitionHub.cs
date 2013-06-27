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
        public String Region { get; set; }
        public Commodity RequestedItem { get; set; }
        public double RequestedAmount { get; set; }
        public Hub AssignedHub { get; set; }
        public DateTime RequistionDate { get; set; }
        public String ReferenceNumber { get; set; }
        public string Remark { get; set; }
        public bool Status { get; set; }


        public virtual ICollection<ReliefRequisitionDetail> ReliefRequisitionDetails { get; set; }
        public virtual Round Round { get; set; }
        public virtual AdminUnit AdminUnit { get; set; }
        public virtual Program Program { get; set; }
        public List<string> Hubs { get; set; }
        public List<Hub> AllHubs { get; set; }

                public RequestHubAssignment Input { get; set; }
                public class RequestHubAssignment
                {
                    public int ReliefRequistionID { get; set; }
                    public int HubID { get; set; }
                }

    }
}
