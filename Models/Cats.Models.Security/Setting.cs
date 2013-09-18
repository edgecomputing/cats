using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Security
{
   public class Setting
    {
        public int SettingID { get; set; }
        public string Category { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Option { get; set; }
        public string Type { get; set; }
    }
}
