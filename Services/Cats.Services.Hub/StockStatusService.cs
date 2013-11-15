using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Services.Hub.Interfaces;
using Cats.Data.Hub;
using Cats.Models.Hubs;
//using Cats.Helpers;

namespace Cats.Services.Hub
{
    public class StockStatusService : IStockStatusService
    
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProgramService _programService;
        private readonly ITransactionService _transactionService;

        public StockStatusService(IUnitOfWork unitOfWork, IProgramService programService,ITransactionService transactionService)
        {
            _unitOfWork = unitOfWork;
            _programService = programService;
            _transactionService = transactionService;
        }

        public IOrderedEnumerable<HubView> GetHubs()
        {
            var hubs = _unitOfWork.HubRepository.GetAll();

            return hubs.Select(hub => new HubView
            {
                Name = hub.Name,
                HubId = hub.HubID
            }).OrderBy(e => e.Name);
        }

        public IEnumerable<ProgramView> GetPrograms() {
            var programs = _unitOfWork.ProgramRepository.GetAll();

            return programs.Select(program => new ProgramView
                {
                    Name = program.Name,
                    ProgramId = program.ProgramID
                }
            );
        }
        
        public List<HubFreeStockView> GetFreeStockStatus(int hub, int program, DateTime date) {

            var status = _transactionService.Get(t => t.HubID == hub && t.ProgramID == program && t.TransactionDate <= date);
            
            var grouped = (from s in status 
                          group s by s.ParentCommodityID into g
                          select g);

            var result = new List<HubFreeStockView> ();
            
            foreach(var i in grouped){
                //var ds = i.Sum(s => s.LedgerID = 3).ToPreferedWeightUnit();
                //var sf = i.Sum(f => f.LedgerID = 2).ToPreferedWeightUnit();
                var phys = i.Sum(s => s.LedgerID = 3);
                var free = i.Sum(f => f.LedgerID = 2);
                var Commodity = "";
                              
                foreach (var s in i)
                {
                    Commodity = s.Commodity.Name;
                }
                var item = new HubFreeStockView()
                {
                    CommodityName = Commodity,
                    PhysicalStock = phys,
                    FreeStock = free
                };
                result.Add(item);
            }
            return result.ToList();
        }

        public List<HubFreeStockView> GetFreeStockStatus(int hub, int program, string date)
        {
            
            DateTime _date = Convert.ToDateTime(date);

            var status = _transactionService.Get(t => t.HubID == hub && t.ProgramID == program && DateTime.Compare(t.TransactionDate, _date) <= 0);
            
            var grouped = (from s in status
                           group s by s.ParentCommodityID into g
                           select g);

            var result = new List<HubFreeStockView>();

            foreach (var i in grouped)
            {

                var ds = i.Sum(s => s.LedgerID = 3);
                var sf = i.Sum(f => f.LedgerID = 2);
                //decimal free=0;
                //decimal physical=0;
                var Commodity = "";


                foreach (var s in i)
                {
                    Commodity = s.Commodity.Name;
                    //free += s.QuantityInMT;
                    //physical += s.QuantityInMT;
                }

                var d = new HubFreeStockView()
                {
                    CommodityName = Commodity,
                    PhysicalStock = ds,
                    FreeStock = sf
                };
                result.Add(d);
            }
            return result.ToList();
        }

        public List<HubFreeStockSummaryView> GetStockSummary(int program, string date)
        {

            var _date = Convert.ToDateTime(date);

            var summary = _transactionService.Get(s => s.ProgramID == program && DateTime.Compare(s.TransactionDate, _date) <= 0);

            var grouped = (
                               from s in summary
                               group s by s.HubID into g
                               select g
                          );

            var result = new List<HubFreeStockSummaryView>();

            foreach (var i in grouped)
            {
                var phys = i.Sum(s => s.LedgerID = 3);
                var free = i.Sum(f => f.LedgerID = 2);
                var Hub = "";

                foreach (var s in i)
                {
                    Hub = i.Key.ToString();

                    var item = new HubFreeStockSummaryView()
                    {
                        HubName = Hub,
                        TotalPhysicalStock = phys,
                        TotalFreestock = free
                    };

                    result.Add(item);

                }
            }
            return result.ToList();
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
    }
}