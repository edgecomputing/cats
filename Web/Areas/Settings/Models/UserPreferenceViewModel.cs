using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Settings.Models
{
    public class UserPreferenceViewModel
    {
        public List<LanguageCode> Language { get; set; }
        public List<PreferedWeightMeasurementUnit> PreferedWeightMeasurement { get; set; }
        public List<Calendar> DatePreference { get; set; }
        public List<Keyboard> KeyboardLanguage { get; set; }
        public List<Theme> DefaultTheme { get; set; }
    }

    public class LanguageCode
    {
        public string Language { get; set; }
    }

    public class PreferedWeightMeasurementUnit
    {
        public string PreferedWeightMeasurement { get; set; }
    }

    public class Calendar
    {
        public string DatePreference { get; set; }
    }

    public class Keyboard
    {
        public string KeyboardLanguage { get; set; }
    }

    public class Theme
    {
        public string DefaultTheme { get; set; }
    }
}