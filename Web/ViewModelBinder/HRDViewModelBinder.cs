using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Areas.EarlyWarning.Models;
using Cats.Helpers;
using Cats.Models;

namespace Cats.ViewModelBinder
{
    public class HRDViewModelBinder
    {

        public static List<HRDCompareViewModel> BindHRDCompareViewModel(HRD hrdOriginal,HRD hrdRefrence,int filterRegion)
        { var hrdCompareViewModels = new List<HRDCompareViewModel>();
        if (hrdOriginal == null) return hrdCompareViewModels;
            if (!hrdOriginal.HRDDetails.Any()) return  hrdCompareViewModels;
           
            foreach (var hrdDetail in hrdOriginal.HRDDetails.Where(t=>t.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID==filterRegion))
            {
                var hrdCompareViewModel = new HRDCompareViewModel();
                hrdCompareViewModel.Year = hrdOriginal.Year;
                hrdCompareViewModel.SeasonId = hrdOriginal.SeasonID.HasValue ? hrdOriginal.SeasonID.Value : 0;
                hrdCompareViewModel.Season = hrdOriginal.SeasonID.HasValue ? hrdOriginal.Season.Name : string.Empty;
                hrdCompareViewModel.RationId = hrdOriginal.RationID;
                hrdCompareViewModel.DurationOfAssistance = hrdDetail.DurationOfAssistance;
                hrdCompareViewModel.BeginingMonth = hrdDetail.StartingMonth;
                hrdCompareViewModel.Beneficiaries = hrdDetail.NumberOfBeneficiaries;
                hrdCompareViewModel.ZoneId = hrdDetail.AdminUnit.AdminUnit2.AdminUnitID;
                hrdCompareViewModel.Zone = hrdDetail.AdminUnit.AdminUnit2.Name;
                hrdCompareViewModel.RegionId = hrdDetail.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID;
                hrdCompareViewModel.Region = hrdDetail.AdminUnit.AdminUnit2.AdminUnit2.Name;
                hrdCompareViewModel.WoredaId = hrdDetail.WoredaID;
                hrdCompareViewModel.Woreda = hrdDetail.AdminUnit.Name;
                hrdCompareViewModel.StartingMonth = RequestHelper.MonthName(hrdDetail.StartingMonth);
                
                 if(hrdRefrence!=null)
                 {
                     var hrdReferenceDetail =
                         hrdRefrence.HRDDetails.FirstOrDefault(t => t.WoredaID == hrdDetail.WoredaID);
                     if(hrdReferenceDetail!=null)
                     {
                         hrdCompareViewModel.RefrenceDuration = hrdReferenceDetail.DurationOfAssistance;
                         hrdCompareViewModel.BeginingMonthReference = hrdReferenceDetail.StartingMonth;
                         hrdCompareViewModel.BeneficiariesRefrence = hrdReferenceDetail.NumberOfBeneficiaries;
                         hrdCompareViewModel.StartingMonthReference = RequestHelper.MonthName(hrdReferenceDetail.StartingMonth);
                     }
                 }
                hrdCompareViewModels.Add(hrdCompareViewModel);

            }
            return hrdCompareViewModels;
        }

    }
}