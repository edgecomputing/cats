using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.Areas.Procurement.Models
{
    public class TransporterAgreementViewModel
    {
        public int TransporterID { get; set; }
        public string TransporterName { get; set; }
        public string TransporterSubcity { get; set; }
        public string TransporterKebele { get; set; }
        public string TransporterHouseNo { get; set; }

        public List<TransactionDetail> TransactionDetailList { get; set; }

        //public Dictionary<string, object> ToDictionary()
       //{
       //    var dictionary = new Dictionary<string, object>
       //                          {
       //                              {"Region", AdminUnit.AdminUnit2.AdminUnit2},
       //                              {"Zone", AdminUnit.AdminUnit2},
       //                              {"Woreda", AdminUnit},
       //                              {"WarehouseID", SourceID},
       //                              {"Tariff", Tariff}
       //                          };
       //    return dictionary;
       //}
    }
}