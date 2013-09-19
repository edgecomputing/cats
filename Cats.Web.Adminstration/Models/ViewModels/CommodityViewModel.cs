using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Web.Adminstration.Models.ViewModels
{
    public class CommodityViewModel
    {
        public int CommodityID { get; set; }
        public string CommodityCode { get; set; }
        public int CommodityTypeID { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public string NameAM { get; set; }
        public int ParentID { get; set; }
    }
}