

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

        public bool UpdateRequisitionStatus(string requisitionNo)
        {
          var entity =  _unitOfWork.ReliefRequisitionRepository.FindBy(r => r.RequisitionNo == requisitionNo).SingleOrDefault();
            if (entity==null) return false;
            entity.Status = 4;//hub allocated
            _unitOfWork.Save();
            return true;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


