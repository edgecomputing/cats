using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public partial class GiftCertificate
    {
        public GiftCertificate()
        {
            this.GiftCertificateDetails = new List<GiftCertificateDetail>();
        }
        [Key]
        public int GiftCertificateID { get; set; }
        public System.DateTime GiftDate { get; set; }
        public int DonorID { get; set; }
        public string SINumber { get; set; }
        public string ReferenceNo { get; set; }
        public string Vessel { get; set; }
        public System.DateTime ETA { get; set; }
        public bool IsPrinted { get; set; }
        public int ProgramID { get; set; }
        public int DModeOfTransport { get; set; }
        public string PortName { get; set; }
        public int StatusID { get; set; }
        public string DeclarationNumber { get; set; }
        public Nullable<Guid> TransactionGroupID { get; set; }
        public virtual Detail Detail { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual Program Program { get; set; }
        public virtual ICollection<GiftCertificateDetail> GiftCertificateDetails { get; set; }

        public Dictionary<string,string> ToDictionary()
        {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("GiftCertificateID",this.GiftCertificateID.ToString());


            return dictionary;
        }
    }
}
