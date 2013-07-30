using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.ViewModels.HRD
{
    public class DetailViewModel
    {
        private HRDDetail _detail;
        public DetailViewModel()
        {
            _detail = new HRDDetail();
        }

        public DetailViewModel(HRDDetail detail)
        {
            _detail = detail;
        }

        public long NumberOfBeneficiaries { get { return _detail.NumberOfBeneficiaries; } set { _detail.NumberOfBeneficiaries = value; } }
        public int DurationOfAssistance { get { return _detail.DurationOfAssistance; } set { _detail.DurationOfAssistance = value; } }
        public int WoredaID { get { return _detail.WoredaID; } set { _detail.WoredaID = value; } }
        public string Woreda { get { return _detail.Woreda.Name; } }
        public string Zone { get { return _detail.Woreda.Name; } }
        public string Region { get { return _detail.Woreda.AdminUnit2.Name; } }
        public int StartingMonth { get { return _detail.StartingMonth; } set { _detail.StartingMonth = value; } }

        public int Cereal { get; set; }
        public int Pulse { get; set; }
        public int CSB { get; set; }
        public int Oil { get; set; }
    }
}
