using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.ViewModels.HRD
{
    public class HRDViewModel
    {


        public int HRDID { get; set; }
        public int Year { get; set; }
        public string Season { get; set; }
        public int SeasonID { get; set; }
        public string Ration { get; set; }
        public DateTime CreatedDate { get; set; }
         [Display(Name = "Date Created")]
        public string CreatedDatePref { get; set; }
        [Display(Name = "Published Date")]
        public string PublishedDatePref { get; set; }
        public DateTime PublishedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Plan { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public Nullable<int> StatusID { get; set; }
        public List<HRDDetailViewModel> HRDDetails { get; set; }

        public string HRDName
        {
            get { return string.Format("{0} {1}", Season, Year); }
        }
       

        
    }
    public class HrdAddWoredaViewModel
    {
        public int HRDID { get; set; }
        public int HRDDetailID { get; set; }
        public int WoredaID { get; set; }
        public int Duration { get; set; }
        public int Beneficiary { get; set; }
        public int StartingMonth { get; set; }
        [Required]
         public int RegionID { get; set; }
        [Required]
        public int ZoneID { get; set; }
        
    }
}
