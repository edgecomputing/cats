using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;
using Cats.Data.UnitWork;

namespace Cats.Services.EarlyWarning
{
    public class ProcessTemplateService : IProcessTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProcessTemplateService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool Add(ProcessTemplate item)
        {
            _unitOfWork.ProcessTemplateRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }
        public bool Update(ProcessTemplate item)
        {
            if (item == null) return false;
            _unitOfWork.ProcessTemplateRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }
        public bool Delete(ProcessTemplate item)
        {
            if (item == null) return false;
            _unitOfWork.ProcessTemplateRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var item = _unitOfWork.ProcessTemplateRepository.FindById(id);
            return Delete(item);
        }
        public ProcessTemplate FindById(int id)
        {
            return _unitOfWork.ProcessTemplateRepository.FindById(id);
        }
        public List<ProcessTemplate> GetAll()
        {
            return _unitOfWork.ProcessTemplateRepository.GetAll();

        }
        public List<ProcessTemplate> FindBy(Expression<Func<ProcessTemplate, bool>> predicate)
        {
            return _unitOfWork.ProcessTemplateRepository.FindBy(predicate);

        }
        public StateTemplate GetStartingState(int id)
        {
            return _unitOfWork.StateTemplateRepository.FindBy(s=>s.ParentProcessTemplateID==id && s.StateType==0).Single();

        }

    }
}