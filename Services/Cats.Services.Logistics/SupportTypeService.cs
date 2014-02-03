using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;

namespace  Cats.Services.Logistics
{

    public class SupportTypeService:ISupportTypeService
   {
       private readonly  IUnitOfWork _unitOfWork;
      

       public SupportTypeService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       #region Default Service Implementation
       public bool AddSupportType(SupportType entity)
       {
           _unitOfWork.SupportTypeRepository.Add(entity);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditSupportType(SupportType entity)
       {
           _unitOfWork.SupportTypeRepository.Edit(entity);
           _unitOfWork.Save();
           return true;

       }
         public bool DeleteSupportType(SupportType entity)
        {
             if(entity==null) return false;
           _unitOfWork.SupportTypeRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.SupportTypeRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.SupportTypeRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<SupportType> GetAllSupportType()
       {
           return _unitOfWork.SupportTypeRepository.GetAll();
       } 
       public SupportType FindById(int id)
       {
           return _unitOfWork.SupportTypeRepository.FindById(id);
       }
       public List<SupportType> FindBy(Expression<Func<SupportType, bool>> predicate)
       {
           return _unitOfWork.SupportTypeRepository.FindBy(predicate);
       }
       #endregion
       
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }
       
   }
   }
   
         
      