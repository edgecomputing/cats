﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Administration
{
    public class HubOwnerService : IHubOwnerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HubOwnerService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        #endregion

        #region Implementation of IHubOwnerService

        public bool AddHubOwner(HubOwner hubOwner)
        {
            _unitOfWork.HubOwnerRepository.Add(hubOwner);
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

        public bool EditHubOwner(HubOwner hubOwner)
        {
            _unitOfWork.HubOwnerRepository.Edit(hubOwner);
            _unitOfWork.Save();
            return true;
        }

        public HubOwner FindById(int id)
        {
            return _unitOfWork.HubOwnerRepository.FindById(id);
        }

        public List<HubOwner> GetAllHubOwner()
        {
            return _unitOfWork.HubOwnerRepository.GetAll();
        }

        public List<HubOwner> FindBy(Expression<Func<HubOwner, bool>> predicate)
        {
            return _unitOfWork.HubOwnerRepository.FindBy(predicate);
        }

        #endregion
    }
}
