using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Transaction
{
    public class AccountTransactionService : IAccountTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountTransactionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool AddAccountTransaction(AccountTransaction item)
        {
            _unitOfWork.AccountTransactionRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateAccountTransaction(AccountTransaction item)
        {
            if (item == null) return false;
            _unitOfWork.AccountTransactionRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteAccountTransaction(AccountTransaction item)
        {
            if (item == null) return false;
            _unitOfWork.AccountTransactionRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(Guid id)
        {
            var item = _unitOfWork.AccountTransactionRepository.FindById(id);
            return DeleteAccountTransaction(item);
        }
        public AccountTransaction FindById(Guid id)
        {
            return _unitOfWork.AccountTransactionRepository.FindById(id);
        }
        public List<AccountTransaction> GetAllAccountTransaction()
        {
            return _unitOfWork.AccountTransactionRepository.GetAll();

        }
        public List<AccountTransaction> FindBy(Expression<Func<AccountTransaction, bool>> predicate)
        {
            return _unitOfWork.AccountTransactionRepository.FindBy(predicate);

        }
    }
}