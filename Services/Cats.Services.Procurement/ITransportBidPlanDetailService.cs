using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Models.ViewModels;

namespace Cats.Services.Procurement
{
    public interface ITransportBidPlanDetailService
    {
        bool AddTransportBidPlanDetail(TransportBidPlanDetail item);
        bool UpdateTransportBidPlanDetail(TransportBidPlanDetail item);

        bool DeleteTransportBidPlanDetail(TransportBidPlanDetail item);
        bool DeleteById(int id);
        bool DeleteByBidPlanID(int id);
        TransportBidPlanDetail FindById(int id);
        List<TransportBidPlanDetail> GetAllTransportBidPlanDetail();
        List<TransportBidPlanDetail> FindBy(Expression<Func<TransportBidPlanDetail, bool>> predicate);

        IEnumerable<TransportBidPlanDetail> Get(
            System.Linq.Expressions.Expression<Func<TransportBidPlanDetail, bool>> filter = null,
            Func<IQueryable<TransportBidPlanDetail>, IOrderedQueryable<TransportBidPlanDetail>> orderBy = null,
            string includeProperties = "");
        double GetRegionPlanTotal(int bidplanid, int regionId, int programId);
        decimal GetHrdCommodityAmount(int woredaID);
        List<PSNPCommodityAmmountViewModel> GetPsnpCommodityAmount();
        decimal GetWoredaGroupedPsnpAmount(int woredaID);
    }
}