using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
using Cats.Web.Adminstration.Models.ViewModels;

namespace Cats.Web.Adminstration.ViewModelBinder
{
    public static class DonorWoredaViewModelBinder
    {
        public static IEnumerable<DonorWoredaViewModel> BindDonorViewModel(List<HrdDonorCovarage> woredasByDonors )
        {
            if (woredasByDonors == null) return new List<DonorWoredaViewModel>();

           
            return woredasByDonors.Select(donor => donor.AdminUnit.ParentID != null ? new DonorWoredaViewModel()
                                                                                          {
                                                                                              WoredaId = donor.WoredaId,
                                                                                              DonorId = donor.DonorId,
                                                                                              WoredaName = donor.AdminUnit.Name,
                                                                                              WoredaDonorInt = donor.DonorWoredaId,
                                                                                              RegionId = (int) donor.AdminUnit.ParentID
                                                           
                                                                                          } : null);
        }

        public static HrdDonorCovarage BindDonorViewModel(DonorWoredaViewModel woredasByDonors)
        {
            if (woredasByDonors == null) return new HrdDonorCovarage();

            var woredaByDonor = new HrdDonorCovarage()
                                    {
                                        WoredaId = woredasByDonors.WoredaId,
                                        DonorId = woredasByDonors.DonorId,
                                    };
            return woredaByDonor;
        }
    }
}