using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Cats.Models.Hub;

namespace Cats.Models.Hub
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// 
    /// 
    public class DispatchDetailModelDto
    {
        public Guid? DispatchDetailID { get; set; }
        public Guid? DispatchID { get; set; }
        public string UnitName { get; set; }
        public string CommodityName { get; set; }
        public decimal RequestedQuantityMT { get; set; }
        public decimal DispatchedQuantityMT { get; set; }
        public decimal RequestedQuantityInUnit { get; set; }
        public decimal DispatchedQuantityInUnit { get; set; }
 
    }

    public class DispatchDetailModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public Guid? Id { get; set; }

        //[Required(ErrorMessage = "required")]
        public Guid? DispatchID { get; set; }


        public int DispatchDetailCounter { get; set; }

        /// <summary>
        /// Gets or sets the name of the commodity.
        /// </summary>
        /// <value>
        /// The name of the commodity.
        /// </value>
        public string CommodityName { get; set; }

        /// <summary>
        /// Gets or sets the commodity ID.
        /// </summary>
        /// <value>
        /// The commodity ID.
        /// </value>
        [Required(ErrorMessage = "required")]
        public int CommodityID { get; set; }

        /// <summary>
        /// Gets or sets the requested quantity MT.
        /// </summary>
        /// <value>
        /// The requested quantity MT.
        /// </value>
        [Required(ErrorMessage="required")]
        //[UIHint("DisabledQtty")]
        [Range(0.1,999999.99)]
        [Display(Name = "Requested Qty (MT)")]
        public decimal? RequestedQuantityMT { get; set; }


        /// <summary>
        /// Gets or sets the dispatched quantity MT.
        /// </summary>
        /// <value>
        /// The dispatched quantity MT.
        /// </value>
        [Required(ErrorMessage = "required")]
       // [UIHint("DisabledQtty")]
        [Range(0.1, 999999.99)]
        [Display(Name = "Dispatched Qty (MT)")]
        public decimal? DispatchedQuantityMT { get; set; }

        /// <summary>
        /// Gets or sets the requested quantity.
        /// </summary>
        /// <value>
        /// The requested quantity.
        /// </value>
        [Required(ErrorMessage = "required")]
        //[UIHint("DisabledQtty")]
        [Range(1, 9999999.9)]
        [Display(Name = "Requested Qty (Unit)")]
        public decimal? RequestedQuantity { get; set; }

        /// <summary>
        /// Gets or sets the dispatched quantity.
        /// </summary>
        /// <value>
        /// The dispatched quantity.
        /// </value>
        [Required(ErrorMessage = "required")]
        //[UIHint("DisabledQtty")]
        [Range(1, 9999999.9)]
        [Display(Name = "Dispatched Qty (Unit)")]
        public decimal? DispatchedQuantity { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        [Required(ErrorMessage = "required")]
        public int Unit { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchDetailModel"/> class.
        /// </summary>
        public DispatchDetailModel()
        {
            //TODO: check if this should be the default unit.
            this.Unit = 1;
        }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, validationContext, validationResults, false);
            return validationResults;
        }
        //Modified Banty:23_5_2013 from EntityCollection<DipatchDetail> to ICollection<DispatchDetail>
        public static List<DispatchDetailModel> GenerateDispatchDetailModels(ICollection<DispatchDetail> dispatchDetails)
        {
            var details = new List<DispatchDetailModel>();
            int count = 0;
            foreach (var dispatchDetail in dispatchDetails)
            {
                count++;
                var disptachDetailx = GenerateReceiveDetailModel(dispatchDetail);
                disptachDetailx.DispatchDetailCounter = count;
                details.Add(disptachDetailx);
            }
            return details;
        }

        private static DispatchDetailModel GenerateReceiveDetailModel(DispatchDetail dispatchDetail)
        {
            DispatchDetailModel model = new DispatchDetailModel();
            model.Id = dispatchDetail.DispatchDetailID;
            model.DispatchID = dispatchDetail.DispatchID;

            model.CommodityID = dispatchDetail.CommodityID;
            model.Description = dispatchDetail.Description;
            model.Unit = dispatchDetail.UnitID;
            model.RequestedQuantity = dispatchDetail.RequestedQunatityInUnit;
            model.RequestedQuantityMT = dispatchDetail.RequestedQuantityInMT;
            model.DispatchedQuantity = dispatchDetail.DispatchedQuantityInUnit;
            model.DispatchedQuantityMT = dispatchDetail.DispatchedQuantityInMT;

            return model;
        }
    }

    public class CommodityModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}