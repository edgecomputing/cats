using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Models;

namespace Cats.ViewModelBinder
{
    public class vwTransportOrderViewModelBinder
    {
        public static List<vwTransportOrderViewModel> BindListvwTransportOrderViewModel(List<vwTransportOrder> transportOrders, string datePref)
        {
            return transportOrders.Select(transportOrder => BindvwTransportOrderViewModel(transportOrder, datePref)).ToList();
        }

        public static vwTransportOrderViewModel BindvwTransportOrderViewModel(vwTransportOrder vwTransportOrder, string datePref) {
            vwTransportOrderViewModel vwTransportOrderView = null;
            
            if (vwTransportOrder != null)
            {
                vwTransportOrderView = new vwTransportOrderViewModel();
                
                vwTransportOrderView.BidDocumentNo = vwTransportOrder.BidDocumentNo;
                vwTransportOrderView.OrderDate = vwTransportOrder.OrderDate.ToCTSPreferedDateFormat(datePref);
                vwTransportOrderView.RequestedDispatchDate = vwTransportOrder.RequestedDispatchDate.ToCTSPreferedDateFormat(datePref);
                vwTransportOrderView.OrderExpiryDate = vwTransportOrder.OrderExpiryDate.ToCTSPreferedDateFormat(datePref);
                vwTransportOrderView.TransportOrderID = vwTransportOrder.TransportOrderID;
                vwTransportOrderView.TransportOrderNo = vwTransportOrder.TransportOrderNo;
                vwTransportOrderView.PerformanceBondReceiptNo = vwTransportOrder.PerformanceBondReceiptNo;
                vwTransportOrderView.TransporterID = vwTransportOrder.TransporterID;
                vwTransportOrderView.ConsignerName = vwTransportOrder.ConsignerName;
                vwTransportOrderView.TransporterSignedName = vwTransportOrder.TransporterSignedName;
                vwTransportOrderView.ConsignerDate = vwTransportOrder.ConsignerDate.Value.ToCTSPreferedDateFormat(datePref);
                vwTransportOrderView.TransporterSignedDate = vwTransportOrder.TransporterSignedDate.Value.ToCTSPreferedDateFormat(datePref);
                vwTransportOrderView.ContractNumber = vwTransportOrder.ContractNumber;
                vwTransportOrderView.TransportOrderDetailID = vwTransportOrder.TransportOrderDetailID;
                vwTransportOrderView.FdpID = vwTransportOrder.FdpID;
                vwTransportOrderView.SourceWarehouseID = vwTransportOrder.SourceWarehouseID;
                vwTransportOrderView.QuantityQtl = vwTransportOrder.QuantityQtl;
                vwTransportOrderView.DistanceFromOrigin = vwTransportOrder.DistanceFromOrigin.HasValue ? vwTransportOrder.DistanceFromOrigin.Value:default(decimal);
                vwTransportOrderView.TariffPerQtl = vwTransportOrder.TariffPerQtl;
                vwTransportOrderView.RequisitionID = vwTransportOrder.RequisitionID;
                vwTransportOrderView.CommodityID = vwTransportOrder.CommodityID;
                //vwTransportOrderView.ZoneID = 5;//vwTransportOrder.ZoneID.HasValue ? vwTransportOrder.ZoneID.Value : default(int);
                //vwTransportOrderView.DonorID = 2;// vwTransportOrder.DonorID.Value;
                vwTransportOrderView.FDPName = vwTransportOrder.FDPName;
                vwTransportOrderView.HubName = vwTransportOrder.HubName;
                vwTransportOrderView.RequisitionNo = vwTransportOrder.RequisitionNo;
                vwTransportOrderView.CommodityName = vwTransportOrder.CommodityName;
                vwTransportOrderView.DonorName = vwTransportOrder.DonorName;
                vwTransportOrderView.WoredaName = vwTransportOrder.WoredaName;
                vwTransportOrderView.ZoneName = vwTransportOrder.ZoneName;
                vwTransportOrderView.TransporterName = vwTransportOrder.TransporterName;
                vwTransportOrderView.OrderEndDate = vwTransportOrder.OrderEndDate.ToCTSPreferedDateFormat(datePref);
                vwTransportOrderView.OrderStartDate = vwTransportOrder.OrderStartDate.ToCTSPreferedDateFormat(datePref);
            }
            return vwTransportOrderView;
        }
    }
}