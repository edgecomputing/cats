using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;

namespace Cats.Services.EarlyWarning
{
   public class CurrencyService:ICurrencyService
    {   
        private readonly IUnitOfWork _unitOfWork;

       public CurrencyService(IUnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork;
       }

        public bool AddCurrency(Models.Currency currency)
        {
            _unitOfWork.CurrencyRepository.Add(currency);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteCurrency(Models.Currency currency)
        {
            if (currency == null) return false;
            _unitOfWork.CurrencyRepository.Delete(currency);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.CurrencyRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.CurrencyRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditCurrency(Models.Currency currency)
        {
            _unitOfWork.CurrencyRepository.Edit(currency);
            _unitOfWork.Save();
            return true;
        }

        public Models.Currency FindById(int id)
        {
            return _unitOfWork.CurrencyRepository.FindById(id);
        }

        public List<Models.Currency> GetAllCurrency()
        {
            return _unitOfWork.CurrencyRepository.GetAll();
        }

        public List<Models.Currency> FindBy(System.Linq.Expressions.Expression<Func<Models.Currency, bool>> predicate)
        {
            return _unitOfWork.CurrencyRepository.FindBy(predicate);
        }

        public IEnumerable<Models.Currency> Get(System.Linq.Expressions.Expression<Func<Models.Currency, bool>> filter = null, Func<IQueryable<Models.Currency>, IOrderedQueryable<Models.Currency>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.CurrencyRepository.Get(filter, orderBy, includeProperties);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
