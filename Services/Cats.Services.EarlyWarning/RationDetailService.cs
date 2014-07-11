using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Services.EarlyWarning;

namespace Cats.Services.EarlyWarning
{

    public class RationDetailService:IRationDetailService
   {
       private readonly  IUnitOfWork _unitOfWork;

       public RationDetailService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       #region Default Service Implementation
       
       public bool AddRationDetail(RationDetail rationDetail)
       {
           _unitOfWork.RationDetailRepository.Add(rationDetail);
           _unitOfWork.Save();
           return true;
       }

       public bool EditRationDetail(RationDetail rationDetail)
       {
           _unitOfWork.RationDetailRepository.Edit(rationDetail);
           _unitOfWork.Save();
           return true;
       }
       public bool DeleteRationDetail(RationDetail rationDetail)
       {
             if(rationDetail==null) return false;
           _unitOfWork.RationDetailRepository.Delete(rationDetail);
           _unitOfWork.Save();
           return true;
       }

       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.RationDetailRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.RationDetailRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }

       public List<RationDetail> GetAllRationDetail()
       {
           return _unitOfWork.RationDetailRepository.GetAll();
       }
 
       public RationDetail FindById(int id)
       {
           return _unitOfWork.RationDetailRepository.FindById(id);
       }

       public List<RationDetail> FindBy(Expression<Func<RationDetail, bool>> predicate)
       {
           return _unitOfWork.RationDetailRepository.FindBy(predicate);
       }
       
       public IEnumerable<RationDetail> Get(
           Expression<Func<RationDetail, bool>> filter = null,
           Func<IQueryable<RationDetail>, IOrderedQueryable<RationDetail>> orderBy = null,
           string includeProperties = "")
        {
           return _unitOfWork.RationDetailRepository.Get(filter, orderBy, includeProperties);
        }

       #endregion
       
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }
       
   }
   }
   
 
      