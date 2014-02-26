using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Helpers
{
    public class CatsGlobals
    {
        public const string CATS = "CATS";


        public const string EARLY_WARNING = "EarlyWarning";
        public const string EARLY_WARNING_PERMISSIONS = "EARLY_WARNING_PERMISSIONS";

        public const string PSNP = "PSNP";
        public const string PSNP_PERMISSIONS = "PSNP_PERMISSIONS";

        public const string LOGISTICS = "Logistics";
        public const string LOGISTICS_PERMISSIONS = "LOGISTICS_PERMISSIONS";


        public enum Applications
        {
            EarlyWarning,
            PSNP,
            Logistics,
            Procurement,
            Finance,
            Hub,
            Administration,
            Region
        }
    }
}