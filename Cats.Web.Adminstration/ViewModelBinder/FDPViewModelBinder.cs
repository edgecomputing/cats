using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
using Cats.Web.Adminstration.Models.ViewModels;

namespace Cats.Web.Adminstration.ViewModelBinder
{
    public class FDPViewModelBinder
    {
        public static FDPViewModel BindFDPViewModel(FDP fdp)
        {
            return new FDPViewModel()
            {
                FDPID = fdp.FDPID,
                Name = fdp.Name,
                NameAM = fdp.NameAM,
                AdminUnitID = fdp.AdminUnitID,
                AdminUnit = fdp.AdminUnit.Name
            };
        }
        public static List<FDPViewModel> BindListFDPViewModel(List<FDP> fdPs)
        {
            return fdPs.Select(BindFDPViewModel).ToList();
        }

        public static FDP BindFDP(FDPViewModel fdpViewModel, FDP fdp = null)
        {
            return fdp ?? new FDP()
            {
                FDPID = fdpViewModel.FDPID,
                Name = fdpViewModel.Name,
                NameAM = fdpViewModel.NameAM,
                AdminUnitID = fdpViewModel.AdminUnitID
            };
        } 
    }

}