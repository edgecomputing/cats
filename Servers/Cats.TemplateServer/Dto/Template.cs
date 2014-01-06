
using System.Runtime.Serialization;

namespace Cats.TemplateServer.Dto
{
    [DataContract]
    public class Template
    {
        [DataMember]
        public int TemplateId { get; set; }
        [DataMember]
        public int TemplateType { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Remark { get; set; }
    }
}
