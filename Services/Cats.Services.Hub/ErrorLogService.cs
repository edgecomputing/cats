

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hub;
using DRMFSS.BLL;


namespace Cats.Services.Hub
{

    public class ErrorLogService:IErrorLogService
   {
       private readonly  IUnitOfWork _unitOfWork;
      

       public ErrorLogService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       #region Default Service Implementation
       public bool AddErrorLog(ErrorLog errorLog)
       {
           _unitOfWork.ErrorLogRepository.Add(errorLog);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditErrorLog(ErrorLog errorLog)
       {
           _unitOfWork.ErrorLogRepository.Edit(errorLog);
           _unitOfWork.Save();
           return true;

       }
         public bool DeleteErrorLog(ErrorLog errorLog)
        {
             if(errorLog==null) return false;
           _unitOfWork.ErrorLogRepository.Delete(errorLog);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.ErrorLogRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.ErrorLogRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<ErrorLog> GetAllErrorLog()
       {
           return _unitOfWork.ErrorLogRepository.GetAll();
       } 
       public ErrorLog FindById(int id)
       {
           return _unitOfWork.ErrorLogRepository.FindById(id);
       }
       public List<ErrorLog> FindBy(Expression<Func<ErrorLog, bool>> predicate)
       {
           return _unitOfWork.ErrorLogRepository.FindBy(predicate);
       }
       
       public IEnumerable<ErrorLog> Get(
           Expression<Func<ErrorLog, bool>> filter = null,
           Func<IQueryable<ErrorLog>, IOrderedQueryable<ErrorLog>> orderBy = null,
           string includeProperties = "")
        {
           return _unitOfWork.ErrorLogRepository.Get(filter, orderBy, includeProperties);
        }
       #endregion
       
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }
       
   }
   }
   
 
      