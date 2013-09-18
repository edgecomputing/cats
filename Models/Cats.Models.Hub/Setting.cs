using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub
{
    public partial class Setting
    {
        [Key]
        public int SettingID { get; set; }
        public string Category { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Option { get; set; }
        public string Type { get; set; }
    }
}
