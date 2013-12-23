using System;
using System.Collections.Generic;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Administration
{
    class ActionTypesService:IActionTypesService
    {
         private readonly IUnitOfWork _unitOfWork;


         public ActionTypesService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool AddActionType(Models.ActionTypes actionType)
        {
            _unitOfWork.ActionTypesRepository.Add(actionType);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteActionType(Models.ActionTypes actionType)
        {
            if (actionType == null) return false;
            _unitOfWork.ActionTypesRepository.Delete(actionType);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ActionTypesRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ActionTypesRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditActionType(Models.ActionTypes actionType)
        {
            _unitOfWork.ActionTypesRepository.Edit(actionType);
            _unitOfWork.Save();
            return true;
        }

        public Models.ActionTypes FindById(int id)
        {
            return _unitOfWork.ActionTypesRepository.FindById(id);
        }

        public List<Models.ActionTypes> GetAllActionType()
        {
            return _unitOfWork.ActionTypesRepository.GetAll();
        }

        public List<Models.ActionTypes> FindBy(System.Linq.Expressions.Expression<Func<Models.ActionTypes, bool>> predicate)
        {
            return _unitOfWork.ActionTypesRepository.FindBy(predicate);
        }
    }
}
