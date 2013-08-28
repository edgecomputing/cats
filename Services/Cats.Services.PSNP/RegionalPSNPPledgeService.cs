using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.PSNP
{
    public class RegionalPSNPPledgeService : IRegionalPSNPPledgeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegionalPSNPPledgeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Implementation of IRegionalPSNPPledgeService

        public bool AddRegionalPSNPPledge(RegionalPSNPPledge item)
        {
            _unitOfWork.RegionalPSNPPledgeRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }

        public bool UpdateRegionalPSNPPledge(RegionalPSNPPledge item)
        {
            if (item == null) return false;
            _unitOfWork.RegionalPSNPPledgeRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteRegionalPSNPPledge(RegionalPSNPPledge item)
        {
            if (item == null) return false;
            _unitOfWork.RegionalPSNPPledgeRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var item = _unitOfWork.RegionalPSNPPledgeRepository.FindById(id);
            return DeleteRegionalPSNPPledge(item);
        }

        public RegionalPSNPPledge FindById(int id)
        {
            return _unitOfWork.RegionalPSNPPledgeRepository.FindById(id);
        }

        public List<RegionalPSNPPledge> GetAllRegionalPSNPPledge()
        {
            return _unitOfWork.RegionalPSNPPledgeRepository.GetAll();
        }

        public List<RegionalPSNPPledge> FindBy(Expression<Func<RegionalPSNPPledge, bool>> predicate)
        {
            return _unitOfWork.RegionalPSNPPledgeRepository.FindBy(predicate);
        }

        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
    }
}
