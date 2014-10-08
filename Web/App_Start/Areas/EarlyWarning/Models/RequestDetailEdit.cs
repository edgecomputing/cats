using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class RequestDetailEdit
    {
        public int RegionalRequestDetailId { get; set; }
        public int RegionalRequestId { get; set; }
        public string Fdp { get; set; }
        public string Zone { get; set; }
        public string Wereda { get; set; }
      
        public int Beneficiaries { get; set; }

        public RequestDetailEditInput Input { get; set; }
        public class RequestDetailEditInput
        {
            public int Number { get; set; }
           
            public int Beneficiaries { get; set; }
        }
    }
}