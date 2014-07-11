using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class ForgetPasswordRequest
    {
        [Key]
        public int ForgetPasswordRequestID { get; set; }
        public int UserProfileID { get; set; }
        public System.DateTime GeneratedDate { get; set; }
        public System.DateTime ExpieryDate { get; set; }
        public bool Completed { get; set; }
        public string RequestKey { get; set; }
    }
}
