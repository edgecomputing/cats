using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
using Cats.Areas.Settings.Models.ViewModels;

namespace Cats.Areas.Settings.ViewModelBinder
{
    public class AdminUnitViewModelBinder
    {
        public static AdminUnitViewModel BindAdminUnitViewModel(AdminUnit adminUnit)
        {
            return new AdminUnitViewModel()
            {
                AdminUnitID = adminUnit.AdminUnitID,
                AdminUnitName = adminUnit.Name,
                AdminUnitTypeID = adminUnit.AdminUnitTypeID ?? 0,
                ParentID = adminUnit.ParentID ?? 0 
            };
        }
        public static List<AdminUnitViewModel> BindListAdminUnitViewModel(List<AdminUnit> adminUnits)
        {
            return adminUnits.Select(BindAdminUnitViewModel).ToList();
        }

        public static AdminUnit BindAdminUnit(AdminUnitViewModel adminUnitViewModel, AdminUnit adminUnit = null)
        {
            return adminUnit ?? new AdminUnit()
            {
                AdminUnitID = adminUnitViewModel.AdminUnitID,
                Name = adminUnitViewModel.AdminUnitName,
                AdminUnitTypeID = adminUnitViewModel.AdminUnitTypeID,
                ParentID = adminUnitViewModel.ParentID
            };
        } 
    }
}