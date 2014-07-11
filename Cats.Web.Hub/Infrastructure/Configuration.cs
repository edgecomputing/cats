using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Cats.Web.Hub.Infrastructure
{
    public class Configuration
    {
        public int DefaultRoleId
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultRoleId"]);
            }
        }

        public static int RegionTypeId
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["RegionTypeId"]);
            }
        }

        public static int ZoneTypeId
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["ZoneTypeId"]);
            }
        }

        public static int WoredaTypeId
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["WoredaTypeId"]);
            }
        }

    }
}
