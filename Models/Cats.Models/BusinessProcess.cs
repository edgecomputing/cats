using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class BusinessProcess
    {
        
        public int BusinessProcessID { get; set; }
        //Fields

        [Display(Name = "Document")]
        public int DocumentID { get; set; }

        [Display(Name = "DocumentType")]
        public string DocumentType { get; set; }


        //Relationships

        [Display(Name = "Process Template")]
        public virtual ProcessTemplate ProcessType { get; set; }

        [Display(Name = "Process Template ID")]
        public int ProcessTypeID { get; set; }

        [Display(Name = "CurrentState")]
        public virtual BusinessProcessState CurrentState { get; set; }

        [Display(Name = "CurrentState ID")]
        public int CurrentStateID { get; set; }

        public virtual ICollection<RegionalPSNPPlan> RegionalPSNPPlans { get; set; }

        public virtual ICollection<PaymentRequest> PaymentRequests { get; set; }

        public virtual ICollection<BidWinner> BidWinners { get; set; }

        public virtual ICollection<BusinessProcessState> BusinessProcessStates { get; set; }
        
    }
    public class BusinessProcessPOCO
    {
        public int BusinessProcessID { get; set; }
        //Fields

        [Display(Name = "Document")]
        public int DocumentID { get; set; }

        [Display(Name = "DocumentType")]
        public string DocumentType { get; set; }


        //Relationships

        [Display(Name = "Process Template ID")]
        public int ProcessTypeID { get; set; }

        [Display(Name = "CurrentState ID")]
        public int CurrentStateID { get; set; }

        [Display(Name = "CurrentStat")]
        public string CurrentStateName { get; set; }
    }
}