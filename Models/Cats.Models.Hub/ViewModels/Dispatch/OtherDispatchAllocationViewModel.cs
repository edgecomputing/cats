using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Cats.Models.Hubs.ViewModels.Dispatch
{
    /// <summary>
    /// 
    /// </summary>
   public class OtherDispatchAllocationViewModel
    {

       public OtherDispatchAllocationViewModel()
       {
           // Default both to the current date.
           AgreementDate = DateTime.Now;
           EstimatedDispatchDate = DateTime.Now;
       }
        /// <summary>
        /// Gets or sets the partition ID.
        /// </summary>
        /// <value>
        /// The partition ID.
        /// </value>
       public int? PartitionID { get; set; }
       /// <summary>
       /// Gets or sets the dispatch allocation to hub ID.
       /// </summary>
       /// <value>
       /// The dispatch allocation to hub ID.
       /// </value>
       public Guid? OtherDispatchAllocationID { get; set; }

        /// <summary>
        /// Gets or sets the agreement date.
        /// </summary>
        /// <value>
        /// The agreement date.
        /// </value>
       [Display ( Name= "Agreement Date")]
       [Required(ErrorMessage = "Agreement date is required")]
       public DateTime AgreementDate { get; set; }

        /// <summary>
        /// Gets or sets to hub ID.
        /// </summary>
        /// <value>
        /// To hub ID.
        /// </value>
       [Display(Name = "Destination Hub / Owner")]
       [Required(ErrorMessage = "Destination is required")]
       public int? ToHubID{get;set;}

       /// <summary>
       /// Gets or sets from hub ID.
       /// </summary>
       /// <value>
       /// From hub ID.
       /// </value>
       [Display(Name = "Source Hub")]
       [Required(ErrorMessage = "Source Hub is required")]
       public int? FromHubID { get; set; }

       /// <summary>
       /// Gets or sets the program ID.
       /// </summary>
       /// <value>
       /// The program ID.
       /// </value>
       [Display(Name = "From Program")]
       [Required(ErrorMessage = "Program is required")]
       public int? ProgramID { get; set; }

       /// <summary>
       /// Gets or sets the shipping instruction.
       /// </summary>
       /// <value>
       /// The shipping instruction.
       /// </value>
       [Display(Name = "SI/Batch")]
       [Required(ErrorMessage = "SI/Batch is required")]
       [Remote("IsSIValid", "DispatchAllocation", ErrorMessage = @"SI/Batch Number does not exist")]
       public string ShippingInstruction { get; set; }

       /// <summary>
       /// Gets or sets the program code.
       /// </summary>
       /// <value>
       /// The program code.
       /// </value>
       [Display(Name = "Project Code")]
       [Required(ErrorMessage = "Project code is required")]
       [Remote("IsProjectValid", "DispatchAllocation", ErrorMessage = "Project Number does not exist")]
       public string ProjectCode { get; set; }
      
       /// <summary>
       /// Gets or sets the reason ID.
       /// </summary>
       /// <value>
       /// The reason ID.
       /// </value>
       [Display(Name = "Reason for Dispatch")]
       [Required(ErrorMessage = "Reason is required")]
       public int ReasonID { get; set; }

       /// <summary>
       /// Gets or sets the estimated dispatch date.
       /// </summary>
       /// <value>
       /// The estimated dispatch date.
       /// </value>
       /// 
       [Display(Name = "Estimated Dipatch Date")]
       public DateTime EstimatedDispatchDate { get; set; }

       /// <summary>
       /// Gets or sets the reference number.
       /// </summary>
       /// <value>
       /// The reference number.
       /// </value>
       [Display(Name = "Reference Number")]
       [Required(ErrorMessage="Reference number is required")]
       public string ReferenceNumber { get; set; }

       /// <summary>
       /// Gets or sets a value indicating whether this instance is closed.
       /// </summary>
       /// <value>
       ///   <c>true</c> if this instance is closed; otherwise, <c>false</c>.
       /// </value>

       [Display(Name = "Closed / Fulfilled")]
       public bool IsClosed { get; set; }

       /// <summary>
       /// Gets or sets the commodity ID.
       /// </summary>
       /// <value>
       /// The commodity ID.
       /// </value>
       [Display(Name = "Commodity")]
       [Required(ErrorMessage = "Commodity number is required")]
       public int CommodityID {get; set;}

       /// <summary>
       /// Gets or sets the unit ID.
       /// </summary>
       /// <value>
       /// The unit ID.
       /// </value>
       [Display(Name = "Unit")]
       [Required(ErrorMessage = "Unit is required")]
       public int UnitID{get;set;}

       /// <summary>
       /// Gets or sets the quantity in unit.
       /// </summary>
       /// <value>
       /// The quantity in unit.
       /// </value>
       [Display(Name = "Quantity in Unit")]
       [Required(ErrorMessage = "Quantity in Unit is required")]
       public decimal QuantityInUnit { get; set; }

       /// <summary>
       /// Gets or sets the quantity in MT.
       /// </summary>
       /// <value>
       /// The quantity in MT.
       /// </value>
       //[Display(Name = "Quantity in MT")]
       [Required(ErrorMessage = "Quantity in MT is required")]
       [RegularExpression("[0-9.]*", ErrorMessage = "Only numbers allowed")]
       [Range(0, 999999.99)]
       [UIHint("PreferedWeightMeasurment")]
       public Decimal QuantityInMT { get; set; }


      

       /// <summary>
       /// Gets or sets the remark.
       /// </summary>
       /// <value>
       /// The remark.
       /// </value>
       public string Remark { get; set; }

       /// <summary>
       /// Gets or sets the units.
       /// </summary>
       /// <value>
       /// The units.
       /// </value>
       public List<Unit> Units { get; set; }

       /// <summary>
       /// Gets or sets to hubs.
       /// </summary>
       /// <value>
       /// To hubs.
       /// </value>
       public List<Hub> ToHubs
       {
           get;
           set;
       }

       /// <summary>
       /// Gets or sets from hubs.
       /// </summary>
       /// <value>
       /// From hubs.
       /// </value>
       public List<Hub> FromHubs
       {
           get;
           set;
       }

       /// <summary>
       /// Gets or sets the programs.
       /// </summary>
       /// <value>
       /// The programs.
       /// </value>
       public List<Program> Programs { get; set; }

       /// <summary>
       /// Gets or sets the commodities.
       /// </summary>
       /// <value>
       /// The commodities.
       /// </value>
       public List<Commodity> Commodities { get; set; }


       public List<CommodityType> CommodityTypes { get; set; }

        /// <summary>
       /// Gets or sets the transfer modes.
       /// </summary>
       /// <value>
       /// The transfer modes.
       /// </value>
       public List<LookupViewModel> Reasons { get; set; }

       [Required]
       public int CommodityTypeID { get; set; }
       /// <summary>
       /// Inits the transfer.
       /// </summary>
       /// <param name="user">The user.</param>
       /// <param name="repository">The repository.</param>
       public void InitTransfer(UserProfile user, List<Hub> hubs ,List<Commodity> commodities,List<CommodityType> commodityTypes  ,List<Program> programs,List<Unit> units )
       {
           FromHubID = user.DefaultHub.HubID;
           ToHubs = hubs;// repository.Hub.GetOthersHavingSameOwner(user.DefaultHub);
           FromHubs = user.UserAllowedHubs;
           Commodities = commodities;// repository.Commodity.GetAllParents();
           CommodityTypes = commodityTypes;// repository.CommodityType.GetAll();
           Programs = programs;// repository.Program.GetAll();
           Units = units;// repository.Unit.GetAll();
           Reasons = new List<LookupViewModel>()
           {
               new LookupViewModel(){ID =1 ,Name = "Transfer"},
               new LookupViewModel(){ID =2 ,Name = "Swap"}
           };

       }

       /// <summary>
       /// Inits the loan.
       /// </summary>
       /// <param name="user">The user.</param>
       /// <param name="repository">The repository.</param>
       public void InitLoan(UserProfile user, List<Hub> hubs, List<Commodity> commodities, List<CommodityType> commodityTypes, List<Program> programs, List<Unit> units)
       {
           FromHubID = user.DefaultHub.HubID;
           ToHubs = hubs;// repository.Hub.GetOthersWithDifferentOwner(user.DefaultHub);
           FromHubs = user.UserAllowedHubs;
           Commodities = commodities;// repository.Commodity.GetAllParents();
           CommodityTypes = commodityTypes;// repository.CommodityType.GetAll();
           Programs = programs;// repository.Program.GetAll();
           Units = units;// repository.Unit.GetAll();
           Reasons = new List<LookupViewModel>()
           {
               new LookupViewModel(){ID = 3 ,Name = "Loan Out"},
               new LookupViewModel(){ID = 4 ,Name = "Repayment"}
           };
       }
    }


}
