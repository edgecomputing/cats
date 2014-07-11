using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
using Cats.Web.Adminstration.Models.ViewModels;

namespace Cats.Web.Adminstration.ViewModelBinder
{
    public class CommodityTypeViewModelBinder
    {
        public static CommodityTypeViewModel BindCommodityTypeViewModel(CommodityType commodityType)
        {
            return new CommodityTypeViewModel()
                       {
                           Name = commodityType.Name,
                           CommodityTypeId = commodityType.CommodityTypeID
                       };
        }

        public static List<CommodityTypeViewModel> BindListCommodityTypeViewModel(List<CommodityType> commodityTypes)
        {
            return commodityTypes.Select(BindCommodityTypeViewModel).ToList();
        }
        public static CommodityType BindCommodityType(CommodityTypeViewModel commodityTypeViewModel, CommodityType commodityType = null)
        {
            var target = commodityType ?? new CommodityType();
            target.CommodityTypeID = commodityTypeViewModel.CommodityTypeId;
            target.Name = commodityTypeViewModel.Name;
            return target;
        }
    }
}