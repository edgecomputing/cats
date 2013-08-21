using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.ViewModelBinder
{
    public class HRDViewModelBinder
    {

        public static List<HRDCompareViewModel> BindHRDCompareViewModel(HRD hrd)
        {
            if (hrd == null) return null;
            if (!hrd.HRDDetails.Any()) return null;
            var hrdCompareViewModels = new List<HRDCompareViewModel>();
            foreach (var hrdDetail in hrd.HRDDetails)
            {
                var hrdCompareViewModel = new HRDCompareViewModel();
                hrdCompareViewModel.Year = hrd.Year;
                hrdCompareViewModel.SeasonId = hrd.SeasonID.HasValue ? hrd.SeasonID.Value : 0;
                hrdCompareViewModel.Season = hrd.SeasonID.HasValue ? hrd.Season.Name : string.Empty;
                hrdCompareViewModel.RationId = hrd.RationID;
                hrdCompareViewModel.DurationOfAssistance = hrdDetail.DurationOfAssistance;
                hrdCompareViewModel.Beneficiaries = hrdDetail.NumberOfBeneficiaries;
                hrdCompareViewModel.RegionId = hrdDetail.AdminUnit.AdminUnit2.AdminUnitID;
                hrdCompareViewModel.Region = hrdDetail.AdminUnit.AdminUnit2.Name;
                hrdCompareViewModel.ZoneId = hrdDetail.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID;
                hrdCompareViewModel.Zone = hrdDetail.AdminUnit.AdminUnit2.AdminUnit2.Name;
                hrdCompareViewModel.WoredaId = hrdDetail.WoredaID;
                hrdCompareViewModel.Woreda = hrdDetail.AdminUnit.Name;
                hrdCompareViewModels.Add(hrdCompareViewModel);

            }
            return hrdCompareViewModels;
        }

    }
}