using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Cats.Models.Hub
{
    public class SMTPInfo
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "SMTP Server")]
        public string SMTPServer { get; set; }

        [Required]
        public int Port { get; set; }
    }
    public class EmailModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

}