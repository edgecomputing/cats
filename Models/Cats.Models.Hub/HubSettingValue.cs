using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub
{
    public partial class HubSettingValue
    {
        [Key]
        public int HubSettingValueID { get; set; }
        public int HubSettingID { get; set; }
        public int HubID { get; set; }
        public string Value { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual HubSetting HubSetting { get; set; }
    }
}
