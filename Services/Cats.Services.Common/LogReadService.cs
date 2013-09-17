using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Data.UnitWork;

namespace Cats.Services.Common
{
    public class LogReadService : ILogReadService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LogReadService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public List<Log> GetAllLog()
        {
            return _unitOfWork.LogRepository.GetAll();
        }

        public List<Log> FindBy(Expression<Func<Log, bool>> predicate)
        {
            return _unitOfWork.LogRepository.FindBy(predicate);
        }

        public IEnumerable<Log> Get(Expression<Func<Log, bool>> filter = null, Func<IQueryable<Models.Log>, IOrderedQueryable<Models.Log>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.LogRepository.Get(filter, orderBy, includeProperties);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
