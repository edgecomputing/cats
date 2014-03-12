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
    public class TemplateService : ITemplateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TemplateService()
        {
            if (null == _unitOfWork)
                this._unitOfWork = new UnitOfWork();
        }

        public TemplateService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool AddTemplate(Template template)
        {
            _unitOfWork.TemplateRepository.Add(template);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteTemplate(Template template)
        {
            if (template == null) return false;
            _unitOfWork.TemplateRepository.Delete(template);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.TemplateRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.TemplateRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditTemplate(Template template)
        {
            _unitOfWork.TemplateRepository.Edit(template);
            _unitOfWork.Save();
            return true;
        }

        public Template FindById(int id)
        {
            return _unitOfWork.TemplateRepository.FindById(id);
        }

        public List<Template> GetAllTemplate()
        {
            return _unitOfWork.TemplateRepository.GetAll();
        }

        public List<Template> FindBy(Expression<Func<Template, bool>> predicate)
        {
            return _unitOfWork.TemplateRepository.FindBy(predicate);
        }
    }
}
