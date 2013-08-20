using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class ProcessTemplate
    {
        public int ProcessTemplateID { get; set; }
        //Fields

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public virtual ICollection<StateTemplate> ParentProcessTemplateStateTemplates { get; set; }
        //Relationships

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