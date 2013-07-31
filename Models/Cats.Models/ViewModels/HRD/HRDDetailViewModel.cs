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

        public int Cereal { get; set; }
        public int Pulse { get; set; }
        public int CSB { get; set; }
        public int Oil { get; set; }
    }
}
