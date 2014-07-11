using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class FlowTemplate
    {
        public int FlowTemplateID { get; set; }
        //Fields

        [Display(Name = "Name")]
        public string Name { get; set; }


        //Relationships
        [Display(Name = "Process")]
        public virtual ProcessTemplate ParentProcessTemplate { get; set; }

        [Display(Name = "Process ID")]
        public int ParentProcessTemplateID { get; set; }

        [Display(Name = "Initial State")]
        public virtual StateTemplate InitialState { get; set; }

        [Display(Name = "Initial State ID")]
        public int InitialStateID { get; set; }

        [Display(Name = "Final State")]
        public virtual StateTemplate FinalState { get; set; }

        [Display(Name = "Final State ID")]
        public int FinalStateID { get; set; }

    }
    public class FlowTemplatePOCO
    {
        public int FlowTemplateID { get; set; }
        //Fields

        [Display(Name = "Name")]
        public string Name { get; set; }


        //Relationships
        [Display(Name = "Process ID")]
        public int ParentProcessTemplateID { get; set; }

        [Display(Name = "Initial State ID")]
        public int InitialStateID { get; set; }

        [Display(Name = "Final State ID")]
        public int FinalStateID { get; set; }

    }
}