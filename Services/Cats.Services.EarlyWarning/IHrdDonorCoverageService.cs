using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
   public interface IHrdDonorCoverageService:IDisposable
    {
       bool AddHrdDonorCoverage(HrdDonorCoverage hrdDonorCoverage);
       bool DeleteHrdDonorCoverage(HrdDonorCoverage hrdDonorCoverage);
        bool DeleteById(int id);
        bool EditHrdDonorCoverage(HrdDonorCoverage hrdDonorCoverage);
        HrdDonorCoverage FindById(int id);
        List<HrdDonorCoverage> GetAllHrdDonorCoverage();
        List<HrdDonorCoverage> FindBy(Expression<Func<HrdDonorCoverage, bool>> predicate);

        IEnumerable<HrdDonorCoverage> Get(
             Expression<Func<HrdDonorCoverage, bool>> filter = null,
             Func<IQueryable<HrdDonorCoverage>, IOrderedQueryable<HrdDonorCoverage>> orderBy = null,
             string includeProperties = "");
       int NumberOfCoveredWoredas(int donorCoverageID);

       DataTable TransposeData(IEnumerable<HrdDonorCoverageDetail> donorCoverageDetails,
                               IEnumerable<RationDetail> rationDetails, string preferedWeight);
    }
}
