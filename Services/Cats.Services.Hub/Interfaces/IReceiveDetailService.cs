
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hub;


namespace Cats.Services.Hub
{
    public interface IReceiveDetailService:IDisposable
    {

        bool AddReceiveDetail(ReceiveDetail receiveDetail);
        bool DeleteReceiveDetail(ReceiveDetail receiveDetail);
        bool DeleteById(int id);
        bool EditReceiveDetail(ReceiveDetail receiveDetail);
        ReceiveDetail FindById(int id);
        List<ReceiveDetail> GetAllReceiveDetail();
        List<ReceiveDetail> FindBy(Expression<Func<ReceiveDetail, bool>> predicate);

        List<ReceiveDetail> GetByReceiveId(Guid receiveId);
        List<ReceiveDetailViewModelDto> ByReceiveIDetached(Guid? receiveId, string weightMeasurmentCode);

     

        IEnumerable<ReceiveDetail> Get(
            Expression<Func<ReceiveDetail, bool>> filter = null,
            Func<IQueryable<ReceiveDetail>, IOrderedQueryable<ReceiveDetail>> orderBy = null,
            string includeProperties = "");


    }
}


