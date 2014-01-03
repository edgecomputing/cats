using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Service.TemplateEditor.Dto
{
    [DataContract]
    public class LoginModel
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public bool RememberMe { get; set; }
    }
}
