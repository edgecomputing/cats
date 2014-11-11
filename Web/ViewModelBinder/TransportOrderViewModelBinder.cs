using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Models;

namespace Cats.ViewModelBinder
{
    public class TransportOrderViewModelBinder
    {
        public static List<TransportOrderViewModel> BindListTransportOrderViewModel(List<TransportOrder> transportOrders, string datePref, List<WorkflowStatus> statuses)
        {
            return transportOrders.Select(transportOrder => BindTransportOrderViewModel(transportOrder, datePref, statuses)).ToList();
        }
        
        public static TransportOrderViewModel BindTransportOrderViewModel(TransportOrder transportOrder, string datePref,List<WorkflowStatus> statuses )
        {
            TransportOrderViewModel transportOrderViewModel = null;
            if (transportOrder != null)
            {
                transportOrderViewModel = new TransportOrderViewModel();
                transportOrderViewModel.BidDocumentNo = transportOrder.BidDocumentNo;
                transportOrderViewModel.OrderDate = transportOrder.OrderDate;
                transportOrderViewModel.OrderDateET =
                   transportOrder.OrderDate.ToCTSPreferedDateFormat(datePref);
                transportOrderViewModel.ContractNumber = transportOrder.ContractNumber;
                transportOrderViewModel.PerformanceBondReceiptNo = transportOrder.PerformanceBondReceiptNo;
                transportOrderViewModel.OrderExpiryDate = transportOrder.OrderExpiryDate;
                transportOrderViewModel.OrderExpiryDateET = transportOrder.OrderExpiryDate.ToCTSPreferedDateFormat(datePref);
                transportOrderViewModel.Transporter = transportOrder.Transporter.Name;
                transportOrderViewModel.RequestedDispatchDate = transportOrder.RequestedDispatchDate;
                transportOrderViewModel.RequestedDispatchDateET = transportOrder.RequestedDispatchDate.ToCTSPreferedDateFormat(datePref);
                transportOrderViewModel.TransporterID = transportOrder.TransporterID;
                transportOrderViewModel.TransportOrderNo = transportOrder.TransportOrderNo;
                transportOrderViewModel.TransportOrderID = transportOrder.TransportOrderID;
                transportOrderViewModel.StatusID = transportOrder.StatusID;
                transportOrderViewModel.StartDate = transportOrder.StartDate.ToCTSPreferedDateFormat(datePref);
                transportOrderViewModel.EndDate = transportOrder.EndDate.ToCTSPreferedDateFormat(datePref);
                transportOrderViewModel.Status =transportOrder.StatusID.HasValue?
                statuses.Find(t => t.StatusID == transportOrder.StatusID.Value).Description:string.Empty;
            }
            return transportOrderViewModel;
        }
    }
}