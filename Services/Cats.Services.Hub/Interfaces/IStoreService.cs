using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cats.Models.Hubs;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Cats.Models.Hubs.ViewModels.Report.Data;

namespace Cats.Services.Hub
{
   public  interface IStoreService
    {

        bool AddStore(Store store);
        bool DeleteStore(Store store);
        bool DeleteById(int id);
        bool EditStore(Store store);
        Store FindById(int id);
        List<Store> GetAllStore();
        List<Store> FindBy(Expression<Func<Store, bool>> predicate);

       List<int> GetStacksByStoreId(int? StoreId);
       List<Store> GetStoresWithBalanceOfCommodityAndSINumber(int parentCommodityId, int SINumber, int hubId);
       List<Store> GetStoresWithBalanceOfCommodity(int parentCommodityId, int hubId);
       List<int> GetStacksWithSIBalance(int storeId, int siNumber);
       List<int> GetStacksByToStoreIdFromStoreIdFromStack(int ToStoreId, int FromStoreId, int FromStackId);
       List<Store> GetAllByHUbs(List<Models.Hubs.Hub> HubIds);
       IEnumerable<BinCardViewModel> GetBinCard(int hubID, int? StoreID, int? CommodityID, string ProjectID);
       List<Store> GetStoreByHub(int hubId);
    }
}


      


          
      