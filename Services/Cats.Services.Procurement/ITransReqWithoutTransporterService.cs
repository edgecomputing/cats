using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Procurement
{
   public interface ITransReqWithoutTransporterService:IDisposable
    {
       bool AddTransReqWithoutTransporter(TransReqWithoutTransporter transReqWithoutTransporter);
       bool DeleteTransReqWithoutTransporter(TransReqWithoutTransporter transReqWithoutTransporter);
        bool DeleteById(int id);
        bool EditTransReqWithoutTransporter(TransReqWithoutTransporter transReqWithoutTransporter);
        TransReqWithoutTransporter FindById(int id);
        List<TransReqWithoutTransporter> GetAllTransReqWithoutTransporter();
        List<TransReqWithoutTransporter> FindBy(Expression<Func<TransReqWithoutTransporter, bool>> predicate);

        IEnumerable<TransReqWithoutTransporter> Get(
             Expression<Func<TransReqWithoutTransporter, bool>> filter = null,
             Func<IQueryable<TransReqWithoutTransporter>, IOrderedQueryable<TransReqWithoutTransporter>> orderBy = null,
             string includeProperties = "");

        bool Save();
    }
}
