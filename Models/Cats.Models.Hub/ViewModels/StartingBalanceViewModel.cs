using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cats.Models.Hubs.ViewModels
{

    public class StartingBalanceViewModelDto
    {

        public String CommodityName { get; set; }
        public String ChildCommodity { get; set; }
        public String SINumber { get; set; }
        public String ProjectCode { get; set; }
        public String ProgramName { get; set; }
        public String StoreName { get; set; }
        public Int32 StackNumber { get; set; }
        public String UnitName { get; set; }
        public Decimal QuantityInUnit { get; set; }
        public Decimal QuantityInMT { get; set; }
        public String DonorName { get; set; }

    }

    public class StartingBalanceViewModel
    {

        public StartingBalanceViewModel()
        {
        }

        

        [Required(ErrorMessage = "Commodity is required")]
        public Int32 CommodityID { get; set; }

        [Required(ErrorMessage = "Program is required")]
        public Int32 ProgramID { get; set; }

        [Required(ErrorMessage = "Store is required")]
        public int StoreID { get; set; }

        [Required(ErrorMessage = "Stack number is required")]
        public int StackNumber { get; set; }

        [Required(ErrorMessage = "Project code is required")]
        [Display(Name = "Project Code")]
        public string ProjectNumber { get; set; }

        [Required(ErrorMessage = "Donor is required")]
        public Int32 DonorID { get; set; }

        [Required(ErrorMessage = "SI Number is required")]
        public string SINumber { get; set; }

        [Required(ErrorMessage = "QuantityInUnit Is Required")]
        [Display(Name = "Quantity In Unit")]
        [Range(0, double.MaxValue, ErrorMessage = "invalid quantity number")]
        public Decimal QuantityInUnit { get; set; }

        [Required(ErrorMessage = "QuantityInUnit Is Required")]
        [Display(Name = "Quantity In Unit")]
        [Range(0, double.MaxValue, ErrorMessage = "invalid quantity number")]
        public Decimal QuantityInMT { get; set; }

        [Required(ErrorMessage = "Unit is required")]
        public int UnitID { get; set; }

        public List<Commodity> Commodities { get; set; }
        public List<Program> Programs { get; set; }
        public List<Store> Stores { get; set; }
        public List<Unit> Units { get; set; }
        public List<Donor> Donors { get; set; }

        public StartingBalanceViewModel(List<Commodity> com,List<Store> stores,List<Unit> units, List<Program> programs, List<Donor> donors, UserProfile user)
        {

           Commodities = com;
           Stores = stores;
           Units = units;
           Programs = programs;
           Donors = donors;
        }
    }
}