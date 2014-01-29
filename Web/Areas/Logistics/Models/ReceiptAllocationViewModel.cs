using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;

namespace Cats.Areas.Logistics.Models
{
    public class ReceiptAllocationViewModel
    {
           

        
        public Int32 PartitionID { get; set; }

        public Guid? ReceiptAllocationID { get; set; }

   
        public Boolean IsCommited { get; set; }

        [Required(ErrorMessage = "ETA is required")]
        [DataType(DataType.DateTime)]
        public DateTime ETA { get; set; }

        [Required(ErrorMessage = "Project Number is required")]
        [StringLength(50)]
        public String ProjectNumber { get; set; }

        //public Int32 GiftCertificateID { get; set; }

        [Required(ErrorMessage = "Commodity is required")]
        public Int32 CommodityID { get; set; }

        [StringLength(50)]
        [Required]
        //[Remote("SIMustBeInGift", "ReceiptAllocation", AdditionalFields = "CommoditySourceID", ErrorMessage = "The SInumber not Found in Gift Certificate")]
        [Remote("SINotUnique", "ReceiptAllocation", AdditionalFields = "CommoditySourceID", ErrorMessage = "The SInumber given is Not valid for this Commodity Source")]
        public String SINumber { get; set; }
        
        [Required(ErrorMessage = "Quantity is required")]
        [RegularExpression("[0-9.]*", ErrorMessage = "Only numbers allowed")]
        [Range(0, 999999999.99)]
        [UIHint("PreferedWeightMeasurment")]
        public Decimal QuantityInMT { get; set; }

        [Required(ErrorMessage = "Quantity In Unit is required")]
        [Range(0, 999999999.99)]
        public Decimal QuantityInUnit { get; set; }

        [Required(ErrorMessage = "Hub is required")]
        public Int32 HubID { get; set; }

        [Required(ErrorMessage = "Donor is required")]
        public Int32? DonorID { get; set; }

        public Int32? GiftCertificateDetailID { get; set; }

        [Required(ErrorMessage = "Program is required")]
        public Int32 ProgramID { get; set; }

        [Required(ErrorMessage = "Commodity Source is required")]
        //[Remote("SINotUnique", "ReceiptAllocation", AdditionalFields = "SINumber", ErrorMessage = "The SInumber given is Not valid for this Commodity Source")]
        public Int32 CommoditySourceID { get; set; }

        [StringLength(50)]
        [Required]
        public String PurchaseOrder { get; set; }

        [StringLength(50)]
        [Required]
        public String SupplierName { get; set; }

        [Required]
        [Remote("SelfReference", "ReceiptAllocation", AdditionalFields = "HubID", ErrorMessage = "Hub can not be the same as Destination")]
        public Int32? SourceHubID { get; set; }

        [StringLength(50)]
        public String OtherDocumentationRef { get; set; }

        [StringLength(5000)]
        public String Remark { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, validationContext, validationResults, false);
            return validationResults;
        }

        [Required]
        public int CommodityTypeID { get; set; }

        public string CommodityTypeText { get; set; }
        public string CommodityText { get; set; }

        public List<Commodity> Commodities { get; set; }
        public List<Donor> Donors { get; set; }
        public List<Hub> Hubs { get; set; }
        public List<Hub> AllHubs { get; set; }
        public List<GiftCertificateDetail> GiftCertificateDetail { get; set; }
        public List<Program> Programs { get; set; }
        public List<CommoditySource> CommoditySources { get; set; }
        public List<CommodityType> CommodityTypes { get; set; }

        
        public ReceiptAllocationViewModel(List<Commodity> commodities, List<Donor> donors, List<Program> programs, List<CommodityType> commodityTypes)
        {
           
            InitalizeViewModel(commodities, donors, programs, commodityTypes);
        }

       
        public void InitalizeViewModel(List<Commodity> commodities, List<Donor> donors, List<Program> programs, List<CommodityType> commodityTypes)
        {
            Commodities = commodities;
            Donors = donors;
           
            Programs = programs;
           
            CommodityTypes = commodityTypes;
        }
    
    }
}