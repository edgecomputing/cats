using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class HRDCompareViewModel
    {
        public int  HRDId { get; set; }
        public int Year { get; set; }
        public int SeasonId { get; set; }
        public int RationId { get; set; }
        public string Season { get; set; }
        public int  WoredaId { get; set; }
        public string Woreda { get; set; }
        public int RegionId { get; set; }
        public int ZoneId { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public int DurationOfAssistance { get; set; }
        public int Beneficiaries { get; set; }
        public int BeginingMonth { get; set; }
        public string StartingMonth { get; set; }
        public int RefrenceDuration { get; set; }
        public int BeneficiariesRefrence { get; set; }
        public int BeginingMonthReference { get; set; }
        public string StartingMonthReference { get; set; }
    }
}