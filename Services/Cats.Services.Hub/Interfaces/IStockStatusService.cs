using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models.Hubs;

namespace Cats.Services.Hub.Interfaces
{
    public interface IStockStatusService: IDisposable
    {
        //IEnumerable<StockStatusViewModel> FreeStockStatusAsOF(DateTime date);
        //IEnumerable<StockStatusViewModel> FreeStockByHub(int hubID);
        //IEnumerable<Object> FreeStockByHubAsOF(DateTime date, int hubID);
        IOrderedEnumerable<HubView> GetHubs();
        IEnumerable<ProgramView> GetPrograms();
        List<HubFreeStockView> GetFreeStockStatusD(int hub, int program, DateTime date);
        List<HubFreeStockView> GetFreeStockStatus(int hub, int program, string date);
        List<HubFreeStockSummaryView> GetStockSummary(int program, string date);
        List<SummaryFreeAndPhysicalStockModel> GetFreeAndPhysicalStockSummary();
        List<VWCommodityReceived>
        GetReceivedCommodity(Expression<Func<VWCommodityReceived, bool>> filter = null);
        List<HubFreeStockSummaryView> GetStockSummaryD(int program, DateTime date);


        List<VWDispatchCommodity>
        GetDispatchedCommodity(Expression<Func<VWDispatchCommodity, bool>> filter = null);

        List<VWCarryOver> GetCarryOverStock(Expression<Func<VWCarryOver, bool>> filter = null);
        List<VWFreePhysicalStock> GetSummaryFreePhysicalStock(Expression<Func<VWFreePhysicalStock, bool>> filter = null);
        List<VWTransferredStock> GetTransferredStock(Expression<Func<VWTransferredStock, bool>> filter = null);
        List<HubFreeStockSummaryView> GetStockSummaryHubDahsBoard(int hubId, DateTime date);
        List<HubDispatchAllocationViewModel> GetHubDispatchAllocation(int program, DateTime date);

    }
}