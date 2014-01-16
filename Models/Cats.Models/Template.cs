

namespace Cats.Models
{
    public partial class Template
    {
        public int TemplateId { get; set; }
        public int TemplateType { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Remark { get; set; }
        public virtual TemplateType TemplateType1 { get; set; }
    }
}
