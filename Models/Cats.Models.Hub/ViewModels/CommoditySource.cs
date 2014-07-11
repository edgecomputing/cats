using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hubs
{
    public partial class CommoditySource
    {
        public class Constants
        {
            public const int DONATION = 1;
            public const int LOAN = 2;
            public const int LOCALPURCHASE = 3;
            public const int RETURN = 4;
            public const int TRANSFER = 5;
            public const int OTHER = 7;
            public const int REPAYMENT = 8;
            public const int SWAP = 9;

        }

    }
}
