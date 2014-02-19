using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class Ledger
    {
        public class Constants
        {

            /// <summary>
            /// 
            /// </summary>

            /// 
            /// 

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

            public const int GOODS_PROMISSED_PLEDGE = 15;
            public const int GOODS_PROMISSED_GIFT_CERTIFICATE_COMMITED = 16;
            public const int GOODS_PROMISSED_AS_PART_OF_LOAN_COMMITED = 17;
            public const int GOODS_PROMISSED_AS_PART_OF_LOAN_UNCOMMITED = 18;
            public const int GOODS_PROMISSED_GIFT_CERTIFICATE_UNCOMMITED = 19;
            public const int GOODS_ON_HAND_UNCOMMITED = 20;
            public const int STATISTICS = 21;
           
           
           
            
           


        }
    }

    public class TransactionConstants
    {
        public class Constants
        {
            public const string SHIPPNG_INSTRUCTION = "SI";
            public const string PROJECT_CODE = "PC";
            public const int HRD_PROGRAM_ID = 1;
            public const int PSNP_PROGRAM_ID = 2;
        }
    }
}
