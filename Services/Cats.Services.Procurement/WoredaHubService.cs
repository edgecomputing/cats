using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public class WoredaHubService :IWoredaHubService
    {

        private readonly IUnitOfWork _unitOfWork;
        public WoredaHubService(IUnitOfWork unitOfWork)
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

        #region Implementation of IWoredaHubService

        public bool AddWoredaHub(WoredaHub woredaHub)
        {
            _unitOfWork.WoredaHubRepository.Add(woredaHub);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteWoredaHub(WoredaHub woredaHub)
        {
            if (woredaHub == null) return false;
            _unitOfWork.WoredaHubRepository.Delete(woredaHub);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.WoredaHubRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.WoredaHubRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditWoredaHub(WoredaHub woredaHub)
        {
            _unitOfWork.WoredaHubRepository.Edit(woredaHub);
            _unitOfWork.Save();
            return true;
        }

        public WoredaHub FindById(int id)
        {
            return _unitOfWork.WoredaHubRepository.FindById(id);
        }

        public List<WoredaHub> GetAllWoredaHub()
        {
            return _unitOfWork.WoredaHubRepository.GetAll();
        }

        public List<WoredaHub> FindBy(Expression<Func<WoredaHub, bool>> predicate)
        {
            return _unitOfWork.WoredaHubRepository.FindBy(predicate);
        }

        public IEnumerable<WoredaHub> Get(Expression<Func<WoredaHub, bool>> filter = null, Func<IQueryable<WoredaHub>, IOrderedQueryable<WoredaHub>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.WoredaHubRepository.Get(filter, orderBy, includeProperties);
        }

        #endregion
    }
}
