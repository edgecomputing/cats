using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Settings.Models.ViewModels
{
    public class FDPViewModel
    {
        public int FDPID { get; set; }
        public string Name { get; set; }
        public string NameAM { get; set; }
        public int AdminUnitID { get; set; }
        public string AdminUnit { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}