using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Settings.Models.ViewModels
{
    public class AdminUnitViewModel
    {
        public int AdminUnitID { get; set; }
        public string AdminUnitName { get; set; }
        public int AdminUnitTypeID { get; set; }
        public string AdminUnitType { get; set; }
        public int ParentID { get; set; }
        public string Parent { get; set; }
    }
}