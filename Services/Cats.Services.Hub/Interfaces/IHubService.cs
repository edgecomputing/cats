using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels.Common;
using Cats.Models.Hubs.ViewModels.Report;
using Cats.Models.Hubs.ViewModels.Report.Data;


namespace Cats.Services.Hub
{
    public interface IHubService:IDisposable
    {
        bool AddHub(Models.Hubs.Hub hub);
        bool DeleteHub(Models.Hubs.Hub hub);
        bool DeleteById(int id);
        bool EditHub(Models.Hubs.Hub hub);
        Models.Hubs.Hub FindById(int id);
        List<Models.Hubs.Hub> GetAllHub();
        List<Models.Hubs.Hub> FindBy(Expression<Func<Models.Hubs.Hub, bool>> predicate);
        List<StoreViewModel> GetAllStoreByUser(UserProfile user);
        DataTable GetStockStatusReport(int hubID, int commodityID);
        IEnumerable<StatusReportBySI_Result> GetStatusReportBySI(int hubID);
        IEnumerable<DispatchFulfillmentStatus_Result> GetDispatchFulfillmentStatus(int hubID);

        List<FreeStockProgram> GetFreeStockGroupedByProgram(int HuBID, FreeStockFilterViewModel freeStockFilterViewModel);
         List<Models.Hubs.Hub> GetAllWithoutId(int hubId);
         List<Models.Hubs.Hub> GetOthersHavingSameOwner(Models.Hubs.Hub hub);
        List<Models.Hubs.Hub> GetOthersWithDifferentOwner(Models.Hubs.Hub hub);
   


    }
}
