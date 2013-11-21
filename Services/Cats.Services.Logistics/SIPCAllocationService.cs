using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public class SIPCAllocationService:ISIPCAllocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SIPCAllocationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region Default Service Implementation

        public bool Create(SIPCAllocation allocation)
        {
            _unitOfWork.SIPCAllocationRepository.Add(allocation);
            _unitOfWork.Save();
            try
            {
//                _unitOfWork.SIPCAllocationRepository.Add(allocation);
 //               _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {

            }
            return false;
        }

        public SIPCAllocation FindById(int id)
        {
            return _unitOfWork.SIPCAllocationRepository.FindById(id);
        }
        public List<SIPCAllocation> GetAll()
        {
            return _unitOfWork.SIPCAllocationRepository.GetAll();
        }

        public List<SIPCAllocation> FindBy(Expression<Func<SIPCAllocation, bool>> predicate)
        {
            return _unitOfWork.SIPCAllocationRepository.FindBy(predicate);
        }

        public bool Update(SIPCAllocation allocation)
        {
            try
            {
                _unitOfWork.SIPCAllocationRepository.Edit(allocation);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {

            }
            return false;
            
        }

        public bool Delete(SIPCAllocation allocation)
        {
            try
            {
                _unitOfWork.SIPCAllocationRepository.Delete(allocation);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {

            }
            return false;
        }
        public bool DeleteById(int id)
        {
            SIPCAllocation allocation = _unitOfWork.SIPCAllocationRepository.FindById(id);
            return Delete(allocation);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}
