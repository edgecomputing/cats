
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
using Cats.Web.Adminstration.Models.ViewModels;

namespace Cats.Web.Adminstration.ViewModelBinder
{
    public class CommoditySourceViewModelBinder
    {
        public static CommoditySourceViewModel BindCommoditySourceViewModel(CommoditySource commoditySource)
        {
            return new CommoditySourceViewModel()
            {
                CommoditySourceID=commoditySource.CommoditySourceID,
                Name=commoditySource.Name

            };
        }

        public static List<CommoditySourceViewModel> BindListCommoditySourceViewModel(List<CommoditySource> commodities)
        {
            return commodities.Select(BindCommoditySourceViewModel).ToList();
        }
        public static CommoditySource BindCommoditySource(CommoditySourceViewModel commoditySourceViewModel, CommoditySource commoditySource = null)
        {
            var target = commoditySource ?? new CommoditySource();
            
                target.CommoditySourceID = commoditySourceViewModel.CommoditySourceID;
                target.Name = commoditySourceViewModel.Name;

            return target;
        }
    }
}