using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Data.Micro.ViewModels
{
    public class RecentRequest
    {
        public int RequestID { get; set; }
        public string RequestNumber { get; set; }
        public int Month { get; set; }
        public DateTime RequestDate { get; set; }
        public int Status { get; set; }
    }
}