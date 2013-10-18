using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models.Security.ViewModels;

namespace Cats.Areas.Settings.Models
{
    public class UserViewModel
    {
        public int UserProfileID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GrandFatherName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public bool IsDisabled { get; set; }
        public List<Cats.Models.Security.ViewModels.Application> Applications { get; set; }
        public bool IsSelected { get; set; }
    }    
}