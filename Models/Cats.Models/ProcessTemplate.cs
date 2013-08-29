using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
namespace Cats.Models
{
    public class ProcessTemplate
    {
        public ProcessTemplate()
        {
            ParentProcessTemplateStateTemplates = new List<StateTemplate>();
        }
        public int ProcessTemplateID { get; set; }
        //Fields
        
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public virtual ICollection<StateTemplate> ParentProcessTemplateStateTemplates { get; set; }
        //Relationships
        public virtual ICollection<BusinessProcess> ProcessTypeBusinessProcesss { get; set; }

       /* public StateTemplate StartingState { 
            get {
                ParentProcessTemplateStateTemplates.Where(s => s.StateType == 0).Select();
                return new StateTemplate(); 
            } }*/
    }
    public class ProcessTemplatePOCO
    {
        public int ProcessTemplateID { get; set; }
        //Fields

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }


        //Relationships

    }
   
}