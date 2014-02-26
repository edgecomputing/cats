using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Data.Micro.Models
{
    public class StatusPercentage
    {
        public string Status { get; set; }
        public int Count { get; set; }
        public int RegionID { get; set; }
    }
}