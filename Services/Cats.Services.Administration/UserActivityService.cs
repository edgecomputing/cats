using System;
using System.Collections.Generic;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Administration
{
   public class UserActivityService : IUserActivityService
   {

       #region Field

       private readonly IUnitOfWork _unitOfWork;

       #endregion

       #region Ctor

       public UserActivityService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }

       #endregion



       #region Default Service Implementation

       public Audit FindById(int id)
       {
           return _unitOfWork.AuditRepository.FindById(id);
       }

     

       public List<Audit> GetAllUserActivity()
       {
           return _unitOfWork.AuditRepository.GetAll();
       }

       public List<Audit> FindBy(System.Linq.Expressions.Expression<Func<Audit, bool>> predicate)
       {
           return _unitOfWork.AuditRepository.FindBy(predicate);
       }

       public IEnumerable<Audit> Get(System.Linq.Expressions.Expression<Func<Audit, bool>> filter = null, Func<IQueryable<Audit>, IOrderedQueryable<Audit>> orderBy = null, string includeProperties = "")
       {
           return _unitOfWork.AuditRepository.Get(filter, orderBy, includeProperties);
       }


       public void Dispose()
       {
           _unitOfWork.Dispose();

       }
       #endregion 
       
      
    }
}
