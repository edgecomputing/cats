using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Areas.Settings.Models
{
    public class ForgetPasswordRequestModel
    {
        [Required]
        [Remote("ISValidUserName", "Users")]
        public string UserName { get; set; }
    }
}