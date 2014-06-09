using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Cats.Helpers
{
    public static class NumericHelpers
    {
        public static bool IsNumeric(this DataColumn col)
        {
            if (col == null)
                return false;
            // Make this const
            var numericTypes = new[] { typeof(Byte), typeof(Decimal), typeof(Double),
                typeof(Int16), typeof(Int32), typeof(Int64), typeof(SByte),
                typeof(Single), typeof(UInt16), typeof(UInt32), typeof(UInt64)};
            return numericTypes.Contains(col.DataType);
        }
    }
}