using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class ApplicationSetting
    {

        //SettingID
        [Display(Name = "ID")]
        [Key]
        public int SettingID { get; set; }

        //SettingName
        [Display(Name = "Name")]
        public string SettingName { get; set; }

        //SettingValue
        [Display(Name = "Value")]
        public string SettingValue { get; set; }
    }
}