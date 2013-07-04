using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public class DispatchAllocationDetailService : IDispatchAllocationDetailService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DispatchAllocationDetailService()
        {
            this._unitOfWork = new UnitOfWork();
        }
      

        #region Implementation of Service
        public bool AddDispatchDetail(DispatchAllocation _dispatchAllocationDetail)
        {
            _unitOfWork.DispatchAllocationRepository.Add(_dispatchAllocationDetail);
            _unitOfWork.Save();
            return true;
        }
        public bool EditDispatchDetail(DispatchAllocation _dispatchAllocationDetail)
        {
            _unitOfWork.DispatchAllocationRepository.Edit(_dispatchAllocationDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteDispatchDetail(DispatchAllocation _dispatchAllocationDetail)
        {
            if (_dispatchAllocationDetail == null) return false;
            _unitOfWork.DispatchAllocationRepository.Delete(_dispatchAllocationDetail);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DispatchAllocationRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DispatchAllocationRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool Save()
        {
            _unitOfWork.Save();
            return true;
        }

        public List<DispatchAllocation> GetAllDispatchAllocationDetail()
        {
            return _unitOfWork.DispatchAllocationRepository.GetAll();
        }
        public DispatchAllocation FindById(Guid id)
        {
            return _unitOfWork.DispatchAllocationRepository.FindById(id);
        }
        public IEnumerable<DispatchAllocation> FindBy(Expression<Func<DispatchAllocation, bool>> predicate)
        {
            return _unitOfWork.DispatchAllocationRepository.FindBy(predicate);
        }
 
        #endregion


        public void Dispose()
        {
            _unitOfWork.Dispose();

        }


        public bool SaveProjectAllocation(IEnumerable<DispatchAllocation> dispatchAllocations)
        {
            try{
                    foreach (var item in dispatchAllocations)
                    {
                        var tempDispatchAllocation=FindById(item.DispatchAllocationID);
                            tempDispatchAllocation.ProjectCodeID=item.ProjectCodeID;
                            tempDispatchAllocation.ShippingInstructionID=item.ShippingInstructionID;
                            this.EditDispatchDetail(tempDispatchAllocation);
                    }
                    return true;
                }
                catch
                    {
                        return false;
                    }
            
        }
    }
}
