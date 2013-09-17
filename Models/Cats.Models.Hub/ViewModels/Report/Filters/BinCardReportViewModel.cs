using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hub.ViewModels.Report.Filters
{
    public class BinCardReportViewModel
    {

        /// <summary>
        /// Members are listed here.
        /// </summary>
        public int Store { get; set; }

        public List<Store> Stores { get; set; }

        public List<Commodity> Commodities { get; set; }

        public int Commodity { get; set; }

        public List<CommodityType> CommodityTypes { get; set; }

        public int CommodityType { get; set; }
 
        // Constructors
        public BinCardReportViewModel()
        {
            
        }
        //TODO:code smell separation of concern
        //public BinCardReportViewModel(IUnitOfWork repository, int hubID )
        //{
        //    // populate the stores
        //    this.Stores = repository.Store.GetStoreByHub(hubID);

        //}

        //public BinCardReportViewModel(IUnitOfWork repository, int hubID, int commodityType)
        //{
        //    // populate the stores
        //    this.CommodityType = commodityType;
        //    this.Stores = repository.Store.GetStoreByHub(hubID);
        //    this.CommodityTypes = repository.CommodityType.GetAll();
        //    this.Commodities = repository.CommodityType.FindById(CommodityType).Commodities.Where(c=>c.ParentID == null) .ToList();
        //    // populate the commodities in the store
        //}

        //public BinCardReportViewModel(IUnitOfWork repository, int hubID, int commodityType, int storeId)
        //{
        //    // populate the stores
        //    this.CommodityType = commodityType;
        //    this.Stores = repository.Store.GetStoreByHub(hubID);
        //    this.CommodityTypes = repository.CommodityType.GetAll();
        //    this.Commodities = repository.CommodityType.FindById(CommodityType).Commodities.Where(c => c.ParentID == null).ToList();
        //}

        

    }
}
