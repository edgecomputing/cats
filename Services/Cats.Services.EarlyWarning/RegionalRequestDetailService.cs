

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;



namespace Cats.Services.EarlyWarning
{

    public class RegionalRequestDetailService : IRegionalRequestDetailService
    {
        private readonly IUnitOfWork _unitOfWork;


        public RegionalRequestDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddRegionalRequestDetail(RegionalRequestDetail regionalRequestDetail)
        {
            _unitOfWork.RegionalRequestDetailRepository.Add(regionalRequestDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool EditRegionalRequestDetail(RegionalRequestDetail regionalRequestDetail)
        {
            _unitOfWork.RegionalRequestDetailRepository.Edit(regionalRequestDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteRegionalRequestDetail(RegionalRequestDetail regionalRequestDetail)
        {
            if (regionalRequestDetail == null) return false;
            _unitOfWork.RegionalRequestDetailRepository.Delete(regionalRequestDetail);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.RegionalRequestDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.RegionalRequestDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<RegionalRequestDetail> GetAllRegionalRequestDetail()
        {
            return _unitOfWork.RegionalRequestDetailRepository.GetAll();
        }
        public RegionalRequestDetail FindById(int id)
        {
            return _unitOfWork.RegionalRequestDetailRepository.FindById(id);
        }
        public List<RegionalRequestDetail> FindBy(Expression<Func<RegionalRequestDetail, bool>> predicate)
        {
            return _unitOfWork.RegionalRequestDetailRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }



        public bool Save()
        {
           _unitOfWork.Save();
            return true;
        }
    }
}


