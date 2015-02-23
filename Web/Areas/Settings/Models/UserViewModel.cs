using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models.Security.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Cats.Areas.Settings.Models
{
    public class UserViewModel
    {
        [Key]
        public int UserProfileID { get; set; }
         [Required]
        public string UserName { get; set; }
         [Required]
        public string FirstName { get; set; }
         [Required]
        public string LastName { get; set; }

        public string GrandFatherName { get; set; }
         [Required]
        public string Password { get; set; }
         [Required]
        public string PasswordConfirm { get; set; }
        public bool IsDisabled { get; set; }
        public List<Cats.Models.Security.ViewModels.Application> Applications { get; set; }
        public bool IsSelected { get; set; }
        public int? DefaultHub { get; set; }
        public bool RegionalUser { get; set; }
        [Required(ErrorMessage = "User type is Required", AllowEmptyStrings = false)]
        public string UserType { get; set; }
        public int CaseTeam { get; set; }
        public int RegionID { get; set; }
        [Required]
        public string Email { get; set; }
         [Required (ErrorMessage = "Program is Required!",AllowEmptyStrings = false)]
        public int? ProgramId { get; set; }

         public bool IsAdmin { get; set; }
        public bool TariffEntry { get; set; }
    }    
}