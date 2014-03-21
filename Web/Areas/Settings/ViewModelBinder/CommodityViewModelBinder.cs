using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models.Hubs;
using Cats.Areas.Settings.Models.ViewModels;

namespace Cats.Areas.Settings.ViewModelBinder
{
    public class CommodityViewModelBinder
    {
        public static CommodityViewModel BindCommodityViewModel(Commodity commodity)
        {
            return new CommodityViewModel()
                       {
                           CommodityID = commodity.CommodityID,
                           Name = commodity.Name,
                           CommodityCode = commodity.CommodityCode,
                           NameAM = commodity.NameAM,
                           LongName = commodity.LongName,
                           CommodityTypeID = commodity.CommodityTypeID,
                           ParentID = commodity.CommodityTypeID

                       };
        }

        public static List<CommodityViewModel> BindListCommodityViewModel(List<Commodity> commodities)
        {
            return commodities.Select(BindCommodityViewModel).ToList();
        }
        public static Commodity BindCommodity(CommodityViewModel commodityViewModel, Commodity commodity = null)
        {
            return commodity ?? new Commodity()
            {
                CommodityID = commodityViewModel.CommodityID,
                Name = commodityViewModel.Name,
                CommodityCode = commodityViewModel.CommodityCode,
                NameAM = commodityViewModel.NameAM,
                LongName = commodityViewModel.LongName,
                CommodityTypeID = commodityViewModel.CommodityTypeID,
                ParentID = commodityViewModel.CommodityTypeID

            };
        }
    }
}