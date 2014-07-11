using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs.ViewModels.Common
{
    public class StoreBalanceViewModel
    {
        [Display(Name="Commodity")]
        public string ParentCommodityNameB { get; set; }
        [Display(Name ="Project Code")]
        public string ProjectCodeNameB { get; set; }
        [Display(Name= "SI Number ")]
        public string ShppingInstructionNumberB { get; set; }
        [Display(Name = "Store")]
        public string StoreNameB { get; set; }
        [Display(Name = "Satck")]
        public string StackNumberB { get; set; }
        [Display(Name = "Balance")]
        public decimal QtBalance { get; set; }
    }
}
