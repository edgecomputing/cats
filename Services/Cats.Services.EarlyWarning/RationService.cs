

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Services.EarlyWarning;


namespace Cats.Services.EarlyWarning
{

    public class RationService:IRationService
   {
       private readonly  IUnitOfWork _unitOfWork;
      

       public RationService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       #region Default Service Implementation
       public bool AddRation(Ration ration)
       {
           _unitOfWork.RationRepository.Add(ration);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditRation(Ration ration)
       {
           _unitOfWork.RationRepository.Edit(ration);
           _unitOfWork.Save();
           return true;

       }
         public bool DeleteRation(Ration ration)
        {
             if(ration==null) return false;
           _unitOfWork.RationRepository.Delete(ration);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.RationRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.RationRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<Ration> GetAllRation()
       {
           return _unitOfWork.RationRepository.GetAll();
       } 
       public Ration FindById(int id)
       {
           return _unitOfWork.RationRepository.FindById(id);
       }
       public List<Ration> FindBy(Expression<Func<Ration, bool>> predicate)
       {
           return _unitOfWork.RationRepository.FindBy(predicate);
       }
       
       public IEnumerable<Ration> Get(
           Expression<Func<Ration, bool>> filter = null,
           Func<IQueryable<Ration>, IOrderedQueryable<Ration>> orderBy = null,
           string includeProperties = "")
        {
           return _unitOfWork.RationRepository.Get(filter, orderBy, includeProperties);
        }
       #endregion
       
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }
       
   }
   }
   
 
      