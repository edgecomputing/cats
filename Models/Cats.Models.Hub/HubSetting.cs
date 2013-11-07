using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace  Cats.Models.Hubs
{
    public partial class HubSetting
    {
        public HubSetting()
        {
            this.HubSettingValues = new List<HubSettingValue>();
        }
        [Key]
        public int HubSettingID { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string ValueType { get; set; }
        public string Options { get; set; }
        public virtual ICollection<HubSettingValue> HubSettingValues { get; set; }
    }
}
