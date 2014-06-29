using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Cats.Data.Hub;
using Cats.Data.Hub.UnitWork;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels.Common;
using Cats.Models.Hubs.ViewModels.Report;
using Cats.Models.Hubs.ViewModels.Report.Data;


namespace Cats.Services.Hub
{
    public class HubService : IHubService
    {

        private readonly IUnitOfWork _unitOfWork;


        public HubService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region Default Service Implementation
        public bool AddHub(Models.Hubs.Hub hub)
        {
            _unitOfWork.HubRepository.Add(hub);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteHub(Models.Hubs.Hub hub)
        {
            if (hub == null) return false;
            _unitOfWork.HubRepository.Delete(hub);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.HubRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.HubRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditHub(Models.Hubs.Hub hub)
        {
            _unitOfWork.HubRepository.Edit(hub);
            _unitOfWork.Save();
            return true;
        }

        public Models.Hubs.Hub FindById(int id)
        {
            return _unitOfWork.HubRepository.FindById(id);
        }

        public List<Models.Hubs.Hub> GetAllHub()
        {
            return _unitOfWork.HubRepository.GetAll();
        }

        public List<Models.Hubs.Hub> FindBy(System.Linq.Expressions.Expression<Func<Models.Hubs.Hub, bool>> predicate)
        {
            return _unitOfWork.HubRepository.FindBy(predicate);
        }
        #endregion

        public List<StoreViewModel> GetAllStoreByUser(UserProfile user)
        {
            if (user == null || user.DefaultHub == null)
            {
                return new List<StoreViewModel>();
            }
            //var stores = (from c in user.DefaultHub.Stores select new ViewModels.Common.StoreViewModel { StoreId = c.StoreID, StoreName = string.Format("{0} - {1} ", c.Name, c.StoreManName) }).OrderBy(c => c.StoreName).ToList();
           
            //=======================modified by banty=================================
            //var stores = (from c in _unitOfWork.StoreRepository.GetStoreByHub(user.DefaultHub.HubID) select new ViewModels.Common.StoreViewModel { StoreId = c.StoreID, StoreName = string.Format("{0} - {1} ", c.Name, c.StoreManName) }).OrderBy(c => c.StoreName).ToList();
            var stores = (from c in _unitOfWork.StoreRepository.Get(t=>t.HubID==user.DefaultHub.HubID) select new StoreViewModel { StoreId = c.StoreID, StoreName = string.Format("{0} - {1} ", c.Name, c.StoreManName) }).OrderBy(c => c.StoreName).ToList();
            
            //==============================end
            
            //stores.Insert(0, new ViewModels.Common.StoreViewModel { StoreName = "Total Hub" });  //I need it for report only so I will modify it on report
            return stores;
        }
        public DataTable GetStockStatusReport(int hubID, int commodityID)
        {
            var commodity = _unitOfWork.CommodityRepository.FindById(commodityID);
            if (commodity != null && commodity.CommodityTypeID == 1)
                return _unitOfWork.ReportRepository.RPTStockStatus(hubID, commodityID);
            else
               return new DataTable();
                //return _unitOfWork.ReportRepository.RPT_StockStatusNonFood(hubID, commodityID);
        }

        public IEnumerable<StatusReportBySI_Result> GetStatusReportBySI(int hubID)
        {
            return _unitOfWork.ReportRepository.GetStatusReportBySI(hubID).AsEnumerable();
        }

        public IEnumerable<DispatchFulfillmentStatus_Result> GetDispatchFulfillmentStatus(int hubID)
        {
            return _unitOfWork.ReportRepository.GetDispatchFulfillmentStatus(hubID);
        }


        public List<FreeStockProgram> GetFreeStockGroupedByProgram(int HuBID, FreeStockFilterViewModel freeStockFilterViewModel)
        {
            var dbGetStatusReportBySI = _unitOfWork.ReportRepository.GetStatusReportBySI(HuBID).ToList();
            if (freeStockFilterViewModel.ProgramId.HasValue && freeStockFilterViewModel.ProgramId != 0)
            {
                dbGetStatusReportBySI = dbGetStatusReportBySI.Where(p => p.ProgramID == freeStockFilterViewModel.ProgramId).ToList();
            }

            if (freeStockFilterViewModel.CommodityId.HasValue && freeStockFilterViewModel.CommodityId != 0)
            {
                dbGetStatusReportBySI = dbGetStatusReportBySI.Where(p => p.CommodityID == freeStockFilterViewModel.CommodityId).ToList();
            }

            if (freeStockFilterViewModel.ShippingInstructionId.HasValue && freeStockFilterViewModel.ShippingInstructionId != 0)
            {
                dbGetStatusReportBySI = dbGetStatusReportBySI.Where(p => p.ShippingInstructionID == freeStockFilterViewModel.ShippingInstructionId).ToList();
            }
            if (freeStockFilterViewModel.ProjectCodeId.HasValue && freeStockFilterViewModel.ProjectCodeId != 0)
            {
                dbGetStatusReportBySI = dbGetStatusReportBySI.Where(p => p.ProjectCodeID == freeStockFilterViewModel.ProjectCodeId).ToList();
            }

            return (from t in dbGetStatusReportBySI
                    group t by new { t.ProgramID, t.ProgramName }
                        into b
                        select new FreeStockProgram()
                        {
                            Name = b.Key.ProgramName,
                            Details = b.Select(t1 => new FreeStockStatusRow()
                            {
                                SINumber = t1.SINumber,
                                Vessel = t1.Vessel,
                                Allocation = t1.AllocatedToHub ?? 0,
                                Dispatched =
                                    t1.dispatchedBalance ?? 0,
                                Transported =
                                    t1.fullyCommitedBalance ?? 0,
                                Donor = t1.Donor,
                                FreeStock = t1.UncommitedStock ?? 0,
                                PhysicalStock =
                                    t1.TotalStockOnHand ?? 0,
                                Product = t1.CommodityName,
                                Program = t1.ProgramName,
                                Project = t1.ProjectCode,
                                ReceivedAmount =
                                    t1.ReceivedBalance ?? 0,
                                Remark = ""
                            }).OrderBy(c => c.Product).ToList()
                        }).ToList();

        }
        public List<Models.Hubs.Hub> GetAllWithoutId(int hubId)
        {
            return _unitOfWork.HubRepository.Get(p => p.HubID != hubId).ToList();
        }
        public List<Models.Hubs.Hub> GetOthersHavingSameOwner(Models.Hubs.Hub hub)
        {
            return (from v in _unitOfWork.HubRepository.GetAll()
                    where v.HubID != hub.HubID && v.HubOwnerID == hub.HubOwnerID
                    select v).ToList();
        }
        public List<Models.Hubs.Hub> GetOthersWithDifferentOwner(Models.Hubs.Hub hub)
        {
            return (from v in _unitOfWork.HubRepository.GetAll()
                    where v.HubOwnerID != hub.HubOwnerID
                    select v).ToList();
        }
       
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    


}
}
