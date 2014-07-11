

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.EarlyWarning
{

    public class FDPService : IFDPService
    {
        private readonly IUnitOfWork _unitOfWork;


        public FDPService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddFDP(FDP fdp)
        {
            _unitOfWork.FDPRepository.Add(fdp);
            _unitOfWork.Save();
            return true;

        }
        public bool EditFDP(FDP fdp)
        {
            _unitOfWork.FDPRepository.Edit(fdp);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteFDP(FDP fdp)
        {
            if (fdp == null) return false;
            _unitOfWork.FDPRepository.Delete(fdp);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.FDPRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.FDPRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<FDP> GetAllFDP()
        {
            return _unitOfWork.FDPRepository.GetAll();
        }
        public FDP FindById(int id)
        {
            return _unitOfWork.FDPRepository.FindById(id);
        }
        public List<FDP> FindBy(Expression<Func<FDP, bool>> predicate)
        {
            return _unitOfWork.FDPRepository.FindBy(predicate);
        }
        public IEnumerable<FDP> Get(System.Linq.Expressions.Expression<Func<FDP, bool>> filter = null,
                                    Func<IQueryable<FDP>, IOrderedQueryable<FDP>> orderBy = null,
                                    string includeProperties = "")
        {
            return _unitOfWork.FDPRepository.Get(filter, orderBy, includeProperties);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


