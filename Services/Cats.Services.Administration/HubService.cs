using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;

namespace Cats.Services.Administration
{
    public class HubService :IHubService
    {
        private readonly IUnitOfWork _unitOfWork;


        public HubService(IUnitOfWork unitOfWork)
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

        #region Implementation of IHubService

        public bool AddHub(Models.Hub hub)
        {
            _unitOfWork.HubRepository.Add(hub);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteHub(Models.Hub hub)
        {
            if (hub == null) return false;
            _unitOfWork.HubRepository.Delete(hub);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.HubRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.HubRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditHub(Models.Hub hub)
        {
            _unitOfWork.HubRepository.Edit(hub);
            _unitOfWork.Save();
            return true;
        }

        public Models.Hub FindById(int id)
        {
            return _unitOfWork.HubRepository.FindById(id);
        }

        public List<Models.Hub> GetAllHub()
        {
            return _unitOfWork.HubRepository.GetAll();
        }

        public List<Models.Hub> FindBy(Expression<Func<Models.Hub, bool>> predicate)
        {
            return _unitOfWork.HubRepository.FindBy(predicate);
        }

        public List<Models.Hub> GetAllWithoutId(int hubId)
        {
            return _unitOfWork.HubRepository.Get(p => p.HubID != hubId).ToList();
        }

        public List<Models.Hub> GetOthersHavingSameOwner(Models.Hub hub)
        {
            return (from v in _unitOfWork.HubRepository.GetAll()
                    where v.HubID != hub.HubID && v.HubOwnerID == hub.HubOwnerID
                    select v).ToList();
        }

        public List<Models.Hub> GetOthersWithDifferentOwner(Models.Hub hub)
        {
            return (from v in _unitOfWork.HubRepository.GetAll()
                    where v.HubOwnerID != hub.HubOwnerID
                    select v).ToList();
        }

        #endregion
    }
}
