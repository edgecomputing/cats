using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public class PromisedContributionService:IPromisedContributionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PromisedContributionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region Default Service Implementation
        
        public bool Create(PromisedContribution promisedContribution)
        {
            _unitOfWork.PromisedContributionRepository.Add(promisedContribution);
            _unitOfWork.Save();
            try
            {
               // _unitOfWork.PromisedContributionRepository.Add(promisedContribution);
               // _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {

            }
            return false;
        }

        public PromisedContribution FindById(int id)
        {
            return _unitOfWork.PromisedContributionRepository.FindById(id);
        }
        public List<PromisedContribution> GetAll()
        {
            return _unitOfWork.PromisedContributionRepository.GetAll();
        }

        public List<PromisedContribution> FindBy(Expression<Func<PromisedContribution, bool>> predicate)
        {
            return _unitOfWork.PromisedContributionRepository.FindBy(predicate);
        }

        public bool Update(PromisedContribution promisedContribution)
        {
            try
            {
                _unitOfWork.PromisedContributionRepository.Edit(promisedContribution);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {

            }
            return false;
            
        }

        public bool Delete(PromisedContribution promisedContribution)
        {
            try
            {
                _unitOfWork.PromisedContributionRepository.Delete(promisedContribution);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {

            }
            return false;
        }
        public bool DeleteById(int id)
        {
            PromisedContribution promisedContribution = _unitOfWork.PromisedContributionRepository.FindById(id);
            return Delete(promisedContribution);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}
