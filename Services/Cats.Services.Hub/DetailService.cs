

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hub;
using Cats.Models.Hub.ViewModels.Common;


namespace Cats.Services.Hub
{

    public class DetailService : IDetailService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DetailService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddDetail(Detail detail)
        {
            _unitOfWork.DetailRepository.Add(detail);
            _unitOfWork.Save();
            return true;

        }
        public bool EditDetail(Detail detail)
        {
            _unitOfWork.DetailRepository.Edit(detail);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteDetail(Detail detail)
        {
            if (detail == null) return false;
            _unitOfWork.DetailRepository.Delete(detail);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Detail> GetAllDetail()
        {
            return _unitOfWork.DetailRepository.GetAll();
        }
        public Detail FindById(int id)
        {
            return _unitOfWork.DetailRepository.FindById(id);
        }
        public List<Detail> FindBy(Expression<Func<Detail, bool>> predicate)
        {
            return _unitOfWork.DetailRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }


        /// <summary>
        /// Gets the queriable list of details by master ID.
        /// </summary>
        /// <param name="masterId">The master id.</param>
        /// <returns></returns>
        public IQueryable<Detail> GetByMasterID(int masterId)
        {
            return _unitOfWork.DetailRepository.Get(t => t.MasterID == masterId) as IQueryable<Detail>;
         
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public List<ReasonViewModel> GetReasonByMaster(int masterId)
        {
            var tempDetails = _unitOfWork.DetailRepository.Get(t => t.MasterID == masterId);
            var reasons = (from c in tempDetails where c.MasterID == masterId select new ReasonViewModel { ReasonId = c.DetailID, ReasonName = c.Name }).ToList();
            return reasons;
        }
    }
}


