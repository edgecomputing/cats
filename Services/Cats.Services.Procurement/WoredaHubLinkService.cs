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
    public class WoredaHubLinkService:IWoredaHubLinkService
    {

        private readonly IUnitOfWork _unitOfWork;
        public WoredaHubLinkService(IUnitOfWork unitOfWork)
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

        #region Implementation of IWoredaHubLinkService

        public bool AddWoredaHubLink(WoredaHubLink woredaHubLink)
        {
            _unitOfWork.WoredaHubLinkRepository.Add(woredaHubLink);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteWoredaHubLink(WoredaHubLink woredaHubLink)
        {
            if (woredaHubLink == null) return false;
            _unitOfWork.WoredaHubLinkRepository.Delete(woredaHubLink);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.WoredaHubLinkRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.WoredaHubLinkRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditWoredaHubLink(WoredaHubLink woredaHubLink)
        {
            _unitOfWork.WoredaHubLinkRepository.Edit(woredaHubLink);
            _unitOfWork.Save();
            return true;
        }

        public WoredaHubLink FindById(int id)
        {
            return _unitOfWork.WoredaHubLinkRepository.FindById(id);
        }

        public List<WoredaHubLink> GetAllWoredaHubLink()
        {
            return _unitOfWork.WoredaHubLinkRepository.GetAll();
        }

        public List<WoredaHubLink> FindBy(Expression<Func<WoredaHubLink, bool>> predicate)
        {
            return _unitOfWork.WoredaHubLinkRepository.FindBy(predicate);
        }

        public IEnumerable<WoredaHubLink> Get(Expression<Func<WoredaHubLink, bool>> filter = null, Func<IQueryable<WoredaHubLink>, IOrderedQueryable<WoredaHubLink>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.WoredaHubLinkRepository.Get(filter, orderBy, includeProperties);
        }

        #endregion
    }
}
