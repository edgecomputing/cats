using System;
using System.Collections.Generic;
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
        public int NumberOfBeneficiaries { get; set; }
        public int DurationOfAssistance { get; set; }
        public int WoredaID { get; set; }
        public string Woreda { get; set; }
        public string Zone { get; set; }
        public string Region { get; set; }
        public int StartingMonth { get; set; }

        public decimal Cereal { get; set; }
        public decimal Pulse { get; set; }
        public decimal CSB { get; set; }
        public decimal Oil { get; set; }
        public decimal Total { get { return CSB + Cereal + Oil + Pulse; } }
    }
}
