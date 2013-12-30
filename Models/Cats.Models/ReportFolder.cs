using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class ReportFolder
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public List<ReportObj> Reports { get; set; }
    }
}
