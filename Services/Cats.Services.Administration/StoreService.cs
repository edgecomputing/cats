using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Administration
{
   public class StoreService:IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #region Default Service Implementation
        public bool AddStore(Store store)
        {
            _unitOfWork.StoreRepository.Add(store);
            _unitOfWork.Save();
            return true;

        }
        public bool EditStore(Store store)
        {
            _unitOfWork.StoreRepository.Edit(store);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteStore(Store store)
        {
            if (store == null) return false;
            _unitOfWork.StoreRepository.Delete(store);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.StoreRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.StoreRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Store> GetAllStore()
        {
            return _unitOfWork.StoreRepository.GetAll();
        }
        public Store FindById(int id)
        {
            return _unitOfWork.StoreRepository.FindById(id);
        }
        public List<Store> FindBy(Expression<Func<Store, bool>> predicate)
        {
            return _unitOfWork.StoreRepository.FindBy(predicate);
        }
        #endregion


        public List<Store> GetStoreByHub(int hubId)
        {
            return _unitOfWork.StoreRepository.FindBy(s => s.HubID == hubId).ToList();
        }

        public bool DeleteByID(int id)
        {
            var store = _unitOfWork.StoreRepository.FindBy(s => s.StoreID == id).SingleOrDefault();
            if (store == null) return false;
            _unitOfWork.StoreRepository.Delete(store);
            _unitOfWork.Save();
            return true;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
    }
}
