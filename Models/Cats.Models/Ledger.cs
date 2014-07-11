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
            public const int REQUIRMENT_DOCUMENT_PLAN = 10;
            public const int REQUIRMENT_DOCUMENT = 11;

            public const int PLEDGED_TO_FDP = 7;
            public const int COMMITED_TO_FDP = 8;

            public const int GOODS_IN_TRANSIT = 2;
            public const int GOODS_ON_HAND = 1;

            public const int STATISTICS_FREE_STOCK = 21;

            public const int DELIVERY_RECEIPT = 12;
            public const int LOSS_IN_TRANSIT = 13;

            public const int GOODS_UNDER_CARE = 5;


            public const int PLEDGE = 15;
            public const int GIFT_CERTIFICATE = 16;

            public const int GOODS_RECIEVABLE = 3;


            // Not part of the normal workflow.
            public const int GOODS_ON_HAND_UNCOMMITED = 20;
            public const int LIABILITIES = 4;

            public const int DELIVERY_IN_TRANSIT = 9;
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
