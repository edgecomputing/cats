using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cats.Models.Hubs.ViewModels.Common;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Cats.Models.Hubs.ViewModels
{
    /// <summary>
    /// view model for Internal Movements 
    /// Internal Movements are the movements of commdity from one store to another 
    /// on the same Hub
    /// </summary>
    public class InternalMovementViewModel
    {
        [Required(ErrorMessage="Frome Store is Required")]
        [Display(Name="From Store")]
        public List<StoreViewModel> FromStore { get; set; }

        [Required(ErrorMessage = "From Stack is Required")]
        [Display(Name="From Stack")]
        public List<int> FromStack { get; set; }

        [Required(ErrorMessage = "Commodity is Required")]
        [Display(Name="Commodity")]
        public List<Commodity> Commodities { get; set; }

        [Required(ErrorMessage = "Program is Required")]
        [Display(Name="Program")]
        public List<ProgramViewModel> Programs { get; set; }

        [Required(ErrorMessage = "Project Code is Required")]
        [Display(Name = "Project Code")]
        public List<ProjectCodeViewModel> ProjectCodes { get; set; }

        [Required(ErrorMessage = "SI Number is Required")]
        [Display(Name = "SI Number")]
        public List<ShippingInstructionViewModel> ShippingInstructions { get; set; }

        [Required(ErrorMessage = "Unit is Required")]
        [Display(Name="Unit")]
        public List<Unit> Units { get; set; }

        [Display(Name="To Store")]
        [Required(ErrorMessage = "To Store is Required")]
        public List<StoreViewModel> ToStore { get; set; }

        [Display(Name="To Stack")]
        [Required(ErrorMessage = "To Stack is Required")]
        public List<int> ToStack { get; set; }

        [Display(Name="Reason")]
        [Required(ErrorMessage = "Reason is Required")]
        public List<ReasonViewModel> Reason { get; set; }

        
        public int FromStoreId { get; set; }

        [Display(Name="Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date is Required")]
        public DateTime SelectedDate { get; set; }

        public int FromStackId { get; set; }

        
        [Display(Name="Ref Number")]
        [Required]
        public string ReferenceNumber { get; set; }
        public int CommodityId { get; set; }
        public int ProgramId { get; set; }

        
        public int ProjectCodeId { get; set; }


        public int ShippingInstructionId { get; set; }
        public int UnitId { get; set; }

        [Required(ErrorMessage = "Is Required")]
        [Display(Name="Quantity In Unit")]
        [DataType("double")]
        [Range(0, double.MaxValue, ErrorMessage = "invalid quantity number")]
        public decimal QuantityInUnit { get; set; }

        [Required(ErrorMessage = "Is Required")]
        [Display(Name="Quntity in Mt")]
        [Range(0, double.MaxValue, ErrorMessage = "invalid quantity number")]
        [Remote("IsQuantityValid", "InternalMovement", AdditionalFields = "QuantityInMt, FromStoreId, FromStackId, CommodityId, ShippingInstructionId, ProjectCodeId", ErrorMessage = "You have insufficent Quantity to transfer")]
        public decimal QuantityInMt { get; set; }

        public int ToStoreId { get; set; }
        public int ToStackId { get; set; }
        public int ReasonId { get; set; }

        [Display(Name="Notes")]
        [DataType(DataType.MultilineText)]
        [UIHint("AmharicTextArea")]
        public string Note { get; set; }

        
        [Display(Name="Approved by")]
        [Required(ErrorMessage="Is Required")]
        [UIHint("AmharicTextBox")]
        public string ApprovedBy { get; set; }

        ///Methods 
        ///

        public bool IsPostBack { get; set; }
        public InternalMovementViewModel()
        {
        }


        public InternalMovementViewModel(List<StoreViewModel> fromStore,
                                         List<Commodity> commodities,
                                         List<ProgramViewModel> programs,
                                         List<Unit> units,
                                         List<StoreViewModel> toStore,
                                         List<ReasonViewModel> reasons 
            )
        {
            this.FromStore = fromStore;//repository.Hub.GetAllStoreByUser(user);
            this.FromStack = new List<int>();
            this.SelectedDate = DateTime.Now;
            //this.ReferenceNumber = "dude";
            this.Commodities = commodities;// repository.Commodity.GetAllParents();
            this.Programs = programs;//repository.Program.GetAllProgramsForReport();
            this.ProjectCodes = new List<ProjectCodeViewModel>();
            this.ShippingInstructions = new List<ShippingInstructionViewModel>();
            this.Units = units;//repository.Unit.GetAll();
            this.ToStore = toStore;// repository.Hub.GetAllStoreByUser(user);
            this.ToStack = new List<int>();
            this.Reason = reasons;// repository.Detail.GetReasonByMaster(Master.Constants.REASON_FOR_INTERNAL_MOVMENT);
            //this.Note
            //this.ApprovedBy = 
            
        }

        
    }
}
