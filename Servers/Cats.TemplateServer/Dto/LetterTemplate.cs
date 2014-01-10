using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Cats.TemplateServer.Dto
{
    [DataContract]
    public class LetterTemplate
    {
        [DataMember]
        public int LetterTemplateID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string FileName  { get; set; }
        [DataMember]
        public int TemplateType { get; set; }
    }
}