

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hubs;


namespace Cats.Services.Hub
{

    public class  ReleaseNoteService : IReleaseNoteService
    {
        private readonly IUnitOfWork _unitOfWork;


        public  ReleaseNoteService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddReleaseNote( ReleaseNote entity)
        {
            _unitOfWork. ReleaseNoteRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditReleaseNote( ReleaseNote entity)
        {
            _unitOfWork. ReleaseNoteRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteReleaseNote( ReleaseNote entity)
        {
            if (entity == null) return false;
            _unitOfWork. ReleaseNoteRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork. ReleaseNoteRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork. ReleaseNoteRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List< ReleaseNote> GetAllReleaseNote()
        {
            return _unitOfWork. ReleaseNoteRepository.GetAll();
        }
        public  ReleaseNote FindById(int id)
        {
            return _unitOfWork. ReleaseNoteRepository.FindById(id);
        }
        public List< ReleaseNote> FindBy(Expression<Func< ReleaseNote, bool>> predicate)
        {
            return _unitOfWork. ReleaseNoteRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


