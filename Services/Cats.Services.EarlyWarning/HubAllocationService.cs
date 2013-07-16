

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;
using Cats.Data.UnitWork;
using System.Linq;

namespace Cats.Services.EarlyWarning
{

    public class HubAllocationService : IHubAllocationService
    {
        private readonly IUnitOfWork _unitOfWork;


        public HubAllocationService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddHubAllocation(HubAllocation hubAllocation)
        {
            _unitOfWork.HubAllocationRepository.Add(hubAllocation);
            var requ =
                _unitOfWork.ReliefRequisitionRepository.Get(t => t.RequisitionID == hubAllocation.RequisitionID).
                    FirstOrDefault();
            requ.Status = 3;

            _unitOfWork.Save();
            return true;

        }
        public bool EditHubAllocation(HubAllocation hubAllocation)
        {
            _unitOfWork.HubAllocationRepository.Edit(hubAllocation);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteHubAllocation(HubAllocation hubAllocation)
        {
            if (hubAllocation == null) return false;
            _unitOfWork.HubAllocationRepository.Delete(hubAllocation);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.HubAllocationRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.HubAllocationRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<HubAllocation> GetAllHubAllocation()
        {
            return _unitOfWork.HubAllocationRepository.GetAll();
        }
        public HubAllocation FindById(int id)
        {
            return _unitOfWork.HubAllocationRepository.FindById(id);
        }
        public List<HubAllocation> FindBy(Expression<Func<HubAllocation, bool>> predicate)
        {
            return _unitOfWork.HubAllocationRepository.FindBy(predicate);
        }
        #endregion

        public bool UpdateRequisitionStatus(int requisitionId)
        {
            var requisition =_unitOfWork.ReliefRequisitionRepository.FindBy(r => r.RequisitionID == requisitionId).SingleOrDefault();
            if (requisition == null) return false;
            requisition.Status = 3;
            _unitOfWork.Save();
            return true;



        }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


