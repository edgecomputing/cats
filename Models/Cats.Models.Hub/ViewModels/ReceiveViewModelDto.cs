using System;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs.ViewModels
{
    public class ReceiveViewModelDto
    {
        public Guid? ReceiveID { get; set; }
        public string GRN { get; set; }
        [UIHint("DateTime")]
        [DataType(DataType.DateTime)]
        public DateTime ReceiptDate { get; set; }
        public string ReceivedByStoreMan { get; set; }
    }
}