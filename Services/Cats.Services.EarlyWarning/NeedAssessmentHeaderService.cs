using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{

    public class NeedAssessmentHeaderService : INeedAssessmentHeaderService
    {
        private readonly IUnitOfWork _unitOfWork;


        public NeedAssessmentHeaderService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddNeedAssessmentHeader(NeedAssessmentHeader needAssessmentHeader)
        {
            _unitOfWork.NeedAssessmentHeaderRepository.Add(needAssessmentHeader);
            _unitOfWork.Save();
            return true;

        }
        public bool EditNeedAssessmentHeader(NeedAssessmentHeader needAssessmentHeader)
        {
            _unitOfWork.NeedAssessmentHeaderRepository.Edit(needAssessmentHeader);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteNeedAssessmentHeader(NeedAssessmentHeader needAssessmentHeader)
        {
            if (needAssessmentHeader == null) return false;
            _unitOfWork.NeedAssessmentHeaderRepository.Delete(needAssessmentHeader);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.NeedAssessmentHeaderRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.NeedAssessmentHeaderRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<NeedAssessmentHeader> GetAllNeedAssessmentHeader()
        {
            return _unitOfWork.NeedAssessmentHeaderRepository.GetAll();
        }
        public NeedAssessmentHeader FindById(int id)
        {
            return _unitOfWork.NeedAssessmentHeaderRepository.FindById(id);
        }
        public List<NeedAssessmentHeader> FindBy(Expression<Func<NeedAssessmentHeader, bool>> predicate)
        {
            return _unitOfWork.NeedAssessmentHeaderRepository.FindBy(predicate);
        }


        public IEnumerable<NeedAssessmentHeader> Get(
          Expression<Func<NeedAssessmentHeader, bool>> filter = null,
          Func<IQueryable<NeedAssessmentHeader>, IOrderedQueryable<NeedAssessmentHeader>> orderBy = null,
          string includeProperties = "")
        {
            return _unitOfWork.NeedAssessmentHeaderRepository.Get(filter, orderBy, includeProperties);
        }


        #endregion

        public int GetUserProfileId(string userName)
        {
            var userProfile  = _unitOfWork.UserProfileRepository.FindBy(u => u.UserName == userName).SingleOrDefault();
            if (userProfile == null) return -1;
            return userProfile.UserProfileID;
        }

        public string GetUserProfileName(int userProfileId)
        {
            var userProfile = _unitOfWork.UserProfileRepository.FindBy(u => u.UserProfileID == userProfileId).SingleOrDefault();
            if (userProfile == null) return string.Empty;
            return userProfile.UserName;
        }
        public List<UserProfile> GetUsers()
        {
            var userProfile = _unitOfWork.UserProfileRepository.GetAll();
            if (userProfile == null) return null;
            return  userProfile.ToList();
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


