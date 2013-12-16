using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public class RequestDetailCommodityService : IRequestDetailCommodityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequestDetailCommodityService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public List<RequestDetailCommodity> FindBy(Expression<Func<RequestDetailCommodity, bool>> predicate)
        {
            return _unitOfWork.RequestDetailCommodityRepository.FindBy(predicate);
        }
    }
}
