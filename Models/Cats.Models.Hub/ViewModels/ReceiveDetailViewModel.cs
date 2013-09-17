using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Models.Hub.Repository;


namespace Cats.Models.Hub
{

    public class ReceiveDetailViewModelDto
    {
        public Guid? ReceiveID { get; set; }
        public Guid? ReceiveDetailID { get; set; }
        public string UnitName { get; set; }
        public string CommodityGradeName { get; set; }
        public string CommodityName { get; set; }
        public decimal ReceivedQuantityInUnit { get; set; }
        public decimal ReceivedQuantityInMT { get; set; }
        public decimal SentQuantityInMT { get; set; }
        public decimal SentQuantityInUnit { get; set; }
    }
    public class ReceiveDetailViewModel 
    {
        //public ReceiveDetailViewModel()
        //{
        //    ReceiveDetailCounter = -1;
        //}
        
        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, validationContext, validationResults, false);
            return validationResults;
        }

        public Guid? ReceiveDetailID { get; set; }
        
        public int ReceiveDetailCounter { get; set; }
        
        //TODO fails the validation
       // [Required(ErrorMessage = "required")]
        public Guid? ReceiveID { get; set; }
        
        public string Description { get; set; }

        [Required(ErrorMessage="required")]
        public int UnitID { get; set; }

        [Required(ErrorMessage = "required")]
        [Range(1, 9999999.9)]
        public decimal? SentQuantityInUnit { get; set; }

        [Required(ErrorMessage = "required")]
        [Range(1, 9999999.9)]
        public decimal? ReceivedQuantityInUnit { get; set; }

        public int? CommodityGradeID { get; set; }

        [Required(ErrorMessage = "required")]
        //[Remote("NotFoundSI", "Receive", AdditionalFields = "SINumber", ErrorMessage = "The Commodity Selected is Not Found for this SINumber")]
        public int CommodityID { get; set; }

        [Required(ErrorMessage = "required")]
        [Range(0.1, 999999.99)]
      //  [UIHint("PreferedWeightMeasurment")]
        public decimal? ReceivedQuantityInMT { get; set; }

        [Required(ErrorMessage = "required")]
        [Range(0.1, 999999.99)]
      //  [UIHint("PreferedWeightMeasurment")]
        public decimal? SentQuantityInMT { get; set; }
        //Banty:Modified 23_5_2013 from EntityCollection to ICollection
        public static List<ReceiveDetailViewModel> GenerateReceiveDetailModels(ICollection<ReceiveDetail> entityCollection)
        {
            var details = new List<ReceiveDetailViewModel>();
            int count = 0;
            foreach (var receiveDetail in entityCollection)
            {
                count++;
                var receiveDetailx = GenerateReceiveDetailModel(receiveDetail);
                receiveDetailx.ReceiveDetailCounter = count;
                details.Add(receiveDetailx);
            }
            return details;
        }


        public static ReceiveDetailViewModel GenerateReceiveDetailModel(ReceiveDetail ReceiveDetailModel)
        {
            ReceiveDetailViewModel model = new ReceiveDetailViewModel();
            model.ReceiveDetailID = ReceiveDetailModel.ReceiveDetailID;
            model.UnitID = ReceiveDetailModel.UnitID;
            model.Description = ReceiveDetailModel.Description;
            model.ReceivedQuantityInMT = ReceiveDetailModel.QuantityInMT;
            model.ReceivedQuantityInUnit = ReceiveDetailModel.QuantityInUnit;
            model.CommodityGradeID = ReceiveDetailModel.CommodityGradeID;
            model.CommodityID = ReceiveDetailModel.CommodityID;
            model.SentQuantityInMT = ReceiveDetailModel.SentQuantityInMT;
            model.SentQuantityInUnit = ReceiveDetailModel.SentQuantityInUnit;
            model.ReceiveID = ReceiveDetailModel.ReceiveID;
            return model;
        }


    }
}