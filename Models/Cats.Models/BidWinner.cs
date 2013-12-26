using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
   public class BidWinner
    {

       public int BidWinnerID { get; set; }
       public int BidID { get; set; }
       public int SourceID { get; set; }
       public int DestinationID { get; set; }
       public int TransporterID { get; set; }
       public int CommodityID { get; set; }
       public decimal Amount { get; set; }
       public decimal Tariff { get; set; }
       public int Position { get; set; }
       public int Status { get; set; }
       public DateTime ExpiryDate { get; set; }

       public int BusinessProcessID { get; set; }
       //public int TransportOrderID { get; set; }


       #region Navigation Properties

       public virtual Bid Bid { get; set; }
       public virtual Transporter Transporter { get; set; }
       public virtual AdminUnit AdminUnit{ get; set; }
       public virtual Hub Hub { get; set; }
       public virtual Commodity Commodity { get; set; }

       public virtual BusinessProcess BusinessProcess { get; set; }
      // public virtual TransportOrder TransportOrder { get; set; }

        #endregion

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
