using System.Collections.Generic;

namespace Cats.Models
{
    public partial class TemplateType
    {
        public TemplateType()
        {
            this.Templates = new List<Template>();
        }

        public int TemplateTypeId { get; set; }
        public string TemplateObject { get; set; }
        public string Remark { get; set; }
        public virtual ICollection<Template> Templates { get; set; }
    }
}
