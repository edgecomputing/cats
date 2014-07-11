using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class BusinessProcessState
    {
        public int BusinessProcessStateID { get; set; }
        //Fields
        /*
        [Display(Name = "StateID")]
        public int StateID { get; set; }
        */
        [Display(Name = "StateID")]
        public int StateID { get; set; }

        [Display(Name = "State")]
        public virtual StateTemplate BaseStateTemplate { get; set; }

        [Display(Name = "Performed By")]
        public string PerformedBy { get; set; }

        [Display(Name = "Date Performed")]
        public DateTime DatePerformed { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }


        //Relationships

        [Display(Name = "Process")]
        public virtual BusinessProcess ParentBusinessProcess { get; set; }

        [Display(Name = "Process ID")]
        public int ParentBusinessProcessID { get; set; }

        public virtual ICollection<BusinessProcess> CurrentStateBusinessProcesss { get; set; }
        

    }
    public class BusinessProcessStatePOCO
    {
        public int BusinessProcessStateID { get; set; }
        //Fields

        /*
        [Display(Name = "StateID")]
        public int StateID { get; set; }
        */

        [Display(Name = "Performed By")]
        public string PerformedBy { get; set; }

        [Display(Name = "Date Performed")]
        public DateTime DatePerformed { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }


        //Relationships

        [Display(Name = "Process ID")]
        public int ParentBusinessProcessID { get; set; }

    }
}