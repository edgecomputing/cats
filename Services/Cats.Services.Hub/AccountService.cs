

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hub;


namespace Cats.Services.Hub
{

    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;


        public AccountService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddAccount(Account entity)
        {
            _unitOfWork.AccountRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditAccount(Account entity)
        {
            _unitOfWork.AccountRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteAccount(Account account)
        {
            if (account == null) return false;
            _unitOfWork.AccountRepository.Delete(account);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.AccountRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.AccountRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Account> GetAllAccount()
        {
            return _unitOfWork.AccountRepository.GetAll();
        }
        public Account FindById(int id)
        {
            return _unitOfWork.AccountRepository.FindById(id);
        }
        public List<Account> FindBy(Expression<Func<Account, bool>> predicate)
        {
            return _unitOfWork.AccountRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

        public int GetAccountId(string entityType, int entityId)
        {
             var account = _unitOfWork.AccountRepository.FindBy(t => t.EntityType == entityType && t.EntityID == entityId).FirstOrDefault();
            if (account != null)
            {
                return account.AccountID;
            }
            // -1 represents an account was not found for this entity
            return -1;
        }

        public int GetAccountIdWithCreate(string entityType, int entityId)
        {
            var account = _unitOfWork.AccountRepository.FindBy(t => t.EntityType == entityType && t.EntityID == entityId);
               
            if (account.Count == 0)
            {
                // this means it doesn't exist, insert it here.
               
                var  newAccount = new Account();
                newAccount.EntityID = entityId;
                newAccount.EntityType = entityType;
                _unitOfWork.AccountRepository.Add(newAccount);
                _unitOfWork.Save();
                return newAccount.AccountID;
            }
          
            return  account.FirstOrDefault().AccountID;
        }
    }
}


