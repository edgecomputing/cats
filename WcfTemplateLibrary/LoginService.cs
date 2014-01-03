using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Cats.Services.Security;

namespace Cats.Service.TemplateEditor
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Login" in both code and config file together.
    public class LoginService : ILoginService
    {
        IUserAccountService _userAccountService=new UserAccountService();

        public bool Authenticate(string userName, string passWord)
        {
            return _userAccountService.Authenticate(userName, passWord);
        }
    }
}
