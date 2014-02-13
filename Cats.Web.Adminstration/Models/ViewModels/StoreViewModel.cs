using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Web.Adminstration.Models.ViewModels
{
    public class StoreViewModel
    {
        public int StoreID { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int HubID { get; set; }
        public string HubName { get; set; }
        public bool IsTemporary { get; set; }
        public bool IsActive { get; set; }
        public int StackCount { get; set; }
        public string StoreManName { get; set; }
    }
}