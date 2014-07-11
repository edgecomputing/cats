

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Data.Hub.UnitWork;
using Cats.Models.Hubs;


namespace Cats.Services.Hub
{

    public class MasterService : IMasterService
    {
        private readonly IUnitOfWork _unitOfWork;


        public MasterService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddMaster(Master master)
        {
            _unitOfWork.MasterRepository.Add(master);
            _unitOfWork.Save();
            return true;

        }
        public bool EditMaster(Master master)
        {
            _unitOfWork.MasterRepository.Edit(master);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteMaster(Master master)
        {
            if (master == null) return false;
            _unitOfWork.MasterRepository.Delete(master);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.MasterRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.MasterRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Master> GetAllMaster()
        {
            return _unitOfWork.MasterRepository.GetAll();
        }
        public Master FindById(int id)
        {
            return _unitOfWork.MasterRepository.FindById(id);
        }
        public List<Master> FindBy(Expression<Func<Master, bool>> predicate)
        {
            return _unitOfWork.MasterRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


