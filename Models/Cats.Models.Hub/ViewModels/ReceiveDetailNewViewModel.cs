using System;
using System.ComponentModel.DataAnnotations;
using Cats.Models.Hubs.ViewModels.Common;

namespace Cats.Models.Hubs.ViewModels
{
    public class ReceiveDetailNewViewModel
    {
        public ReceiveDetailNewViewModel()
        {
            SentQuantityInUnit = 0;
            SentQuantityInMt = 0;
            ReceivedQuantityInUnit = 0;
            ReceivedQuantityInMt = 0;

        }

        #region Properties 

        public int ReceiveDetailId2 { get; set; }
        public Guid ReceiveDetailId { get; set; }
        public Guid? ReceiveId { get; set; }

        //[Required(ErrorMessage = "required")]
        public int CommodityId { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "required")]
        public int UnitId { get; set; }

        [Required(ErrorMessage = "Sent quantity required")]
        [Range(1, 9999999.9)]
        public decimal SentQuantityInUnit { get; set; }

        [Required(ErrorMessage = "Recieved quantity is required")]
        [Range(1, 9999999.9)]
        public decimal ReceivedQuantityInUnit { get; set; }

        [Required(ErrorMessage = "required")]
        [Range(0.1, 999999.99)]
        public decimal ReceivedQuantityInMt { get; set; }

        [Required(ErrorMessage = "required")]
        [Range(0.1, 999999.99)]
        public decimal SentQuantityInMt { get; set; }

        #endregion

        //public string CommodityName { get; set; }
        [UIHint("_CommodityDropdown")]
        public CommodityViewModel CommodityViewModel { get; set; }

        [UIHint("_UnitDropdown")]
        public UnitViewModel UnitViewModel { get; set; }
    }
}