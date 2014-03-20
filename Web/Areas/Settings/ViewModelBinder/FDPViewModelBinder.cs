using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
using Cats.Areas.Settings.Models.ViewModels;
using System.Data.Spatial;

namespace Cats.Areas.Settings.ViewModelBinder
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
                AdminUnit = fdp.AdminUnit.Name,
                //latitiude = fdp.FDPLocation!=null?fdp.FDPLocation.Latitude:0.0,
                //longitude = fdp.FDPLocation!=null?fdp.FDPLocation.Longitude: 0.0
                latitude = fdp.Latitude,
                longitude = fdp.Longitude

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
                AdminUnitID = fdpViewModel.AdminUnitID,
                //FDPLocation = DbGeography.FromText("POINT(47.605049 48.605049)"),
                //FDPLocation = DbGeography.FromText("POINT("+fdpViewModel.longitude.ToString()+" "+fdpViewModel.latitiude+")")
                Latitude = fdpViewModel.latitude,
                Longitude = fdpViewModel.longitude
            };
        } 
    }

}