

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
using Cats.Web.Adminstration.Models.ViewModels;

namespace Cats.Web.Adminstration.ViewModelBinder
{
    public class CommodityGradeViewModelBinder
    {
        public static CommodityGradeViewModel BindCommodityGradeViewModel(CommodityGrade commodityGrade)
        {
            return new CommodityGradeViewModel()
                       {
                         CommodityGradeID=commodityGrade.CommodityGradeID,
                         Name=commodityGrade.Name,
                         Description = commodityGrade.Description

                       };
        }

        public static List<CommodityGradeViewModel> BindListCommodityGradeViewModel(List<CommodityGrade> commodityGrades)
        {
            return commodityGrades.Select(BindCommodityGradeViewModel).ToList();
        }
        public static CommodityGrade BindCommodityGrade(CommodityGradeViewModel commodityGradeViewModel, CommodityGrade commodityGrade = null)
        {
            return commodityGrade ?? new CommodityGrade()
            {
              CommodityGradeID=commodityGradeViewModel.CommodityGradeID,
              Name=commodityGradeViewModel.Name,
              Description=commodityGradeViewModel.Description

            };
        }
    }
}


 
