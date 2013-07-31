using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.ViewModels.HRD
{
    public class HRDDetailViewModel
    {
        private HRDDetail _detail;
        public HRDDetailViewModel()
        {
            _detail = new HRDDetail();
        }

        public HRDDetailViewModel(HRDDetail detail)
        {
            _detail = detail;
        }

        public int HRDDetailID { get; set; }
        public int HRDID { get; set; }
        public int NumberOfBeneficiaries { get; set; }
        public int DurationOfAssistance { get; set; }
        public int WoredaID { get { return _detail.WoredaID; } set { _detail.WoredaID = value; } }
        public string Woreda { get { return _detail.AdminUnit.Name; } }
        public string Zone { get { return _detail.AdminUnit.Name; } }
        public string Region { get { return _detail.AdminUnit.AdminUnit2.Name; } }
        public int StartingMonth { get { return _detail.StartingMonth; } set { _detail.StartingMonth = value; } }

        public int Cereal { get; set; }
        public int Pulse { get; set; }
        public int CSB { get; set; }
        public int Oil { get; set; }
    }
}
