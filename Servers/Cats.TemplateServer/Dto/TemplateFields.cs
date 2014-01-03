using System;
using System.Runtime.Serialization;

namespace Cats.TemplateServer.Dto
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
