

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hubs;


namespace Cats.Services.Hub
{

    public class  LetterTemplateService : ILetterTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;


        public  LetterTemplateService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddLetterTemplate( LetterTemplate letterTemplate)
        {
            _unitOfWork. LetterTemplateRepository.Add(letterTemplate);
            _unitOfWork.Save();
            return true;

        }
        public bool EditLetterTemplate( LetterTemplate letterTemplate)
        {
            _unitOfWork. LetterTemplateRepository.Edit(letterTemplate);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteLetterTemplate( LetterTemplate letterTemplate)
        {
            if (letterTemplate == null) return false;
            _unitOfWork. LetterTemplateRepository.Delete(letterTemplate);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork. LetterTemplateRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork. LetterTemplateRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List< LetterTemplate> GetAllLetterTemplate()
        {
            return _unitOfWork. LetterTemplateRepository.GetAll();
        }
        public  LetterTemplate FindById(int id)
        {
            return _unitOfWork. LetterTemplateRepository.FindById(id);
        }
        public List< LetterTemplate> FindBy(Expression<Func< LetterTemplate, bool>> predicate)
        {
            return _unitOfWork. LetterTemplateRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


