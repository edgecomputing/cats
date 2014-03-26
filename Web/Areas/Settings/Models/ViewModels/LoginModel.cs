using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Settings.Models.ViewModels
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}