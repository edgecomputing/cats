using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.PSNP
{
    public class RegionalPSNPPlanDetailService : IRegionalPSNPPlanDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegionalPSNPPlanDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool AddRegionalPSNPPlanDetail(RegionalPSNPPlanDetail item)
        {
            _unitOfWork.RegionalPSNPPlanDetailRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateRegionalPSNPPlanDetail(RegionalPSNPPlanDetail item)
        {
            if (item == null) return false;
            _unitOfWork.RegionalPSNPPlanDetailRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteRegionalPSNPPlanDetail(RegionalPSNPPlanDetail item)
        {
            if (item == null) return false;
            _unitOfWork.RegionalPSNPPlanDetailRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var item = _unitOfWork.RegionalPSNPPlanDetailRepository.FindById(id);
            return DeleteRegionalPSNPPlanDetail(item);
        }
        public RegionalPSNPPlanDetail FindById(int id)
        {
            return _unitOfWork.RegionalPSNPPlanDetailRepository.FindById(id);
        }
        public List<RegionalPSNPPlanDetail> GetAllRegionalPSNPPlanDetail()
        {
            return _unitOfWork.RegionalPSNPPlanDetailRepository.GetAll();

        }
        public List<RegionalPSNPPlanDetail> FindBy(Expression<Func<RegionalPSNPPlanDetail, bool>> predicate)
        {
            return _unitOfWork.RegionalPSNPPlanDetailRepository.FindBy(predicate);

        }
    }
}