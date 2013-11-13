

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hubs;


namespace Cats.Services.Hub
{

    public class HubOwnerService : IHubOwnerService
    {
        private readonly IUnitOfWork _unitOfWork;


        public HubOwnerService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddHubOwner(HubOwner hubOwner)
        {
            _unitOfWork.HubOwnerRepository.Add(hubOwner);
            _unitOfWork.Save();
            return true;

        }
        public bool EditHubOwner(HubOwner hubOwner)
        {
            _unitOfWork.HubOwnerRepository.Edit(hubOwner);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteHubOwner(HubOwner hubOwner)
        {
            if (hubOwner == null) return false;
            _unitOfWork.HubOwnerRepository.Delete(hubOwner);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.HubOwnerRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.HubOwnerRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<HubOwner> GetAllHubOwner()
        {
            return _unitOfWork.HubOwnerRepository.GetAll();
        }
        public HubOwner FindById(int id)
        {
            return _unitOfWork.HubOwnerRepository.FindById(id);
        }
        public List<HubOwner> FindBy(Expression<Func<HubOwner, bool>> predicate)
        {
            return _unitOfWork.HubOwnerRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


