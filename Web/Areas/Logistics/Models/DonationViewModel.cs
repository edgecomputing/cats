using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
namespace Cats.Areas.Logistics.Models
{
    public class DonationViewModel
    {
        public int DonationHeaderPlanID { get; set; }
        public int ShippingInstructionId { get; set; }
        public Nullable<int> GiftCertificateID { get; set; }
        public int CommodityID { get; set; }
        public int DonorID { get; set; }
        public int ProgramID { get; set; }
        public System.DateTime ETA { get; set; }
        public string Vessel { get; set; }
        public string ReferenceNo { get; set; }
        public Nullable<int> ModeOfTransport { get; set; }
        public Nullable<System.Guid> TransactionGroupID { get; set; }
        public Nullable<bool> IsCommited { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<System.DateTime> AllocationDate { get; set; }
        public string Remark { get; set; }
        public virtual ICollection<DonationPlanDetail> DonationPlanDetails { get; set; }
       public virtual UserProfile UserProfile { get; set; }

       public List<Commodity> Commodities { get; set; }
       public List<Donor> Donors { get; set; }
       public List<Hub> Hubs { get; set; }
       public List<Hub> AllHubs { get; set; }
       public List<GiftCertificateDetail> GiftCertificateDetail { get; set; }
       public List<Program> Programs { get; set; }
       public List<CommoditySource> CommoditySources { get; set; }
       public List<CommodityType> CommodityTypes { get; set; }

       public DonationViewModel(List<Commodity> commodities, List<Donor> donors, List<Hub> allHubs, List<Program> programs, List<CommoditySource> commoditySources, List<CommodityType> commodityTypes, UserProfile user)
        {
            InitalizeViewModel(  commodities,  donors ,  allHubs,  programs  , commoditySources,  commodityTypes  );
        }

        /// <summary>
        /// Initalizes the view model.
        /// </summary>
        public void InitalizeViewModel(List<Commodity> commodities,List<Donor> donors ,List<Hub> allHubs,List<Program> programs  ,List<CommoditySource> commoditySources,List<CommodityType> commodityTypes  )
        {
            Commodities = commodities;
            Donors = donors;
            AllHubs = allHubs;
            Hubs = allHubs;
            Programs = programs;
            CommoditySources = commoditySources;
            CommodityTypes = commodityTypes;
        }
    }
}