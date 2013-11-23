using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class vwTransportOrderViewModel
    {
        public int TransportOrderID { get; set; }
        public string TransportOrderNo { get; set; }
        public string OrderDate { get; set; }
        public string RequestedDispatchDate { get; set; }
        public string OrderExpiryDate { get; set; }
        public string BidDocumentNo { get; set; }
        public string PerformanceBondReceiptNo { get; set; }
        public int TransporterID { get; set; }
        public string ConsignerName { get; set; }
        public string TransporterSignedName { get; set; }
        public string ConsignerDate { get; set; }
        public string TransporterSignedDate { get; set; }
        public string ContractNumber { get; set; }
        public int StatusID { get; set; }
        public int TransportOrderDetailID { get; set; }
        public int FdpID { get; set; }
        public int SourceWarehouseID { get; set; }
        public decimal QuantityQtl { get; set; }
        public decimal DistanceFromOrigin { get; set; }
        public decimal TariffPerQtl { get; set; }
        public int RequisitionID { get; set; }
        public int CommodityID { get; set; }
        public int ZoneID { get; set; }
        public int DonorID { get; set; }
        public string FDPName { get; set; }
        public string HubName { get; set; }
        public string RequisitionNo { get; set; }
        public string CommodityName { get; set; }
        public string DonorName { get; set; }
        public string WoredaName { get; set; }
        public string ZoneName { get; set; }
        public string TransporterName { get; set; }
        public string OrderEndDate { get; set; }
        public string OrderStartDate { get; set; }

    }
}