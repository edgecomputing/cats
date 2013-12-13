using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public interface ITransporterAgreementVersionService
    {
        bool AddTransporterAgreementVersion(TransporterAgreementVersion transporterAgreementVersion);
        bool DeleteTransporterAgreementVersion(TransporterAgreementVersion transporterAgreementVersion);
        bool DeleteById(int id);
        bool EditTransporterAgreementVersion(TransporterAgreementVersion transporterAgreementVersion);
        TransporterAgreementVersion FindById(int id);
        List<TransporterAgreementVersion> GetAllTransporterAgreementVersion();
        List<TransporterAgreementVersion> FindBy(Expression<Func<TransporterAgreementVersion, bool>> predicate);

        IEnumerable<TransporterAgreementVersion> Get(
            System.Linq.Expressions.Expression<Func<TransporterAgreementVersion, bool>> filter = null,
            Func<IQueryable<TransporterAgreementVersion>, IOrderedQueryable<TransporterAgreementVersion>> orderBy = null,
            string includeProperties = "");
    }
}
