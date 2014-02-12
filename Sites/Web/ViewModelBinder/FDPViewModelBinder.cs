using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels;

namespace Cats.Web.Hub.ViewModelBinder
{
    public class FDPViewModelBinder
    {
        public static List<FDPViewModel> FDPListViewModelBinder(List<FDP> fdps)
        {
            return fdps.Select(fdp1 => new FDPViewModel()
                {
                    FDPID = fdp1.FDPID, 
                    Name = fdp1.Name,
                    NameAM = fdp1.NameAM, 
                    AdminUnitID = fdp1.AdminUnitID
                    
                }).ToList();
        }
    }
}