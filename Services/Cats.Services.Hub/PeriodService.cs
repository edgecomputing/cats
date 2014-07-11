

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.Hub;
using Cats.Data.Hub.UnitWork;
using Cats.Models.Hubs;


namespace Cats.Services.Hub
{

    public class PeriodService : IPeriodService
    {
        private readonly IUnitOfWork _unitOfWork;


        public PeriodService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddPeriod(Period entity)
        {
            _unitOfWork.PeriodRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditPeriod(Period entity)
        {
            _unitOfWork.PeriodRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeletePeriod(Period entity)
        {
            if (entity == null) return false;
            _unitOfWork.PeriodRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.PeriodRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.PeriodRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Period> GetAllPeriod()
        {
            return _unitOfWork.PeriodRepository.GetAll();
        }
        public Period FindById(int id)
        {
            return _unitOfWork.PeriodRepository.FindById(id);
        }
        public List<Period> FindBy(Expression<Func<Period, bool>> predicate)
        {
            return _unitOfWork.PeriodRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }



        public List<int?> GetYears()
        {
            return _unitOfWork.PeriodRepository.FindBy(y => y.Year.HasValue).Select(p => p.Year).Distinct().ToList();
           
        }

        public List<int?> GetMonths(int year)
        {
            
            return _unitOfWork.PeriodRepository.FindBy(y => y.Year == year && y.Month.HasValue).Select(p => p.Month).Distinct().ToList();
        }

        public Period GetPeriod(int year, int month)
        {
            return _unitOfWork.PeriodRepository.FindBy(p => p.Year == year && p.Month == month).SingleOrDefault();
        }
    }
}


