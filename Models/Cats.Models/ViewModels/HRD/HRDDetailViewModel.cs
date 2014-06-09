using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cats.Models.ViewModels.HRD
{
    public class HRDDetailViewModel
    {

        public int HRDDetailID { get; set; }
        public int HRDID { get; set; }

        //[DataType(DataType.Currency)]
        [Range(0, Double.MaxValue, ErrorMessage = "Number of Beneficiaries can not be less than 0!")]
        public int NumberOfBeneficiaries { get; set; }
        //[DataType(DataType.Duration)]
        [Range(0, 12, ErrorMessage = "Duration can not be less than 0 and greater than 12!")]
        public int DurationOfAssistance { get; set; }
        public int WoredaID { get; set; }
        public string Woreda { get; set; }
        public string Zone { get; set; }
        public string Region { get; set; }
        public int StartingMonth { get; set; }

    }
    public class DurationViewModel
    {
        public string StartDateGC { get; set; }
        public string EndDateGC { get; set; }
        public string StartDateEC { get; set; }
        public string EndDateEC { get; set; }
    }
}
