using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;
using Cats.Data.UnitWork;

namespace Cats.Services.EarlyWarning
{
    public class FlowTemplateService : IFlowTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FlowTemplateService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool Add(FlowTemplate item)
        {
            _unitOfWork.FlowTemplateRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }
        public bool Update(FlowTemplate item)
        {
            if (item == null) return false;
            _unitOfWork.FlowTemplateRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }
        public bool Delete(FlowTemplate item)
        {
            if (item == null) return false;
            _unitOfWork.FlowTemplateRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var item = _unitOfWork.FlowTemplateRepository.FindById(id);
            return Delete(item);
        }
        public FlowTemplate FindById(int id)
        {
            return _unitOfWork.FlowTemplateRepository.FindById(id);
        }
        public List<FlowTemplate> GetAll()
        {
            return _unitOfWork.FlowTemplateRepository.GetAll();

        }
        public List<FlowTemplate> FindBy(Expression<Func<FlowTemplate, bool>> predicate)
        {
            return _unitOfWork.FlowTemplateRepository.FindBy(predicate);

        }
    }
}