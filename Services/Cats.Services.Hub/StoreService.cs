using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cats.Data.Hub;
using Cats.Models.Hub;
using Cats.Models.Hub.MetaModels;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Cats.Models.Hub.ViewModels.Report.Data;

namespace Cats.Services.Hub
{
    public class StoreService:IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #region Default Service Implementation
        public bool AddStore(Store store)
        {
            _unitOfWork.StoreRepository.Add(store);
            _unitOfWork.Save();
            return true;

        }
        public bool EditStore(Store store)
        {
            _unitOfWork.StoreRepository.Edit(store);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteStore(Store store)
        {
            if (store == null) return false;
            _unitOfWork.StoreRepository.Delete(store);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.StoreRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.StoreRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Store> GetAllStore()
        {
            return _unitOfWork.StoreRepository.GetAll();
        }
        public Store FindById(int id)
        {
            return _unitOfWork.StoreRepository.FindById(id);
        }
        public List<Store> FindBy(Expression<Func<Store, bool>> predicate)
        {
            return _unitOfWork.StoreRepository.FindBy(predicate);
        }
        #endregion
       
    
        public List<Store> GetStoreByHub(int hubId)
        {
            return _unitOfWork.StoreRepository.FindBy(s => s.HubID == hubId).ToList();
        }

        public List<int> GetStacksByStoreId(int? StoreId)
        {
            List<int> stacks = new List<int>();

            if (StoreId != null)
            {
                var maxStackNumber = _unitOfWork.StoreRepository.FindBy(s => s.StoreID == StoreId).Select(r => r.StackCount).FirstOrDefault();
                   
                for (int i = 0; i <= maxStackNumber; i++)
                {
                    stacks.Add(i);
                }
            }
            return stacks;
        }

        public List<Store> GetStoresWithBalanceOfCommodityAndSINumber(int parentCommodityId, int SINumber, int hubId)
        {
            Models.Hub.Hub hub = FindHubById(hubId);
            List<Store> result = new List<Store>();
            foreach (var store in hub.Stores)
            {
                var balance = _unitOfWork.TransactionRepository.FindBy(s => s.StoreID == store.StoreID && parentCommodityId == s.ParentCommodityID &&
                                s.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED && s.ShippingInstructionID == SINumber).Select(q => q.QuantityInMT);
                    
                if (balance.Any() && balance.Sum() > 0)
                {           
                    result.Add(store);
                }
            }
            return result;
        }

        public List<Store> GetStoresWithBalanceOfCommodity(int parentCommodityId, int hubId)
        {
            Models.Hub.Hub hub = FindHubById(hubId);
            List<Store> result = new List<Store>();
            foreach (var store in hub.Stores)
            {
                var balance = ( _unitOfWork.TransactionRepository.FindBy(s => s.StoreID == store.StoreID && parentCommodityId == s.ParentCommodityID &&
                                s.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED).Select(q => q.QuantityInMT));

                if (balance.Any() && balance.Sum() > 0)
                {
                    result.Add(store);
                }
            }
            return result;
        }

        public List<int> GetStacksWithSIBalance(int storeId, int siNumber)
        {
            List<int> result = new List<int>();
            Store store = FindById(storeId);
            foreach (var stack in store.Stacks)
            {

                var balance = (_unitOfWork.TransactionRepository.FindBy(s => s.StoreID == store.StoreID && siNumber == s.ShippingInstructionID &&
                               s.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED  && s.Stack == stack).Select(q => q.QuantityInMT));

               
                if (balance.Any() && balance.Sum() > 0)
                {
                    result.Add(stack);
                }
            }
            return result;
        }

        public List<int> GetStacksByToStoreIdFromStoreIdFromStack(int ToStoreId, int FromStoreId, int FromStackId)
        {
            List<int> result = new List<int>();
            Store store = FindById(ToStoreId);
            if (ToStoreId == FromStoreId)
            {
                foreach (var stack in store.Stacks)
                {
                    if (stack != FromStackId)
                    {
                        result.Add(stack);
                    }
                }
            }
            else
            {
                foreach (var stack in store.Stacks)
                {
                    result.Add(stack);
                }
            }
            return result;
        }

        public List<Store> GetAllByHUbs(List<Models.Hub.Hub> HubIds)
        {
            List<int> hubIds = HubIds.Select(hubId => hubId.HubID).ToList();

            IQueryable<Store> result =  _unitOfWork.StoreRepository.GetAll().AsQueryable();
            result = (from sT in result
                      where hubIds.Any(p => p == sT.HubID)
                      select sT);

            return result.ToList();
        }

        public IEnumerable<BinCardViewModel> GetBinCard(int hubID, int? StoreID, int? CommodityID, string ProjectID)
        {
            var commodity = new Commodity();
            if (CommodityID.HasValue)
                commodity = FindCommodityById(CommodityID.Value);
            List<BinCardReport> results = new List<BinCardReport>();
            
            //TODO:the logic should be verified(Robi)
            if (commodity != null && commodity.CommodityTypeID == 1)
                results = _unitOfWork.ReportRepository.RPT_BinCard(hubID, StoreID, CommodityID, ProjectID).ToList();
            else
                results = _unitOfWork.ReportRepository.RPT_BinCard(hubID, StoreID, CommodityID, ProjectID).ToList();

            //var results = db.RPT_BinCard(hubID,StoreID,CommodityID,ProjectID);
            var returnValue = new List<BinCardViewModel>();
            decimal balance = 0;
            foreach (var res in results)
            {
                balance += (res.Received.HasValue) ? res.Received.Value : 0;
                balance -= (res.Dispatched.HasValue) ? res.Dispatched.Value : 0;

                returnValue.Add(new BinCardViewModel
                {
                    SINumber = res.SINumber,
                    DriverName = res.DriverName,
                    Transporter = res.Transporter,
                    TransporterAM = res.TransporterAM,
                    Date = res.Date,
                    Project = res.Project,
                    Dispatched = res.Dispatched,
                    Received = res.Received,
                    Balance = balance,
                    Identification = res.Identification,
                    ToFrom = res.ToFrom

                });
            }

            return returnValue;
        }

        public bool DeleteByID(int id)
        {
            var store = _unitOfWork.StoreRepository.FindBy(s => s.StoreID == id).SingleOrDefault();
            if (store == null) return false;
            _unitOfWork.StoreRepository.Delete(store);
            _unitOfWork.Save();
            return true;
        }

       
        public Models.Hub.Hub FindHubById(int HubId)
        {
            return _unitOfWork.HubRepository.FindBy(h => h.HubID == HubId).SingleOrDefault();
        }
        public Commodity FindCommodityById(int id)
        {
            return _unitOfWork.CommodityRepository.FindBy(c => c.CommodityID == id).SingleOrDefault();
        }
    }
}

      
