using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cats.Helpers
{
    public class CatsGlobals
    {
        public const string CATS = "CATS";

        public const string EARLY_WARNING = "Early Warning";
        public static string EARLY_WARNING_PERMISSIONS = "EARLY_WARNING_PERMISSIONS";

        public const string PSNP = "PSNP";
        public static string PSNP_PERMISSIONS = "PSNP_PERMISSIONS";

        public const string LOGISTICS = "Logistics";
        public const string LOGISTICS_PERMISSIONS = "LOGISTICS_PERMISSIONS";

        public const string PROCUREMENT = "Procurement";
        public const string PROCUREMENT_PERMISSIONS = "PROCUREMENT_PERMISSIONS";

        public const string FINANCE = "Finance";
        public const string FINANCE_PERMISSIONS = "FINANCE_PERMISSIONS";

        public const string HUB = "Hub";
        public const string HUB_PERMISSIONS = "HUB_PERMISSIONS";

        public const string ADMINISTRATION = "Administration";
        public const string ADMINISTRATION_PERMISSIONS = "ADMINISTRATION_PERMISSIONS";

        public const string REGION = "Region";
        public const string REGION_PERMISSIONS = "REGION_PERMISSIONS";

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