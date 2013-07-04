using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class Transporter
    {
        [Key]
        public int TransporterID { get; set; }

        //Transporter Address
         [Required]
         [Display(Name = "Name")]
        public string Name { get; set; }
        public string Region { get; set; }

         [Display(Name = "Sub City")]
        public string SubCity { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
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
    }
}
