using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public StockStatusService(IUnitOfWork unitOfWork,
                                   IProgramService programService,
                                   ITransactionService transactionService)
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
                Name = hub.Name + " " + hub.HubOwner.Name,
                HubId = hub.HubID
            }).OrderBy(e => e.Name);
        }

        public IEnumerable<ProgramView> GetPrograms()
        {
            var programs = _unitOfWork.ProgramRepository.GetAll();

            return programs.Select(program => new ProgramView
                {
                    Name = program.Name,
                    ProgramId = program.ProgramID
                }
            );
        }

        public List<HubFreeStockView> GetFreeStockStatusD(int hub, int program, DateTime date)
        {
            
            var status = _transactionService.Get(t => t.HubID == hub && t.ProgramID == program && t.TransactionDate <= date);
            var grouped = (
                            from s in status
                            group s by s.ParentCommodityID into g
                            select new { CommodityId = g.Key, Transactions = g }
                          );

            var result = new List<HubFreeStockView>();

            foreach (var i in grouped)
            {

                //var d = i.Select(p => p.LedgerID == 3);
                //var phys = i.TakeWhile(r => r.LedgerID == 3).Sum(p => p.QuantityInMT);
                //TODO: && s.LedgerID =2 && s.LedgerID=12
                //var phys = i.Sum(p=>p.QuantityInMT=-40);
                //var free = i.Sum(f => f.LedgerID=3);
                //var u = (
                //            from n in i.Transactions
                //            where  (n.LedgerID == 0)
                //            select n
                //        );
                //var free =  u.Sum(a => a.QuantityInMT);
                //var phys =  i.Transactions.Sum(f => f.QuantityInMT);
                decimal phys = 0;
                decimal free = 0;
                //var free = i.Sum(a=>a.Select(a.QuantityInMT).Where(a.LedgerID==3));
                //var free = i.Sum(i.Select(a => a.QuantityInMT));

                var Commodity = "";

                foreach (var s in i.Transactions)
                {
                    Commodity = s.Commodity.Name;

                    if (s.LedgerID == 2 || s.LedgerID == 3 || s.LedgerID == 12)
                    {
                        phys = phys + Math.Abs(s.QuantityInMT);
                    }

                    if (s.LedgerID == 2)
                    {
                        free = free + Math.Abs(s.QuantityInMT);
                    }
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

            var grouped = (
                           from s in status
                           group s by s.ParentCommodityID into g
                           select new { CommodityId = g.Key, Transactions = g }
                         );

            var result = new List<HubFreeStockView>();

            foreach (var i in grouped)
            {

                //var d = i.Select(p => p.LedgerID == 3);
                //var phys = i.TakeWhile(r => r.LedgerID == 3).Sum(p => p.QuantityInMT);
                //TODO: && s.LedgerID =2 && s.LedgerID=12
                //var phys = i.Sum(p=>p.QuantityInMT=-40);
                //var free = i.Sum(f => f.LedgerID=3);
                //var u = (
                //            from n in i.Transactions
                //            where  (n.LedgerID == 0)
                //            select n
                //        );
                //var free =  u.Sum(a => a.QuantityInMT);
                //var phys =  i.Transactions.Sum(f => f.QuantityInMT);
                decimal phys = 0;
                decimal free = 0;
                //var free = i.Sum(a=>a.Select(a.QuantityInMT).Where(a.LedgerID==3));
                //var free = i.Sum(i.Select(a => a.QuantityInMT));

                var Commodity = "";

                foreach (var s in i.Transactions)
                {
                    Commodity = s.Commodity.Name;

                    if (s.LedgerID == 2 || s.LedgerID == 3 || s.LedgerID == 12)
                    {
                        phys = phys + Math.Abs(s.QuantityInMT);
                    }

                    if (s.LedgerID == 2)
                    {
                        free = free + Math.Abs(s.QuantityInMT);
                    }
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

        public List<HubFreeStockSummaryView> GetStockSummary(int program, string date)
        {
            DateTime _date = Convert.ToDateTime(date);
            var status = _transactionService.Get(t => t.HubID != null && t.ProgramID == program && DateTime.Compare(t.TransactionDate, _date) <= 0);
            var grouped = (
                           from s in status
                           group s by s.HubID into g
                           select new { CommodityId = g.Key, Transactions = g }
                         );

            var result = new List<HubFreeStockSummaryView>();

             foreach (var i in grouped)
            {

                decimal phys = 0;
                decimal free = 0;

                var hubName = "";

                foreach (var s in i.Transactions)
                {
                    hubName = s.Hub.Name;

                    if (s.LedgerID == 2 || s.LedgerID == 3 || s.LedgerID == 12)
                    {
                        phys = phys + Math.Abs(s.QuantityInMT);
                    }

                    if (s.LedgerID == 2)
                    {
                        free = free + Math.Abs(s.QuantityInMT);
                    }
                }

                var item = new HubFreeStockSummaryView()
                {
                    HubName = hubName,
                    TotalPhysicalStock = phys,
                    TotalFreestock = free
                };

                result.Add(item);
            }
            return result.ToList();

            //var _date = Convert.ToDateTime(date);

            //var summary = _transactionService.Get(s => s.ProgramID == program && DateTime.Compare(s.TransactionDate, _date) <= 0);

            //var grouped = (
            //                   from s in summary
            //                   group s by s.ParentCommodityID into g
            //                   select g
            //              );

            //var result = new List<HubFreeStockSummaryView>();

            //foreach (var i in grouped)
            //{
            //    var phys = i.Sum(s => s.LedgerID = Ledger.Constants.GOODS_ON_HAND_UNCOMMITED);
            //    var free = i.Sum(f => f.LedgerID = Ledger.Constants.GOODS_ON_HAND_UNCOMMITED);
            //    var Hub = "";

            //    foreach (var s in i)
            //    {
            //        Hub = i.Key.ToString();

            //        var item = new HubFreeStockSummaryView()
            //        {
            //            HubName = Hub,
            //            TotalPhysicalStock = phys,
            //            TotalFreestock = free
            //        };

            //        result.Add(item);

            //    }
            //}
            //return result.ToList();
        }

        public List<SummaryFreeAndPhysicalStockModel> GetFreeAndPhysicalStockSummary()
        {
            var status = _transactionService.Get(t => DateTime.Compare(t.TransactionDate, DateTime.Now) <= 0, null, "Hub,Program,Commodity");

            var grouped = from c in status
                            group c by new
                            {
                                c.HubID,
                                c.Hub,
                                c.ProgramID,
                                c.Program,
                                c.ParentCommodityID,
                                c.Commodity1
                            } into g
                            select new { HubID = g.Key.HubID, Hub = g.Key.Hub, ProgramID = g.Key.ProgramID, Program = g.Key.Program, 
                                CommodityId = g.Key.ParentCommodityID, ParentCommodity = g.Key.Commodity1, Transactions = g };

            var result = new List<SummaryFreeAndPhysicalStockModel>();

            foreach (var i in grouped)
            {
                decimal phys = 0;
                decimal free = 0;

                var hubName = i.Hub.Name;
                var program = i.Program.Name;
                var commodity = "";

                foreach (var s in i.Transactions)
                {
                    commodity = s.Commodity.Name;
                    
                    if (s.LedgerID == 2 || s.LedgerID == 3 || s.LedgerID == 12)
                    {
                        phys = phys + Math.Abs(s.QuantityInMT);
                    }

                    if (s.LedgerID == 2)
                    {
                        free = free + Math.Abs(s.QuantityInMT);
                    }
                }

                var item = new SummaryFreeAndPhysicalStockModel()
                {
                    HubName = hubName,
                    Program = program,
                    CommodityName = commodity,
                    PhysicalStock = phys,
                    FreeStock = free
                };

                result.Add(item);
            }
            return result.ToList();
        }

        public List<HubFreeStockSummaryView> GetStockSummaryD(int program, DateTime date)
        {
            var status = _transactionService.Get(t => t.HubID != null && t.ProgramID == program && DateTime.Compare(t.TransactionDate, date) <= 0);
            var grouped = (
                           from s in status
                           group s by s.HubID into g
                           select new { CommodityId = g.Key, Transactions = g }
                         );

            var result = new List<HubFreeStockSummaryView>();

            foreach (var i in grouped)
            {
                decimal phys = 0;
                decimal free = 0;

                var hubName = "";

                foreach (var s in i.Transactions)
                {
                    hubName = s.Hub.Name;

                    if (s.LedgerID == 2 || s.LedgerID == 3 || s.LedgerID == 12)
                    {
                        phys = phys + Math.Abs(s.QuantityInMT);
                    }   

                    if (s.LedgerID == 2)
                    {
                        free = free + Math.Abs(s.QuantityInMT);
                    }
                }
                var item = new HubFreeStockSummaryView()
                {
                    HubName = hubName,
                    TotalPhysicalStock = phys,
                    TotalFreestock = free
                };

                result.Add(item);
            }
            return result.ToList();
        }


      public List<VWCommodityReceived> GetReceivedCommodity (Expression<Func<VWCommodityReceived, bool>> filter = null)
      {
          return _unitOfWork.VWCommodityReceived.Get(filter,null,string.Empty).ToList();
      }
 
      public List<VWFreePhysicalStock> GetSummaryFreePhysicalStock(Expression<Func<VWFreePhysicalStock, bool>> filter = null)
      {
          return _unitOfWork.VWFreePhysicalStock.Get(filter, null, string.Empty).ToList();
      }

        public List<VWDispatchCommodity> GetDispatchedCommodity(
            Expression<Func<VWDispatchCommodity, bool>> filter = null)

      {
          return _unitOfWork.VWDispatchCommodity.Get(filter, null, string.Empty).ToList();
      }

        public List<VWCarryOver> GetCarryOverStock(Expression<Func<VWCarryOver, bool>> filter = null)
      {
          return _unitOfWork.VWCarryOver.Get(filter, null, string.Empty).ToList();
      }
      public List<VWTransferredStock> GetTransferredStock(Expression<Func<VWTransferredStock, bool>> filter = null)
      {
          return _unitOfWork.VWTransferredStock.Get(filter, null, string.Empty).ToList();

      }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}