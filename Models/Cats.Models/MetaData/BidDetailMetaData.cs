using System.ComponentModel.DataAnnotations;
namespace Cats.Models.MetaData
{
    public class BidDetailMetaData
    {
        public decimal AmountForReliefProgram { get; set; }
        public decimal AmountForPSNPProgram { get; set; }
        [Required(ErrorMessage ="Please Enter Bid Document Price")]
        public float BidDocumentPrice { get; set; }
        [Required(ErrorMessage = "Please Enter CPO")]
        public float CBO { get; set; }
    }
}
