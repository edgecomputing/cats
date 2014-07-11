using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Web.Adminstration.Models.ViewModels
{
    public class CommodityViewModel
    {
        public int CommodityID { get; set; }
        [Display(Name="Commodity Code")]
        public string CommodityCode { get; set; }
        [Display(Name="Commodity Type")]
        public int CommodityTypeID { get; set; }
        public string Name { get; set; }
        [Display(Name="Long Name")]
        public string LongName { get; set; }
        [Display(Name="Name AM")]
        public string NameAM { get; set; }
        [Display(Name="Parent Commodity")]
        public int ParentID { get; set; }
    }
}