

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using System.Linq;
using Cats.Data.Hub;
using Cats.Data.Hub.UnitWork;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels;
using Cats.Models.Hubs.ViewModels.Dispatch;

namespace Cats.Services.Hub
{

    public class OtherDispatchAllocationService : IOtherDispatchAllocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectCodeService _projectCodeService;
        private readonly IShippingInstructionService _shippingInstructionService;


        public OtherDispatchAllocationService(IUnitOfWork unitOfWork, IProjectCodeService projectCodeService, IShippingInstructionService ShippingInstructionService)
        {
            this._unitOfWork = unitOfWork;
            this._projectCodeService=projectCodeService;
            this._shippingInstructionService = ShippingInstructionService;
        }
        #region Default Service Implementation
        public bool AddOtherDispatchAllocation(OtherDispatchAllocation otherDispatchAllocation)
        {
            _unitOfWork.OtherDispatchAllocationRepository.Add(otherDispatchAllocation);
            _unitOfWork.Save();
            return true;

        }
        public bool EditOtherDispatchAllocation(OtherDispatchAllocation otherDispatchAllocation)
        {
            _unitOfWork.OtherDispatchAllocationRepository.Edit(otherDispatchAllocation);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteOtherDispatchAllocation(OtherDispatchAllocation otherDispatchAllocation)
        {
            if (otherDispatchAllocation == null) return false;
            _unitOfWork.OtherDispatchAllocationRepository.Delete(otherDispatchAllocation);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.OtherDispatchAllocationRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.OtherDispatchAllocationRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<OtherDispatchAllocation> GetAllOtherDispatchAllocation()
        {
            return _unitOfWork.OtherDispatchAllocationRepository.GetAll();
        }
        public OtherDispatchAllocation FindById(int id)
        {
            return _unitOfWork.OtherDispatchAllocationRepository.FindById(id);
        }
        public OtherDispatchAllocation FindById(Guid id)
        {
            return _unitOfWork.OtherDispatchAllocationRepository.FindBy(t => t.OtherDispatchAllocationID == id).FirstOrDefault();
        }


        public List<OtherDispatchAllocation> FindBy(Expression<Func<OtherDispatchAllocation, bool>> predicate)
        {
            return _unitOfWork.OtherDispatchAllocationRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }



        public void Save(Models.Hubs.ViewModels.Dispatch.OtherDispatchAllocationViewModel model)
        {
            OtherDispatchAllocation oAllocation = new OtherDispatchAllocation();
            if (model.OtherDispatchAllocationID != null)
            {

                oAllocation = _unitOfWork.OtherDispatchAllocationRepository.FindById((Guid)model.OtherDispatchAllocationID);

                oAllocation.OtherDispatchAllocationID = model.OtherDispatchAllocationID.Value;
                oAllocation.ProgramID = model.ProgramID.Value;
                oAllocation.HubID = model.FromHubID.Value;
                oAllocation.ToHubID = model.ToHubID.Value;
                oAllocation.ReasonID = model.ReasonID;
                oAllocation.ReferenceNumber = model.ReferenceNumber;
                oAllocation.AgreementDate = model.AgreementDate;
                oAllocation.CommodityID = model.CommodityID;
                oAllocation.EstimatedDispatchDate = model.EstimatedDispatchDate;
                oAllocation.IsClosed = model.IsClosed;
                oAllocation.ProjectCodeID =  _projectCodeService.GetProjectCodeId(model.ProjectCode);
                oAllocation.ShippingInstructionID =
                    _shippingInstructionService.GetShipingInstructionId(model.ShippingInstruction);
                oAllocation.UnitID = model.UnitID;
                oAllocation.QuantityInUnit = model.QuantityInUnit;
                oAllocation.QuantityInMT = model.QuantityInMT;
                oAllocation.QuantityInUnit = model.QuantityInUnit;
                oAllocation.Remark = model.Remark;
                //Modify Banty :From SaveChanges(oAllocation) to SaveChanges()
               // repository.OtherDispatchAllocation.SaveChanges(oAllocation);
                _unitOfWork.Save();

            }
            else
            {
                oAllocation.PartitionID = (model.PartitionID.HasValue) ? model.PartitionID.Value : 0;
                if (model.OtherDispatchAllocationID.HasValue)
                {
                    oAllocation.OtherDispatchAllocationID = model.OtherDispatchAllocationID.Value;
                }
                oAllocation.ProgramID = model.ProgramID.Value;
                oAllocation.HubID = model.FromHubID.Value;
                oAllocation.ToHubID = model.ToHubID.Value;
                oAllocation.ReasonID = model.ReasonID;
                oAllocation.ReferenceNumber = model.ReferenceNumber;
                oAllocation.AgreementDate = model.AgreementDate;
                oAllocation.CommodityID = model.CommodityID;
                oAllocation.EstimatedDispatchDate = model.EstimatedDispatchDate;
                oAllocation.IsClosed = model.IsClosed;
                oAllocation.ProjectCodeID = _projectCodeService.GetProjectCodeId(model.ProjectCode);
                oAllocation.ShippingInstructionID =
                    _shippingInstructionService.GetShipingInstructionId(model.ShippingInstruction);
                oAllocation.UnitID = model.UnitID;
                oAllocation.QuantityInUnit = model.QuantityInUnit;
                oAllocation.QuantityInMT = model.QuantityInMT;
                oAllocation.QuantityInUnit = model.QuantityInUnit;
                oAllocation.Remark = model.Remark;
                _unitOfWork.OtherDispatchAllocationRepository.Add(oAllocation);//
                _unitOfWork.Save();
                
            }

        }


        public OtherDispatchAllocationViewModel GetViewModelByID(Guid otherDispatchAllocationId)
        {
            var  OtherDispach =  _unitOfWork.OtherDispatchAllocationRepository.Get();
            var model = (from v in OtherDispach
                         where v.OtherDispatchAllocationID == otherDispatchAllocationId
                         select v).FirstOrDefault();
            if (model != null)
            {
                var val = new OtherDispatchAllocationViewModel()
                {
                    PartitionID = 0,
                    OtherDispatchAllocationID = model.OtherDispatchAllocationID,
                    ProgramID = model.ProgramID,
                    FromHubID = model.HubID,
                    ToHubID = model.ToHubID,
                    ReasonID = model.ReasonID,
                    ReferenceNumber = model.ReferenceNumber,
                    AgreementDate = model.AgreementDate,
                    CommodityID = model.CommodityID,
                    EstimatedDispatchDate = model.EstimatedDispatchDate,
                    IsClosed = model.IsClosed,
                    ProjectCode = model.ProjectCode.Value,
                    ShippingInstruction = model.ShippingInstruction.Value,
                    UnitID = model.UnitID,
                    QuantityInUnit = model.QuantityInUnit,
                    QuantityInMT = model.QuantityInMT,
                    CommodityTypeID = model.Commodity.CommodityTypeID,
                    Remark = model.Remark,
                };
                return val;
            }
            return null;
        }


        public List<OtherDispatchAllocation> GetAllToCurrentOwnerHubs(UserProfile user)
        {
            return (from v in _unitOfWork.OtherDispatchAllocationRepository.Get()
                    where v.HubID == user.DefaultHub.HubID && v.Hub1.HubOwnerID == user.DefaultHub.HubOwnerID
                    select v
                   ).OrderByDescending(o => o.AgreementDate).ToList();
        }

        public List<OtherDispatchAllocation> GetAllToOtherOwnerHubs(UserProfile user)
        {
            return (from v in _unitOfWork.OtherDispatchAllocationRepository.Get()
                    where v.HubID == user.DefaultHub.HubID && v.Hub1.HubOwnerID != user.DefaultHub.HubOwnerID
                    select v
                   ).OrderByDescending(o => o.AgreementDate).ToList();
        }

        public List<OtherDispatchAllocationDto> GetCommitedLoanAllocationsDetached(UserProfile user, bool? closedToo, int? CommodityType)
        {
            return GetCommitedLoanAllocationsDetached(user, user.DefaultHub.HubID, closedToo, CommodityType);
        }
        public List<OtherDispatchAllocationDto> GetCommitedLoanAllocationsDetached(UserProfile user,int hubId, bool? closedToo, int? CommodityType)
        {

            List<OtherDispatchAllocationDto> LoanList = new List<OtherDispatchAllocationDto>();

            var Loans = (from v in _unitOfWork.OtherDispatchAllocationRepository.Get()
                         where v.HubID == hubId
                        // where v.HubID == user.DefaultHub.HubID && v.Hub1.HubOwnerID != user.DefaultHub.HubOwnerID
                         select v
                        );


            if (closedToo == null || closedToo == false)
            {
                Loans = Loans.Where(p => p.IsClosed == false);
            }
            else
            {
                Loans = Loans.Where(p => p.IsClosed == true);
            }

            if (CommodityType.HasValue)
            {
                Loans = Loans.Where(p => p.Commodity.CommodityTypeID == CommodityType.Value);
            }
            else
            {
                Loans = Loans.Where(p => p.Commodity.CommodityTypeID == 1);//by default
            }

            foreach (var otherDispatchAllocation in Loans)
            {
                var loan = new OtherDispatchAllocationDto();

                loan.OtherDispatchAllocationID = otherDispatchAllocation.OtherDispatchAllocationID;
                loan.ReferenceNumber = otherDispatchAllocation.ReferenceNumber;
                loan.CommodityName = otherDispatchAllocation.Commodity.Name;
                loan.EstimatedDispatchDate = otherDispatchAllocation.EstimatedDispatchDate;
                loan.AgreementDate = otherDispatchAllocation.AgreementDate;
                loan.SINumber = otherDispatchAllocation.ShippingInstruction.Value;
                loan.ProjectCode = otherDispatchAllocation.ProjectCode.Value;
                loan.IsClosed = otherDispatchAllocation.IsClosed;

                loan.QuantityInUnit = otherDispatchAllocation.QuantityInUnit;
                loan.RemainingQuantityInUnit = otherDispatchAllocation.QuantityInUnit;

                if (user.PreferedWeightMeasurment.ToUpperInvariant() == "QN")
                {
                    loan.QuantityInMT = Math.Abs(otherDispatchAllocation.QuantityInMT) * 10;
                    loan.RemainingQuantityInMt = (otherDispatchAllocation.RemainingQuantityInMt) * 10;
                }
                else
                {
                    loan.QuantityInMT = Math.Abs(otherDispatchAllocation.QuantityInMT);
                    loan.RemainingQuantityInMt = (otherDispatchAllocation.RemainingQuantityInMt);
                }
                LoanList.Add(loan);
            }
            return LoanList;
        }

        public List<OtherDispatchAllocationDto> GetCommitedTransferAllocationsDetached(UserProfile user, bool? closedToo, int? CommodityType)
        {
            return GetCommitedTransferAllocationsDetached(user,user.DefaultHub.HubID, closedToo, CommodityType);
        }
        public List<OtherDispatchAllocationDto> GetCommitedTransferAllocationsDetached(UserProfile user,int hubId, bool? closedToo, int? CommodityType)
        {
            List<OtherDispatchAllocationDto> TransferList = new List<OtherDispatchAllocationDto>();

            var Transafers = (from v in _unitOfWork.OtherDispatchAllocationRepository.Get()
                            //  where v.HubID == user.DefaultHub.HubID && v.Hub1.HubOwnerID == user.DefaultHub.HubOwnerID
                            where v.HubID ==hubId
                              select v
                        );


            if (closedToo == null || closedToo == false)
            {
                Transafers = Transafers.Where(p => p.IsClosed == false);
            }
            else
            {
                Transafers = Transafers.Where(p => p.IsClosed == true);
            }

            if (CommodityType.HasValue)
            {
                Transafers = Transafers.Where(p => p.Commodity.CommodityTypeID == CommodityType.Value);
            }
            else
            {
                Transafers = Transafers.Where(p => p.Commodity.CommodityTypeID == 1);//by default
            }

            foreach (var otherDispatchAllocation in Transafers)
            {
                var transfer = new OtherDispatchAllocationDto();

                transfer.OtherDispatchAllocationID = otherDispatchAllocation.OtherDispatchAllocationID;
                transfer.ReferenceNumber = otherDispatchAllocation.ReferenceNumber;
                transfer.CommodityName = otherDispatchAllocation.Commodity.Name;
                transfer.EstimatedDispatchDate = otherDispatchAllocation.EstimatedDispatchDate;
                transfer.AgreementDate = otherDispatchAllocation.AgreementDate;
                transfer.SINumber = otherDispatchAllocation.ShippingInstruction.Value;
                transfer.ProjectCode = otherDispatchAllocation.ProjectCode.Value;
                transfer.IsClosed = otherDispatchAllocation.IsClosed;

                transfer.QuantityInUnit = otherDispatchAllocation.QuantityInUnit;
                transfer.RemainingQuantityInUnit = otherDispatchAllocation.RemainingQuantityInUnit;

                if (user.PreferedWeightMeasurment.ToUpperInvariant() == "QN")
                {
                    transfer.QuantityInMT = Math.Abs(otherDispatchAllocation.QuantityInMT) * 10;
                    transfer.RemainingQuantityInMt = (otherDispatchAllocation.RemainingQuantityInMt) * 10;
                }
                else
                {
                    transfer.QuantityInMT = Math.Abs(otherDispatchAllocation.QuantityInMT);
                    transfer.RemainingQuantityInMt = (otherDispatchAllocation.RemainingQuantityInMt);
                }
                TransferList.Add(transfer);
            }
            return TransferList;
        }

        public void CloseById(Guid id)
        {
            var delAllocation = _unitOfWork.OtherDispatchAllocationRepository.Get().FirstOrDefault(allocation => allocation.OtherDispatchAllocationID == id);
            if (delAllocation != null) delAllocation.IsClosed = true;
            delAllocation.IsClosed = true;
          //  _unitOfWork.OtherDispatchAllocationRepository.Add(delAllocation);
            _unitOfWork.Save();
        }

       

    }
}


