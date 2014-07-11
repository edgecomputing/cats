using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class Detail
    {
        public Detail()
        {
            this.GiftCertificates = new List<GiftCertificate>();
            this.GiftCertificateDetails = new List<GiftCertificateDetail>();
            this.GiftCertificateDetails1 = new List<GiftCertificateDetail>();
            this.GiftCertificateDetails2 = new List<GiftCertificateDetail>();
            this.InternalMovements = new List<InternalMovement>();
        }

        public int DetailID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MasterID { get; set; }
        public int SortOrder { get; set; }
        public virtual Master Master { get; set; }
        public virtual ICollection<GiftCertificate> GiftCertificates { get; set; }
        public virtual ICollection<GiftCertificateDetail> GiftCertificateDetails { get; set; }
        public virtual ICollection<GiftCertificateDetail> GiftCertificateDetails1 { get; set; }
        public virtual ICollection<GiftCertificateDetail> GiftCertificateDetails2 { get; set; }
        public virtual ICollection<InternalMovement> InternalMovements { get; set; }
    }
}
