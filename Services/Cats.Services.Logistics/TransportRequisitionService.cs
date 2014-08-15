using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.Logistics;
using Cats.Services.Common;

namespace Cats.Services.Logistics
{
    public class TransportRequisitionService : ITransportRequisitionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;

        public TransportRequisitionService(IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            this._unitOfWork = unitOfWork;
            _notificationService = notificationService;
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

        public bool CreateTransportRequisition(List<List<int>> programRequisitons,int requestedBy)
        {
            if(programRequisitons.Count < 1) return false;
            foreach (var reliefRequisitions in programRequisitons)
            {


                if (reliefRequisitions.Count < 1) break;
                var anyReliefRequisition =
                    _unitOfWork.ReliefRequisitionRepository.FindById(reliefRequisitions.ElementAt(0));
                var region = new AdminUnit();
                if (anyReliefRequisition.RegionID != null)
                {
                    region = _unitOfWork.AdminUnitRepository.FindById(anyReliefRequisition.RegionID.Value);
                }
                var program = new Program();
                
                if (anyReliefRequisition.ProgramID != null)
                {
                    program = _unitOfWork.ProgramRepository.FindById(anyReliefRequisition.ProgramID);
                }
                
                var transportRequisition = new TransportRequisition()
                                               {
                                                   Status = (int)TransportRequisitionStatus.Draft, //Draft
                                                   RequestedDate = DateTime.Today,
                                                   RequestedBy = requestedBy, //should be current user
                                                   CertifiedBy = requestedBy, //Should be some user ????
                                                   CertifiedDate = DateTime.Today, //should be date cerified
                                                   TransportRequisitionNo = Guid.NewGuid().ToString(),
                                                   RegionID = region.AdminUnitID,
                                                   ProgramID = program.ProgramID
                                               };

                foreach (var reliefRequisition in reliefRequisitions)
                {
                    transportRequisition.TransportRequisitionDetails.Add(new TransportRequisitionDetail
                                                                             {RequisitionID = reliefRequisition});
                    var orignal =
                        _unitOfWork.ReliefRequisitionRepository.Get(t => t.RequisitionID == reliefRequisition).
                            FirstOrDefault();
                    orignal.Status = (int) ReliefRequisitionStatus.TransportRequisitionCreated;
                }

                AddTransportRequisition(transportRequisition);
                
                var year = transportRequisition.RequestedDate.Year;
                transportRequisition.TransportRequisitionNo = string.Format("{0}/{1}/{2}/{3}",
                                                                            program.ShortCode, region.AdminUnitID,
                                                                            transportRequisition.TransportRequisitionID,
                                                                            year);

                _unitOfWork.Save();
               
            }
            return true;


        }


        private void AddToNotification(TransportRequisition transportRequisition)
        {
            if (HttpContext.Current == null) return;
            string destinationURl;
            if (HttpContext.Current.Request.Url.Host == "localhost")
            {
                destinationURl = "http://" + HttpContext.Current.Request.Url.Authority +
                                 "/Procurement/TransportOrder/NotificationNewRequisitions?recordId=" + transportRequisition.TransportRequisitionID;
                return;
            }
            destinationURl = "http://" + HttpContext.Current.Request.Url.Authority +
                             HttpContext.Current.Request.ApplicationPath +
                             "/Procurement/TransportOrder/NotificationNewRequisitions?recordId=" + transportRequisition.TransportRequisitionID;
            _notificationService.AddNotificationForProcurementFromLogistics(destinationURl, transportRequisition);
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


        public bool ApproveTransportRequisition(int id,int approvedBy)
        {
            var transportRequisition =
                _unitOfWork.TransportRequisitionRepository.FindById(id);
            if(transportRequisition==null) return false;
            
            transportRequisition.Status = (int) TransportRequisitionStatus.Approved;
            transportRequisition.CertifiedBy = approvedBy;
            transportRequisition.CertifiedDate = DateTime.Today;
            _unitOfWork.Save();
            //calling the notification 
            AddToNotification(transportRequisition);
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

                if (hubAllocation != null) requisitionToDispatch.HubID = hubAllocation.HubID;
                requisitionToDispatch.RequisitionID = requisition.RequisitionID;
                requisitionToDispatch.RequisitionNo = requisition.RequisitionNo;
                if (requisition.Status != null) requisitionToDispatch.RequisitionStatus = requisition.Status.Value;
                if (requisition.ZoneID != null) requisitionToDispatch.ZoneID = requisition.ZoneID.Value;
                requisitionToDispatch.QuanityInQtl = requisition.ReliefRequisitionDetails.Sum(m => m.Amount);
                if (hubAllocation != null) requisitionToDispatch.OrignWarehouse = hubAllocation.Hub.Name;
                if (requisition.CommodityID != null) requisitionToDispatch.CommodityID = requisition.CommodityID.Value;
                requisitionToDispatch.CommodityName = requisition.Commodity.Name;
                requisitionToDispatch.Zone = requisition.AdminUnit1.Name;
                if (requisition.RegionID != null) requisitionToDispatch.RegionID = requisition.RegionID.Value;
                requisitionToDispatch.ProgramID = requisition.ProgramID;
                requisitionToDispatch.Program = requisition.Program.Name;
                requisitionToDispatch.RegionName = requisition.AdminUnit.Name;
                if (status != null) requisitionToDispatch.RequisitionStatusName = status.Description;
                result.Add(requisitionToDispatch);
            }
            return result;
        }
        public List<TransportRequisitionDetail> GetTransportRequsitionDetails(int programId)
        {
            return
                _unitOfWork.TransportRequisitionDetailRepository.FindBy(
                    m => m.TransportRequisition.ProgramID == programId
                         && m.TransportRequisition.Status == (int) TransportRequisitionStatus.Closed).ToList();
        }
        public List<TransportRequisitionDetail> GetTransportRequsitionDetails()
        {
            return
                _unitOfWork.TransportRequisitionDetailRepository.FindBy(
                    m => m.TransportRequisition.Status == (int)TransportRequisitionStatus.Closed).ToList();
        }
    }
}


