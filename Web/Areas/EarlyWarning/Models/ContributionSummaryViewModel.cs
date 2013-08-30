namespace Cats.Areas.EarlyWarning.Models
{
    public class ContributionSummaryViewModel
    {
        public int ContributionID { get; set; }
        public int  HRDID { get; set; }
        public string Donor { get; set; }
        public int DonorID { get; set; }
        public int CommodityID { get; set; }
        public string Commodity { get; set; }
        public decimal Ammount { get; set; }
        public decimal BlendedFood { get; set; }
        public decimal Oil { get; set; }
        public decimal Pulse { get; set; }
        public decimal Grain { get; set; }
    }
}