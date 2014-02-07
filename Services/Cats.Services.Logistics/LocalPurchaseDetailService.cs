using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Logistics
{

    public class LocalPurchaseDetailService:ILocalPurchaseDetailService
   {
       private readonly  IUnitOfWork _unitOfWork;


       public LocalPurchaseDetailService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       #region Default Service Implementation
       public bool AddLocalPurchaseDetail(LocalPurchaseDetail entity)
       {
           _unitOfWork.LocalPurchaseDetailRepository.Add(entity);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditLocalPurchaseDetail(LocalPurchaseDetail entity)
       {
           _unitOfWork.LocalPurchaseDetailRepository.Edit(entity);
           _unitOfWork.Save();
           return true;

       }
       public bool DeleteLocalPurchaseDetail(LocalPurchaseDetail entity)
        {
             if(entity==null) return false;
             _unitOfWork.LocalPurchaseDetailRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.LocalPurchaseDetailRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.LocalPurchaseDetailRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<LocalPurchaseDetail> GetAllLocalPurchaseDetail()
       {
           return _unitOfWork.LocalPurchaseDetailRepository.GetAll();
       }
       public LocalPurchaseDetail FindById(int id)
       {
           return _unitOfWork.LocalPurchaseDetailRepository.FindById(id);
       }
       public List<LocalPurchaseDetail> FindBy(Expression<Func<LocalPurchaseDetail, bool>> predicate)
       {
           return _unitOfWork.LocalPurchaseDetailRepository.FindBy(predicate);
       }
       #endregion
       
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }
       
   }
   }
   
         
      