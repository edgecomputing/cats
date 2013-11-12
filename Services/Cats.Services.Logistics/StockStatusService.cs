using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.Hub;
using Cats.Services.Hub;
using Cats.Models;


namespace Cats.Services.Logistics
{
    public class StockStatusService: IStockStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionService _transactionService;
        private readonly ITransactionGroupService _transactionGroupService;

        public StockStatusService(ITransactionService transactionService) {
            _transactionService = transactionService;
        }

        public IEnumerable<StockStatusViewModel> FreeStockStatusAsOF(DateTime date)
        {
            return (from transaction in _transactionService.Get(d => d.TransactionDate <= date)
                    select new StockStatusViewModel
                    {
                        CommodityName = transaction.Commodity.Name,
                        FreeStock = transaction.QuantityInMT,
                        PhysicalStock = transaction.QuantityInMT,
                        hubID = 2
                    });

        }

        public IEnumerable<StockStatusViewModel> FreeStockByHub(int hubID)
        {
            var transactions = _transactionService.Get(tr => tr.HubID == hubID && tr.LedgerID == 2);

            var gr = (
                        from tr in transactions
                        group tr by tr.ParentCommodityID into g
                        select new { g.Key, g}
                     );

            var grs = (
                        from tr in transactions 
                        group tr by tr.ParentCommodityID into g
                        select new StockStatusViewModel{
                            CommodityName = g.Key.ToString(),
                            PhysicalStock = g.Sum(s=>s.QuantityInMT),
                            FreeStock = g.Sum(s=>s.QuantityInMT)
                        }
                     );

            return grs;
        }
        
        public IEnumerable<Object> FreeStockByHubAsOF(DateTime date, int hubID)
        {
            var s = new List<Object>();
            return s;
        }
    }
}