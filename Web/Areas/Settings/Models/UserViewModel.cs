using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Settings.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public bool IsDisabled { get; set; }        
    }    
}