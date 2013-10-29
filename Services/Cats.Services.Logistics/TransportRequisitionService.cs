using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.Logistics;

namespace Cats.Services.Logistics
{
    public class TransportRequisitionService : ITransportRequisitionService
    {
        private readonly IUnitOfWork _unitOfWork;


        public TransportRequisitionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddTransportRequisition(TransportRequisition transportRequisition)
        {
            _unitOfWork.TransportRequisitionRepository.Add(transportRequisition);
            _unitOfWork.Save();
            return true;

        }
        public bool EditTransportRequisition(TransportRequisition transportRequisition)
        {
            _unitOfWork.TransportRequisitionRepository.Edit(transportRequisition);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteTransportRequisition(TransportRequisition transportRequisition)
        {
            if (transportRequisition == null) return false;
            _unitOfWork.TransportRequisitionRepository.Delete(transportRequisition);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.TransportRequisitionRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.TransportRequisitionRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<TransportRequisition> GetAllTransportRequisition()
        {
            return _unitOfWork.TransportRequisitionRepository.GetAll();
        }
        public TransportRequisition FindById(int id)
        {
            return _unitOfWork.TransportRequisitionRepository.FindById(id);
        }
        public List<TransportRequisition> FindBy(Expression<Func<TransportRequisition, bool>> predicate)
        {
            return _unitOfWork.TransportRequisitionRepository.FindBy(predicate);
        }

        public IEnumerable<TransportRequisition> Get(
            Expression<Func<TransportRequisition, bool>> filter = null,
            Func<IQueryable<TransportRequisition>, IOrderedQueryable<TransportRequisition>> orderBy = null,
            string includeProperties = "")
        {
            return _unitOfWork.TransportRequisitionRepository.Get(filter, orderBy, includeProperties);
        }
        #endregion

        public TransportRequisition CreateTransportRequisition(List<int> reliefRequisitions)
        {

            if (reliefRequisitions.Count < 1) return null;

            var transportRequisition = new TransportRequisition()
                                           {
                                               Status = 1,//Draft
                                               RequestedDate = DateTime.Today,
                                               RequestedBy = 1, //should be current user
                                               CertifiedBy = 1,//Should be some user
                                               CertifiedDate = DateTime.Today,//should be date cerified
                                               TransportRequisitionNo = Guid.NewGuid().ToString(),

                                           };

            foreach (var reliefRequisition in reliefRequisitions)
            {
                transportRequisition.TransportRequisitionDetails.Add(new TransportRequisitionDetail { RequisitionID = reliefRequisition });
                var orignal =
                    _unitOfWork.ReliefRequisitionRepository.Get(t => t.RequisitionID == reliefRequisition).FirstOrDefault();
                orignal.Status = (int)ReliefRequisitionStatus.TransportRequisitionCreated;
            }

            AddTransportRequisition(transportRequisition);
            transportRequisition.TransportRequisitionNo = string.Format("TRN-{0}",
                                                                        transportRequisition.TransportRequisitionID);
          
            _unitOfWork.Save();
            return transportRequisition;


        }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }


        public IEnumerable<RequisitionToDispatch> GetRequisitionToDispatch()
        {

            var requisitions = GetProjectCodeAssignedRequisitions();
            var result = new List<RequisitionToDispatch>();
            foreach (var requisition in requisitions)
            {
                var requisitionToDispatch = new RequisitionToDispatch();
                var hubAllocation =
                    _unitOfWork.HubAllocationRepository.Get(t => t.RequisitionID == requisition.RequisitionID,null,"Hub").FirstOrDefault();
                var status = _unitOfWork.WorkflowStatusRepository.Get(
                    t => t.StatusID == requisition.Status && t.WorkflowID == (int)WORKFLOW.RELIEF_REQUISITION).FirstOrDefault();

                requisitionToDispatch.HubID = hubAllocation.HubID;
                requisitionToDispatch.RequisitionID = requisition.RequisitionID;
                requisitionToDispatch.RequisitionNo = requisition.RequisitionNo;
                requisitionToDispatch.RequisitionStatus = requisition.Status.Value;
                requisitionToDispatch.ZoneID = requisition.ZoneID.Value;
                requisitionToDispatch.QuanityInQtl = requisition.ReliefRequisitionDetails.Sum(m => m.Amount);
                requisitionToDispatch.OrignWarehouse = hubAllocation.Hub.Name;
                requisitionToDispatch.CommodityID = requisition.CommodityID.Value;
                requisitionToDispatch.CommodityName = requisition.Commodity.Name;
                requisitionToDispatch.Zone = requisition.AdminUnit.Name;
                requisitionToDispatch.RegionID = requisition.RegionID.Value;

                requisitionToDispatch.RegionName = requisition.AdminUnit1.Name;
                requisitionToDispatch.RequisitionStatusName = status.Description;
               result.Add(requisitionToDispatch);
            }


            return result;
        }

        public IEnumerable<ReliefRequisition> GetProjectCodeAssignedRequisitions()
        {
            return _unitOfWork.ReliefRequisitionRepository.Get(t => t.Status == (int)ReliefRequisitionStatus.ProjectCodeAssigned, null,
                                                          "ReliefRequisitionDetails,Program,AdminUnit1,AdminUnit,Commodity");
        }


        public bool ApproveTransportRequisition(int id)
        {
            var transportRequisition =
                _unitOfWork.TransportRequisitionRepository.FindById(id);
            if(transportRequisition==null) return false;
            
            transportRequisition.Status = (int) TransportRequisitionStatus.Approved;
            _unitOfWork.Save();
            return true;
        }


        public List<RequisitionToDispatch> GetTransportRequisitionDetail(List<int> requIds)
        {
            var result = new List<RequisitionToDispatch>();
            foreach (var requId in requIds)
            {
                var requisition = _unitOfWork.ReliefRequisitionRepository.FindById(requId);
                var requisitionToDispatch = new RequisitionToDispatch();
                var hubAllocation =
                    _unitOfWork.HubAllocationRepository.Get(t => t.RequisitionID == requisition.RequisitionID, null,
                                                            "Hub").FirstOrDefault();
                var status = _unitOfWork.WorkflowStatusRepository.Get(
                    t => t.StatusID == requisition.Status && t.WorkflowID == (int)WORKFLOW.RELIEF_REQUISITION).FirstOrDefault();

                requisitionToDispatch.HubID = hubAllocation.HubID;
                requisitionToDispatch.RequisitionID = requisition.RequisitionID;
                requisitionToDispatch.RequisitionNo = requisition.RequisitionNo;
                requisitionToDispatch.RequisitionStatus = requisition.Status.Value;
                requisitionToDispatch.ZoneID = requisition.ZoneID.Value;
                requisitionToDispatch.QuanityInQtl = requisition.ReliefRequisitionDetails.Sum(m => m.Amount);
                requisitionToDispatch.OrignWarehouse = hubAllocation.Hub.Name;
                requisitionToDispatch.CommodityID = requisition.CommodityID.Value;
                requisitionToDispatch.CommodityName = requisition.Commodity.Name;
                requisitionToDispatch.Zone = requisition.AdminUnit1.Name;
                requisitionToDispatch.RegionID = requisition.RegionID.Value;

                requisitionToDispatch.RegionName = requisition.AdminUnit.Name;
                requisitionToDispatch.RequisitionStatusName = status.Description;
                result.Add(requisitionToDispatch);
            }
            return result;
        }
    }
}


