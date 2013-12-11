using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class StateTemplate
    {
        public int StateTemplateID { get; set; }
        //Fields

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Access Level")]
        public int AllowedAccessLevel { get; set; }

        [Display(Name = "Type")]
        public int StateType { get; set; }

        [Display(Name = "State No")]
        public int StateNo { get; set; }
        //Relationships

        [Display(Name = "Process")]
        public virtual ProcessTemplate ParentProcessTemplate { get; set; }

        [Display(Name = "Process ID")]
        public int ParentProcessTemplateID { get; set; }

        public virtual ICollection<FlowTemplate> InitialStateFlowTemplates { get; set; }

       
        public virtual ICollection<FlowTemplate> FinalStateFlowTemplates { get; set; }

//        public virtual ICollection<RegionalPSNPPlan> RegionalPSNPPlans { get; set; }

        public virtual ICollection<BusinessProcessState> DerivedBusinessProcessStates { get; set; }
        
       // public virtual ICollection<BusinessProcess> CurrentStateBusinessProcesss { get; set; }
    }
    public class StateTemplatePOCO
    {
        public int StateTemplateID { get; set; }
        //Fields

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Access Level")]
        public int AllowedAccessLevel { get; set; }

        [Display(Name = "Type")]
        public int StateType { get; set; }

        [Display(Name = "State No")]
        public int StateNo { get; set; }


        //Relationships

        [Display(Name = "Process ID")]
        public int ParentProcessTemplateID { get; set; }

        

        //public virtual ICollection<Request> CurrentStateRequests { get; set; }
    }
     public class LookupData
     {
         public int ID { get; set; }
         public string Name { get; set; }
     }
}