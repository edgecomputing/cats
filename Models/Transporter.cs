using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class Transporter
    {

        public int TransporterID { get; set; }

        //Transporter Address
        public string Region { get; set; }
        public string SubCity { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public string Kebele { get; set; }
        public string HouseNo { get; set; }
        public string TelephoneNo { get; set; }
        public string MobileNo { get; set; }

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
