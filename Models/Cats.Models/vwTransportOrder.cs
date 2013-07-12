using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class vwTransportOrder
    {
        public int TransportOrderID { get; set; }
        public string TransportOrderNo { get; set; }
        public System.DateTime OrderDate { get; set; }
        public System.DateTime RequestedDispatchDate { get; set; }
        public System.DateTime OrderExpiryDate { get; set; }
        public string BidDocumentNo { get; set; }
        public string PerformanceBondReceiptNo { get; set; }
        public int TransporterID { get; set; }
        public string ConsignerName { get; set; }
        public string TransporterSignedName { get; set; }
        public Nullable<System.DateTime> ConsignerDate { get; set; }
        public Nullable<System.DateTime> TransporterSignedDate { get; set; }
        public string ContractNumber { get; set; }
        public int TransportOrderDetailID { get; set; }
        public int FdpID { get; set; }
        public int SourceWarehouseID { get; set; }
        public decimal QuantityQtl { get; set; }
        public Nullable<decimal> DistanceFromOrigin { get; set; }
        public decimal TariffPerQtl { get; set; }
        public int RequisitionID { get; set; }
        public int CommodityID { get; set; }
        public Nullable<int> ZoneID { get; set; }
        public Nullable<int> DonorID { get; set; }
        public string FDPName { get; set; }
        public string HubName { get; set; }
        public string RequisitionNo { get; set; }
        public string CommodityName { get; set; }
        public string DonorName { get; set; }
        public string WoredaName { get; set; }
        public string ZoneName { get; set; }
        public string TransporterName { get; set; }
    }
}

