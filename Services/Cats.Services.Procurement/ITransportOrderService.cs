
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;
using Cats.Models.ViewModels;

namespace Cats.Services.Procurement
{
    public interface ITransportOrderService : IDisposable
    {

        bool AddTransportOrder(TransportOrder transportOrder);
        bool DeleteTransportOrder(TransportOrder transportOrder);
        bool DeleteById(int id);
        bool EditTransportOrder(TransportOrder transportOrder);
        TransportOrder FindById(int id);
        List<TransportOrder> GetAllTransportOrder();
        List<TransportOrder> FindBy(Expression<Func<TransportOrder, bool>> predicate);
        IEnumerable<TransportOrderDetail> GetTransportOrderDetail(int requisitionId);
        IEnumerable<ReliefRequisition> GetTransportOrderReleifRequisition(int status);
        IEnumerable<TransportOrderDetail> GetTransportOrderDetailByTransportId(int transportId);
        IEnumerable<TransportOrder> Get(
                   Expression<Func<TransportOrder, bool>> filter = null,
                   Func<IQueryable<TransportOrder>, IOrderedQueryable<TransportOrder>> orderBy = null,
                   string includeProperties = "");
        IEnumerable<TransportOrder> GetByHub(
                   Expression<Func<TransportOrder, bool>> filter = null,
                   Func<IQueryable<TransportOrder>, IOrderedQueryable<TransportOrder>> orderBy = null,
                   string includeProperties = "", int hubId = 0, int statusId = 0);

        //IEnumerable<RequisitionToDispatch> GetRequisitionToDispatch();
        //IEnumerable<ReliefRequisition> GetProjectCodeAssignedRequisitions();
       bool CreateTransportOrder(int requisitionId);
       bool ReAssignTransporter(IEnumerable<TransportRequisitionWithoutWinnerModel> transReqWithTransporter,int transporterID);
       bool ApproveTransportOrder(TransportOrder transportOrder);
       bool SignTransportOrder(TransportOrder transportOrder);
       List<vwTransportOrder> GeTransportOrderRpt(int id);
       List<Transporter> GetTransporter();
       List<Hub> GetHubs();
        bool GeneratDispatchPlan(int transportOrderId);
        IOrderedEnumerable<RequisiionNoViewModel> GetZone();
        IOrderedEnumerable<RegionsViewModel> GetRegions();
        IEnumerable<TransportOrder> GetFilteredTransportOrder(
            IEnumerable<TransportRequisitionDetail> transportRequsitionDetails, int statusId);

        IEnumerable<TransportOrder> GetFilteredTransportOrder(IEnumerable<TransportOrderDetail> transportOrderDetails,
                                                              int statusId);
        List<Program> GetPrograms();
    }
}


