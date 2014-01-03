using System.Runtime.Serialization;

namespace Cats.TemplateServer.Dto
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
