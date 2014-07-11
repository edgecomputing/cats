using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;
using Cats.Data.UnitWork;

namespace Cats.Services.EarlyWarning
{
    public class BusinessProcessStateService :IBusinessProcessStateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BusinessProcessStateService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool Add(BusinessProcessState item)
        {
            _unitOfWork.BusinessProcessStateRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }
        public bool Update(BusinessProcessState item)
        {
            if (item == null) return false;
            _unitOfWork.BusinessProcessStateRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }
        public bool Delete(BusinessProcessState item)
        {
            if (item == null) return false;
            _unitOfWork.BusinessProcessStateRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var item = _unitOfWork.BusinessProcessStateRepository.FindById(id);
            return Delete(item);
        }
        public BusinessProcessState FindById(int id)
        {
            return _unitOfWork.BusinessProcessStateRepository.FindById(id);
        }
        public List<BusinessProcessState> GetAll()
        {
            return _unitOfWork.BusinessProcessStateRepository.GetAll();

        }
        public List<BusinessProcessState> FindBy(Expression<Func<BusinessProcessState, bool>> predicate)
        {
            return _unitOfWork.BusinessProcessStateRepository.FindBy(predicate);

        }
   }
 }