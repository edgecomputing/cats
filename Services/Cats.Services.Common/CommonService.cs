using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cats.Data.UnitWork;

namespace Cats.Services.Common
{
    public class CommonService:ICommonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Models.CommodityType> GetCommodityTypes(System.Linq.Expressions.Expression<Func<Models.CommodityType, bool>> filter = null, Func<IQueryable<Models.CommodityType>, IOrderedQueryable<Models.CommodityType>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.CommodityTypeRepository.Get(filter, orderBy, includeProperties);
        }

        public IEnumerable<Models.Commodity> GetCommodities(System.Linq.Expressions.Expression<Func<Models.Commodity, bool>> filter = null, Func<IQueryable<Models.Commodity>, IOrderedQueryable<Models.Commodity>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.CommodityRepository.Get(filter, orderBy, includeProperties);
            
        }

        public IEnumerable<Models.Donor> GetDonors(System.Linq.Expressions.Expression<Func<Models.Donor, bool>> filter = null, Func<IQueryable<Models.Donor>, IOrderedQueryable<Models.Donor>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.DonorRepository.Get(filter, orderBy, includeProperties);
        }

        public IEnumerable<Models.Program> GetPrograms(System.Linq.Expressions.Expression<Func<Models.Program, bool>> filter = null, Func<IQueryable<Models.Program>, IOrderedQueryable<Models.Program>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.ProgramRepository.Get(filter, orderBy, includeProperties);
        }

     


        public IEnumerable<Models.Detail> GetDetails(System.Linq.Expressions.Expression<Func<Models.Detail, bool>> filter = null, Func<IQueryable<Models.Detail>, IOrderedQueryable<Models.Detail>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.DetailRepository.Get(filter, orderBy, includeProperties);
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
