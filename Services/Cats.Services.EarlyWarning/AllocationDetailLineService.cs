

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{

    public class AllocationDetailLineService : IAllocationDetailLineService
    {
        private readonly IUnitOfWork _unitOfWork;


        public AllocationDetailLineService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddAllocationDetailLine(AllocationDetailLine allocationDetailLine)
        {
            _unitOfWork.AllocationDetailLineRepository.Add(allocationDetailLine);
            _unitOfWork.Save();
            return true;

        }
        public bool EditAllocationDetailLine(AllocationDetailLine allocationDetailLine)
        {
            _unitOfWork.AllocationDetailLineRepository.Edit(allocationDetailLine);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteAllocationDetailLine(AllocationDetailLine allocationDetailLine)
        {
            if (allocationDetailLine == null) return false;
            _unitOfWork.AllocationDetailLineRepository.Delete(allocationDetailLine);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.AllocationDetailLineRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.AllocationDetailLineRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<AllocationDetailLine> GetAllAllocationDetailLine()
        {
            return _unitOfWork.AllocationDetailLineRepository.GetAll();
        }
        public AllocationDetailLine FindById(int id)
        {
            return _unitOfWork.AllocationDetailLineRepository.FindById(id);
        }
        public List<AllocationDetailLine> FindBy(Expression<Func<AllocationDetailLine, bool>> predicate)
        {
            return _unitOfWork.AllocationDetailLineRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


