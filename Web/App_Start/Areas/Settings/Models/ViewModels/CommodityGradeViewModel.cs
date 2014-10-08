using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.Areas.Settings.Models.ViewModels
{
    public class CommodityGradeViewModel
    {
        public int CommodityGradeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}