using System;
using System.Collections.Generic;

namespace Cats.Models.ViewModels.HRD
{
    public class CreateHumanitarianRequirementViewModel
    {
        private readonly HumanitarianRequirement _humanitarianRequirement;
        private List<DetailViewModel> _details = new List<DetailViewModel>(); 
        public CreateHumanitarianRequirementViewModel()
        {
            _humanitarianRequirement = new HumanitarianRequirement();
        }

        public CreateHumanitarianRequirementViewModel(HumanitarianRequirement humanitarianRequirement)
        {
            _humanitarianRequirement = humanitarianRequirement;
            foreach (var detail in humanitarianRequirement.HumanitarianRequirementDetails)
            {
                _details.Add(new DetailViewModel(detail));
            }
        }

        public int Year { get { return _humanitarianRequirement.Year; } set { _humanitarianRequirement.Year = value; } }
        public string Month { get { return _humanitarianRequirement.Month; } set { _humanitarianRequirement.Month = value; } }
        public DateTime CreatedDate { get { return _humanitarianRequirement.CreatedDate; } set { _humanitarianRequirement.CreatedDate = value; } }
        public List<DetailViewModel> Details
        {
            get { return _details ?? (_details = new List<DetailViewModel>()); }
            set { _details = value; }
        }

        public HumanitarianRequirement HumanitarianRequirement { get { return _humanitarianRequirement; } }
    }
}
