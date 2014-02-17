using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Cats.Data.Micro;
using Cats.Data.Micro;


namespace Cats.Data.Micro.Models
{
    public class RegionalRequest : DynamicModel
    {
        //you don't have to specify the connection - Massive will use the first one it finds in your config
        public RegionalRequest()
            : base("CatsContext", "EarlyWarning.RegionalRequest", "RegionalRequestID")
        {
           
        }
    }
}