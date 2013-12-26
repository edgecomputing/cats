using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class ReportFolder
    {
        private string Name { get; set; }
        private string URL { get; set; }
        private List<Report> Reports { get; set; }
        private List<ReportFolder> ReportFolders { get; set; }
    }
}
