using System;
using System.Collections.Generic;

namespace Cats.Models.ViewModels.HRD
{
    public class CreateHumanitarianRequirementViewModel
    {
        private readonly Models.HRD _hrd;
        private List<DetailViewModel> _details = new List<DetailViewModel>(); 
        public CreateHumanitarianRequirementViewModel()
        {
            _hrd = new Models.HRD();
        }

        public CreateHumanitarianRequirementViewModel(Models.HRD hrd)
        {
            _hrd = hrd;
            foreach (var detail in hrd.HRDDetails)
            {
                _details.Add(new DetailViewModel(detail));
            }
        }

        public int Year { get { return _hrd.Year; } set { _hrd.Year = value; } }
        public string Month { get { return _hrd.Month; } set { _hrd.Month = value; } }
        public DateTime CreatedDate { get { return _hrd.CreatedDate; } set { _hrd.CreatedDate = value; } }
        public List<DetailViewModel> Details
        {
            get { return _details ?? (_details = new List<DetailViewModel>()); }
            set { _details = value; }
        }

        public Models.HRD Hrd { get { return _hrd; } }
    }
}
