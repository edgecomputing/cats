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


            }

            AddTransportRequisition(transportRequisition);
            return transportRequisition;


        }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }


        public IEnumerable<RequisitionToDispatch> GetRequisitionToDispatch()
        {
            
            var requisitions = GetProjectCodeAssignedRequisitions();

            var result = (from requisition in requisitions
                          select new RequisitionToDispatch
                          {

                              HubID =requisition.HubAllocations.FirstOrDefault().HubID, //_unitOfWork.HubAllocationRepository.FindBy(t=>t.RequisitionID==requisition.RequisitionID).First().HubID,

                              RequisitionID = requisition.RequisitionID,
                              RequisitionNo = requisition.RequisitionNo,
                              RequisitionStatus = requisition.Status.Value,
                              ZoneID = requisition.ZoneID.Value,
                              QuanityInQtl = requisition.ReliefRequisitionDetails.Sum(m => m.Amount),
                              OrignWarehouse = requisition.HubAllocations.FirstOrDefault().Hub.Name,//_unitOfWork.HubAllocationRepository.FindBy(t => t.RequisitionID == requisition.RequisitionID).First().Hub.Name,
                              CommodityID = requisition.CommodityID.Value,
                              CommodityName = requisition.Commodity.Name,
                              Zone = requisition.AdminUnit.Name,
                              RegionID = requisition.RegionID.Value,

                              RegionName = requisition.AdminUnit1.Name,
                              //RequisitionStatusName=_unitOfWork.WorkflowStatusRepository.FindBy(t=>t.StatusID==requisition.Status && t.WorkflowID==2).FirstOrDefault().Description 



                          });


            return result;
        }

        public IEnumerable<ReliefRequisition> GetProjectCodeAssignedRequisitions()
        {
            return _unitOfWork.ReliefRequisitionRepository.Get(t => t.Status == (int)ReliefRequisitionStatus.ProjectCodeAssigned, null,
                                                          "HubAllocations,HubAllocations.Hub,ReliefRequisitionDetails,Program,AdminUnit1,AdminUnit,Commodity");
        }
    }
}


