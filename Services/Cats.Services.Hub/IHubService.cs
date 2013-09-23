using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hub;
using Cats.Models.Hub.ViewModels.Common;
using Cats.Models.Hub.ViewModels.Report;
using Cats.Models.Hub.ViewModels.Report.Data;


namespace Cats.Services.Hub
{
    public interface IHubService:IDisposable
    {
        bool AddHub(Models.Hub.Hub hub);
        bool DeleteHub(Models.Hub.Hub hub);
        bool DeleteById(int id);
        bool EditHub(Models.Hub.Hub hub);
        Models.Hub.Hub FindById(int id);
        List<Models.Hub.Hub> GetAllHub();
        List<Models.Hub.Hub> FindBy(Expression<Func<Models.Hub.Hub, bool>> predicate);
        List<StoreViewModel> GetAllStoreByUser(UserProfile user);
        IEnumerable<StockStatusReport> GetStockStatusReport(int hubID, int commodityID);
        IEnumerable<StatusReportBySI_Result> GetStatusReportBySI(int hubID);
        IEnumerable<DispatchFulfillmentStatus_Result> GetDispatchFulfillmentStatus(int hubID);

        List<FreeStockProgram> GetFreeStockGroupedByProgram(int HuBID, FreeStockFilterViewModel freeStockFilterViewModel);
         List<Models.Hub.Hub> GetAllWithoutId(int hubId);
         List<Models.Hub.Hub> GetOthersHavingSameOwner(Models.Hub.Hub hub);
        List<Models.Hub.Hub> GetOthersWithDifferentOwner(Models.Hub.Hub hub);
   


    }
}
