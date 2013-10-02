using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cats.Models.ViewModels.HRD
{
    public class HRDDetailViewModel
    {
        //private HRDDetail _detail;
        //public HRDDetailViewModel()
        //{
        //    _detail = new HRDDetail();
        //}

        //public HRDDetailViewModel(HRDDetail detail)
        //{
        //    _detail = detail;
        //}

        public int HRDDetailID { get; set; }
        public int HRDID { get; set; }

        //[DataType(DataType.Currency)]
        [Range(0, Double.MaxValue, ErrorMessage = "Number of Beneficiaries can not be less than 0!")]
        public int NumberOfBeneficiaries { get; set; }
        //[DataType(DataType.Currency)]
       [Range(0, Int32.MaxValue, ErrorMessage = "Duration of Assistance can not be less than 0!")]
        public int DurationOfAssistance { get; set; }
        public int WoredaID { get; set; }
        public string Woreda { get; set; }
        public string Zone { get; set; }
        public string Region { get; set; }
        public int StartingMonth { get; set; }

        //public decimal Cereal { get; set; }
        //public decimal Pulse { get; set; }
        //public decimal BlendedFood { get; set; }
        //public decimal Oil { get; set; }
        //public decimal Total { get { return BlendedFood + Cereal + Oil + Pulse; } }
    }
}
