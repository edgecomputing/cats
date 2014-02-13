using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hubs
{
    /// <summary>
    /// Leger Class
    /// </summary>
    public partial class Ledger
    {

        /// <summary>
        /// Constants in the Ledger 
        /// </summary>
        public class Constants
        {

            /// <summary>
            /// 
            /// </summary>
            public const int GOODS_ON_HAND_UNCOMMITED = 2;
          //  public const int GOODS_ON_HAND_COMMITED = 3;
            public const int GOODS_PROMISSED_PLEDGE = 4;
            public const int GOODS_PROMISSED_GIFT_CERTIFICATE_UNCOMMITED = 13;
            public const int GOODS_PROMISSED_GIFT_CERTIFICATE_COMMITED = 5;
            public const int GOODS_PROMISSED_AS_PART_OF_LOAN_UNCOMMITED = 7;
            public const int GOODS_PROMISSED_AS_PART_OF_LOAN_COMMITED = 8;
           // public const int GOODS_DISPATCHED = 9;
            //public const int GOODS_RECIEVABLE = 10;
            //public const int LIABILITIES = 11;
            //public const int GOODS_UNDER_CARE = 12;
            //public const int PLEDGED_TO_FDP = 15;
            //public const int COMMITED_TO_FDP = 16;



            //new 
            public const int GOODS_ON_HAND = 1;

            public const int GOODS_IN_TRANSIT = 2;
            public const int GOODS_RECIEVABLE = 3;
            public const int LIABILITIES = 4;
            public const int GOODS_UNDER_CARE = 5;
            public const int ADJUSTMENT = 6;
            public const int PLEDGED_TO_FDP = 7;
            public const int COMMITED_TO_FDP = 8;
            public const int DELIVERY_IN_TRANSIT = 9;
            public const int REQUIRMENT_DOCUMENT_PALN = 10;
            public const int REQUIRMENT_DOCUMENT = 11;
            public const int DELIVERY_RECEIPT = 12;
            public const int LOSS = 13;
            public const int DAMAGED = 14;
        }

    }
}
