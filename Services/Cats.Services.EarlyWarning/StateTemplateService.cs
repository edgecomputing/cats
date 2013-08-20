using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;
using Cats.Data.UnitWork;

namespace Cats.Services.EarlyWarning
{
    public class StateTemplateService : IStateTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StateTemplateService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool Add(StateTemplate item)
        {
            _unitOfWork.StateTemplateRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }
        public bool Update(StateTemplate item)
        {
            if (item == null) return false;
            _unitOfWork.StateTemplateRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }
        public bool Delete(StateTemplate item)
        {
            if (item == null) return false;
            _unitOfWork.StateTemplateRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var item = _unitOfWork.StateTemplateRepository.FindById(id);
            return Delete(item);
        }
        public StateTemplate FindById(int id)
        {
            return _unitOfWork.StateTemplateRepository.FindById(id);
        }
        public List<StateTemplate> GetAll()
        {
            return _unitOfWork.StateTemplateRepository.GetAll();

        }
        public List<StateTemplate> FindBy(Expression<Func<StateTemplate, bool>> predicate)
        {
            return _unitOfWork.StateTemplateRepository.FindBy(predicate);

        }
    }
}