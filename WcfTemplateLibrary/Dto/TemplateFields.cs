using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Cats.Service.TemplateEditor.Dto
{
    [DataContract]
    public class TemplateFields
    {
        [DataMember]
        public int TemplateFieldId { get; set; }
        [DataMember]
        public Nullable<int> TemplateId { get; set; }
        [DataMember]
        public string Field { get; set; }
    }
}
