using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Data.Hub;
using Cats.Models.Hub;

namespace Cats.Services.Hub
{
    public class TransactionGroupService:ITransactionGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionGroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
       public bool AddTransactionGroup(TransactionGroup entity)
       {
           _unitOfWork.TransactionGroupRepository.Add(entity);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditTransactionGroup(TransactionGroup entity)
       {
           _unitOfWork.TransactionGroupRepository.Edit(entity);
           _unitOfWork.Save();
           return true;

       }
         public bool DeleteTransactionGroup(TransactionGroup entity)
        {
             if(entity==null) return false;
           _unitOfWork.TransactionGroupRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.TransactionGroupRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.TransactionGroupRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<TransactionGroup> GetAllTransactionGroup()
       {
           return _unitOfWork.TransactionGroupRepository.GetAll();
       } 
       public TransactionGroup FindById(int id)
       {
           return _unitOfWork.TransactionGroupRepository.FindById(id);
       }
       public List<TransactionGroup> FindBy(Expression<Func<TransactionGroup, bool>> predicate)
       {
           return _unitOfWork.TransactionGroupRepository.FindBy(predicate);
       }
       #endregion
       
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }
        public Guid GetLastTrasactionGroupId()
        {
            Guid trasactionGroupId = _unitOfWork.TransactionGroupRepository.GetAll().OrderByDescending(t => t.TransactionGroupID).Select(g => g.TransactionGroupID).First();
            return trasactionGroupId;
        }

       
    }
}



