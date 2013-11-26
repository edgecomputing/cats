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
        List<VWCommodityReceived>
        GetReceivedCommodity(Expression<Func<VWCommodityReceived, bool>> filter = null);
        List<HubFreeStockSummaryView> GetStockSummaryD(int program, DateTime date);
        List<VWCarryOver> GetCarryOverStock(Expression<Func<VWCarryOver, bool>> filter = null);

    }
}