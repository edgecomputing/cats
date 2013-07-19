using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class TransporterMetaData
    {
        [Required(ErrorMessage = "Please Enter Start Date")]
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please Enter End Date")]
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Please Enter Bid Number", AllowEmptyStrings = false)]
        public string BidNumber { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime OpeningDate { get; set; }
        [Required(ErrorMessage = "Please Select Status")]
        public int StatusID { get; set; }
        [Required(ErrorMessage = "Please Select Bid Plan")]
        public int TransportBidPlanID { get; set; }



         [Required(ErrorMessage = "Please Enter Name")]
         [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Region")]
        public string Region { get; set; }

        [Display(Name = "Sub City")]
        public string SubCity { get; set; }

        [Display(Name = "Zone")]
        public string Zone { get; set; }

        [Display(Name = "Woreda")]
        public string Woreda { get; set; }

        [Display(Name = "Kebele")]
        public string Kebele { get; set; }

        [Display(Name = "House No")]
        public string HouseNo { get; set; }

        [Display(Name = "Telephone No")]
        public string TelephoneNo { get; set; }

        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [Display(Name = "Telephone No")]
        public string Email { get; set; }

        [Display(Name = "Ownership")]
        public string Ownership { get; set; }

        [Display(Name = "Vehicle Count")]
        public int VehicleCount { get; set; }

        [Display(Name = "Telephone No")]
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

   
    }
}
