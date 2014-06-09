using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.PSNP.Models
{
    public class PSNPRequetViewModel
    {
        public string Number { get; set; }
        public int fdps { get; set; }
        public int amount { get; set; }
        public int status { get; set; }
        public int beneficiaries { get; set; }
        public int RequestId { get; set; }
    }
}