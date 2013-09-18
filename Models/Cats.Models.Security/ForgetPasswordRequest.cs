using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Security
{
   public class ForgetPasswordRequest
    {
        public int ForgetPasswordRequestID { get; set; }
        public int UserAccountID { get; set; }
        public DateTime GeneratedDate { get; set; }
        public DateTime ExpieryDate { get; set; }
        public bool Completed { get; set; }
        public string RequestKey { get; set; }

    }
}
