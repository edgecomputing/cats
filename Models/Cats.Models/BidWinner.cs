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
       public decimal Amount { get; set; }
       public decimal Tariff { get; set; }
       public int Position { get; set; }
       public int Status { get; set; }
       public DateTime ExpiryDate { get; set; }


       #region Navigation Properties

       public virtual Bid Bid { get; set; }
       public virtual Transporter Transporter { get; set; }
       public virtual AdminUnit AdminUnit{ get; set; }
       public virtual Hub Hub { get; set; }

        #endregion

    }
}
