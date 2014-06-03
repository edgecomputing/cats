using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Administration
{
   public class LossReasonService:ILossReasonService
    {
       private readonly IUnitOfWork _unitOfWork;


       public LossReasonService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
       public bool AddLossReason(LossReason lossReason)
        {
            _unitOfWork.LossReasonRepository.Add(lossReason);
            _unitOfWork.Save();
            return true;

        }
       public bool EditLossReason(LossReason lossReason)
        {
            _unitOfWork.LossReasonRepository.Edit(lossReason);
            _unitOfWork.Save();
            return true;

        }
       public bool DeleteLossReason(LossReason lossReason)
        {
            if (lossReason == null) return false;
            _unitOfWork.LossReasonRepository.Delete(lossReason);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.LossReasonRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.LossReasonRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<LossReason> GetAllLossReason()
        {
            return _unitOfWork.LossReasonRepository.GetAll();
        }
        public FDP FindById(int id)
        {
            return _unitOfWork.FDPRepository.FindById(id);
        }
        public List<LossReason> FindBy(Expression<Func<LossReason, bool>> predicate)
        {
            return _unitOfWork.LossReasonRepository.FindBy(predicate);
        }

        public IEnumerable<LossReason> Get(System.Linq.Expressions.Expression<Func<LossReason, bool>> filter = null,
                                    Func<IQueryable<LossReason>, IOrderedQueryable<LossReason>> orderBy = null,
                                    string includeProperties = "")
        {
            return _unitOfWork.LossReasonRepository.Get(filter, orderBy, includeProperties);
        }

        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
    }
}
