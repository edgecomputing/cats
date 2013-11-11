

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hubs;


namespace Cats.Services.Hub
{

    public class ForgetPasswordRequestService : IForgetPasswordRequestService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ForgetPasswordRequestService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddForgetPasswordRequest(ForgetPasswordRequest forgetPasswordRequest)
        {
            _unitOfWork.ForgetPasswordRequestRepository.Add(forgetPasswordRequest);
            _unitOfWork.Save();
            return true;

        }
        public bool EditForgetPasswordRequest(ForgetPasswordRequest forgetPasswordRequest)
        {
            _unitOfWork.ForgetPasswordRequestRepository.Edit(forgetPasswordRequest);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteForgetPasswordRequest(ForgetPasswordRequest forgetPasswordRequest)
        {
            if (forgetPasswordRequest == null) return false;
            _unitOfWork.ForgetPasswordRequestRepository.Delete(forgetPasswordRequest);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ForgetPasswordRequestRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ForgetPasswordRequestRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<ForgetPasswordRequest> GetAllForgetPasswordRequest()
        {
            return _unitOfWork.ForgetPasswordRequestRepository.GetAll();
        }
        public ForgetPasswordRequest FindById(int id)
        {
            return _unitOfWork.ForgetPasswordRequestRepository.FindById(id);
        }
        public List<ForgetPasswordRequest> FindBy(Expression<Func<ForgetPasswordRequest, bool>> predicate)
        {
            return _unitOfWork.ForgetPasswordRequestRepository.FindBy(predicate);
        }
        #endregion
        /// <summary>
        /// Gets the valid password reset request.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public ForgetPasswordRequest GetValidRequest(string key)
        {
            return _unitOfWork.ForgetPasswordRequestRepository.Get(t=>t
                    .RequestKey == key && t.ExpieryDate > DateTime.Now && !t.Completed).SingleOrDefault();
        }

        /// <summary>
        /// Invalidates the request.
        /// </summary>
        /// <param name="userId">The user id.</param>
        public void InvalidateRequest(int userId)
        {
            var reqs = _unitOfWork.ForgetPasswordRequestRepository.FindBy(p => p.UserProfileID == userId).ToList();
            if (reqs.Count > 0)
            {
                foreach (ForgetPasswordRequest req in reqs)
                {
                    req.Completed = true;
                }
              _unitOfWork.Save();
            }
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


