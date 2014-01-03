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
   public class TemplateTypeService:ITemplateTypeTypeService
    {
       private readonly IUnitOfWork _unitOfWork;

        public TemplateTypeService()
        {
            if (null == _unitOfWork)
                this._unitOfWork = new UnitOfWork();
        }

        public TemplateTypeService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

       public bool AddTemplateType(TemplateType templateType)
       {
           _unitOfWork.TemplateTypeRepository.Add(templateType);
           _unitOfWork.Save();
           return true;
       }

       public bool DeleteTemplateType(TemplateType templateType)
       {
           if (templateType == null) return false;
           _unitOfWork.TemplateTypeRepository.Delete(templateType);
           _unitOfWork.Save();
           return true;
       }

       public bool DeleteById(int id)
       {
           var entity = _unitOfWork.TemplateTypeRepository.FindById(id);
           if (entity == null) return false;
           _unitOfWork.TemplateTypeRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }

       public bool EditTemplateType(TemplateType templateType)
       {
           _unitOfWork.TemplateTypeRepository.Edit(templateType);
           _unitOfWork.Save();
           return true;
       }

       public TemplateType FindById(int id)
       {
           return _unitOfWork.TemplateTypeRepository.FindById(id);
       }

       public List<TemplateType> GetAllTemplateType()
       {
           return _unitOfWork.TemplateTypeRepository.GetAll();
       }

       public List<TemplateType> FindBy(Expression<Func<TemplateType, bool>> predicate)
       {
           return _unitOfWork.TemplateTypeRepository.FindBy(predicate);
       }
    }
}
