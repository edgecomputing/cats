

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.Administration
{

    public class CommodityGradeService : ICommodityGradeService
    {
        private readonly IUnitOfWork _unitOfWork;


        public CommodityGradeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddCommodityGrade(CommodityGrade commodityGrade)
        {
            _unitOfWork.CommodityGradeRepository.Add(commodityGrade);
            _unitOfWork.Save();
            return true;

        }
        public bool EditCommodityGrade(CommodityGrade commodityGrade)
        {
            _unitOfWork.CommodityGradeRepository.Edit(commodityGrade);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteCommodityGrade(CommodityGrade commodityGrade)
        {
            if (commodityGrade == null) return false;
            _unitOfWork.CommodityGradeRepository.Delete(commodityGrade);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.CommodityGradeRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.CommodityGradeRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<CommodityGrade> GetAllCommodityGrade()
        {
            return _unitOfWork.CommodityGradeRepository.GetAll();
        }
        public CommodityGrade FindById(int id)
        {
            return _unitOfWork.CommodityGradeRepository.FindById(id);
        }
        public List<CommodityGrade> FindBy(Expression<Func<CommodityGrade, bool>> predicate)
        {
            return _unitOfWork.CommodityGradeRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


