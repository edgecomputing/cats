using System;
using System.Collections.Generic;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public class StatusService:IStatusService
    {
         private readonly  IUnitOfWork _unitOfWork;
      

       public StatusService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       public bool AddStatus(Status status)
       {
           _unitOfWork.StatusRepository.Add(status);
           _unitOfWork.Save();
           return true;
       }

        public bool DeleteStatus(Status status)
       {
           if (status == null) return false;
           _unitOfWork.StatusRepository.Delete(status);
           _unitOfWork.Save();
           return true;
       }
       public bool DeleteById(int id)
       {
           var entity = _unitOfWork.StatusRepository.FindById(id);
           if (entity == null) return false;
           _unitOfWork.StatusRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }

       public bool EditStatus(Status status)
       {
           _unitOfWork.StatusRepository.Edit(status);
           _unitOfWork.Save();
           return true;
       }

       public Status FindById(int id)
       {
           return _unitOfWork.StatusRepository.FindById(id);
       }

       public List<Status> GetAllStatus()
       {
           return _unitOfWork.StatusRepository.GetAll();
       }

       public List<Status> FindBy(System.Linq.Expressions.Expression<Func<Status, bool>> predicate)
       {
           return _unitOfWork.StatusRepository.FindBy(predicate);
       }

       public IEnumerable<Status> Get(System.Linq.Expressions.
           Expression<Func<Status, bool>> filter = null,
           Func<IQueryable<Status>, IOrderedQueryable<Status>> orderBy = null, string includeProperties = "")
       {
           return _unitOfWork.StatusRepository.Get(filter, orderBy, includeProperties);
       }

    }
}
