using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public class TemplateFieldService:ITemplateFieldsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TemplateFieldService()
        {
            if (null == _unitOfWork)
                this._unitOfWork = new UnitOfWork();
        }

        public TemplateFieldService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool AddTemplateField(TemplateField templateFields)
        {
            _unitOfWork.TemplateFieldRepository.Add(templateFields);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteTemplateField(TemplateField templateFields)
        {
            if (templateFields == null) return false;
            _unitOfWork.TemplateFieldRepository.Delete(templateFields);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.TemplateFieldRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.TemplateFieldRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditTemplateField(TemplateField templateFields)
        {
            _unitOfWork.TemplateFieldRepository.Edit(templateFields);
            _unitOfWork.Save();
            return true;
        }

        public TemplateField FindById(int id)
        {
            return _unitOfWork.TemplateFieldRepository.FindById(id);
        }

        public List<TemplateField> GetAllTemplateField()
        {
            return _unitOfWork.TemplateFieldRepository.GetAll();
        }

        public List<TemplateField> FindBy(Expression<Func<TemplateField, bool>> predicate)
        {
            return _unitOfWork.TemplateFieldRepository.FindBy(predicate);
        }
    }
}
