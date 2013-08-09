
using Cats.Models;

namespace Cats.Areas.EarlyWarning.Models
{
    public class GiftCertificateViewModel
    {
        public int GiftCertificateID { get; set; }
        public System.DateTime GiftDate { get; set; }
        public int DonorID { get; set; }
        public string Donor { get; set; }
        public string SINumber { get; set; }
        public string ReferenceNo { get; set; }
        public string Vessel { get; set; }
        public System.DateTime ETA { get; set; }
        public bool IsPrinted { get; set; }
        public int ProgramID { get; set; }
        public string Program { get; set; }
        public int DModeOfTransport { get; set; }
        public string PortName { get; set; }
        public string CommodityName { get; set; }
        public GiftCertificateDetail GiftCertificateDetail { get; set; } 

    }
}
