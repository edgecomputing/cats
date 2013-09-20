using System;
using System.Collections.Generic;
using Cats.Models;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;

namespace Cats.Services.Administration
{
   public class HubService:IHubService
    {
        private readonly IUnitOfWork _unitOfWork;


        public HubService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool AddHub(Hub hub)
        {
            _unitOfWork.HubRepository.Add(hub);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteHub(Hub hub)
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

        public bool EditHub(Hub hub)
        {
            _unitOfWork.HubRepository.Edit(hub);
            _unitOfWork.Save();
            return true;
        }

        public Hub FindById(int id)
        {
            return _unitOfWork.HubRepository.FindById(id);
        }

        public List<Hub> GetAllHub()
        {
            return _unitOfWork.HubRepository.GetAll();
        }

        public List<Hub> FindBy(System.Linq.Expressions.Expression<Func<Hub, bool>> predicate)
        {
            return _unitOfWork.HubRepository.FindBy(predicate);
        }
        public void Dispose()
       {
           _unitOfWork.Dispose();
       }
    }
}
