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
            this.LocalPurchases=new List<LocalPurchase>();
        }
        [Key]
        public int GiftCertificateID { get; set; }
        public System.DateTime GiftDate { get; set; }
        public int DonorID { get; set; }
        //public string SINumber { get; set; }
        public int ShippingInstructionID { get; set; }
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
        public virtual IList<GiftCertificateDetail> GiftCertificateDetails { get; set; }
        public virtual ShippingInstruction ShippingInstruction { get; set; }
        public virtual ICollection<LocalPurchase> LocalPurchases  { get; set; }
        public Dictionary<string,string> ToDictionary()
        {
            var dictionary = new Dictionary<string, string>
                                 {
                                     {"GiftCertificateID", this.GiftCertificateID.ToString()},
                                     {"GiftDate", this.GiftDate.ToString()},
                                     {"DonorID", this.DonorID.ToString()},
                                     {"Donor", this.Donor.Name},
                                     {"ShippingInstructionID", this.ShippingInstructionID.ToString()},
                                     {"Vessel", this.Vessel.ToString()},
                                     {"ETA", this.ETA.ToString()},
                                     {"ReferenceNo", this.ReferenceNo.ToString()},
                                  
                                     //{"DModeOfTransport", this.DModeOfTransport.ToString()},
                                     //{"PortName", this.PortName.ToString()},
                                     //{"StatusID", this.StatusID.ToString()},
                                     //{"DeclarationNumber", this.DeclarationNumber.ToString()},
                                    
                                    
                                    
                                     //{"Program", this.Program.Name.ToString()},
                                     {"Commodity", this.GiftCertificateDetails[0].Commodity.Name},
                                     {"EstimatedPrice", this.GiftCertificateDetails[0].EstimatedPrice.ToString()},
                                     {"EstimatedTax", this.GiftCertificateDetails[0].EstimatedTax.ToString()},
                                     //{"ExpiryDate", this.GiftCertificateDetails[0].ExpiryDate.ToString()},
                                     {"WeightINMT", this.GiftCertificateDetails[0].WeightInMT.ToString()},
                                     {"BillOfLoading", this.GiftCertificateDetails[0].BillOfLoading.ToString()},
                                     {"AccountNumber", this.GiftCertificateDetails[0].AccountNumber.ToString()},
                                     {"YearPurchased", this.GiftCertificateDetails[0].YearPurchased.ToString()},
                                     {"FundSource", this.GiftCertificateDetails[0].Detail.Name.ToString()},
                                     {"Currency", this.GiftCertificateDetails[0].Detail1.Name.ToString()},
                                     {"BudgetType", this.GiftCertificateDetails[0].Detail2.Name.ToString()}
                                 };


            return dictionary;
        }
    }
}
