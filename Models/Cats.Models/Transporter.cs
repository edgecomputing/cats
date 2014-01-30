using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class Transporter
    {
        public Transporter()
        {
            this.TransportOrders = new List<TransportOrder>();
            this.BidWinners=new List<BidWinner>();
            DateTime dt = DateTime.Now; ;
            this.ExperienceFrom = dt;
            this.ExperienceTo = dt;
            this.TransportBidQuotationHeaders = new List<TransportBidQuotationHeader>();
            
        }
     
        [Key]
        public int TransporterID { get; set; }
        
        //Transporter Address
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public int Region { get; set; }
        //public int RegionId { get; set; }

        [Display(Name = "Sub City")]
        public string SubCity { get; set; }
        public int Zone { get; set; }
        public int Woreda { get; set; }
        public string Kebele { get; set; }
        public string HouseNo { get; set; }
        public string TelephoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }

        public string Ownership { get; set; }
        public int VehicleCount { get; set; }
        public decimal LiftCapacityFrom { get; set; }
        public decimal LiftCapacityTo { get; set; }
        public decimal LiftCapacityTotal { get; set; }
        public decimal Capital { get; set; }

        public int EmployeeCountMale { get; set; }
        public int EmployeeCountFemale { get; set; }

        public string OwnerName { get; set; }
        public string OwnerMobile { get; set; }

        public string ManagerName { get; set; }
        public string ManagerMobile { get; set; }

        public DateTime ExperienceFrom { get; set; }
        public DateTime ExperienceTo { get; set; }
        public virtual ICollection<TransportOrder> TransportOrders { get; set; }
        public virtual ICollection<BidWinner> BidWinners { get; set; }
        public virtual ICollection<TransportBidQuotation> TransportBidQuotations { get; set; }
        public virtual ICollection<TransporterAgreementVersion> TransporterAgreementVersions { get; set; }
        public virtual ICollection<TransportBidQuotationHeader> TransportBidQuotationHeaders { get; set; }
        //public  List<AdminUnit> Regions { get; set; }
       

        
    }
}
