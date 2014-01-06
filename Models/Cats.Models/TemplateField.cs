using System;

namespace Cats.Models
{
    public partial class TemplateField
    {
        public int TemplateFieldId { get; set; }
        public Nullable<int> TemplateId { get; set; }
        public string Field { get; set; }
    }
}
