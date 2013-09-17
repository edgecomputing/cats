using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cats.Models.Hub.Repository;
using Cats.Models.Hub.ViewModels.Common;

namespace Cats.Models.Hub.ViewModels.Report
{
    /// <summary>
    /// view for Dispatches view and Wrapping  filtering criteria objects
    /// </summary>
    public class DistributionViewModel
    {
        public List<BidRefViewModel> BidRefs { get; set; }
        public List<ProgramViewModel> Programs { get; set; }
        public List<CodesViewModel> Cods { get; set; }
        public List<CommodityTypeViewModel> CommodityTypes { get; set; }
        public List<PeriodViewModel> Periods { get; set; }
        public List<StoreViewModel> Stores { get; set; }
        public List<AreaViewModel> Areas { get; set; }


        public string bidRefId { set; get; }
        public int? ProgramId { get; set; }
        public int? CodesId { get; set; }
        public int? CommodityTypeId { get; set; }
        public int? PeriodId { get; set; }
        public int? StoreId { get; set; }
        public int? AreaId { get; set; }
        public int? ProjectCodeId { get; set; }
        public int? ShippingInstructionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public DistributionViewModel()
        {
        }

        public DistributionViewModel(List<CodesViewModel> codes, List<CommodityTypeViewModel> commodityTypes, List<ProgramViewModel> programs, List<StoreViewModel> stores,
            List<AreaViewModel> areas,List<BidRefViewModel> bidRefs)
        {
            this.Periods = GetAllPeriod();
            this.Cods = codes;//ConstantsRepository.GetAllCodes();
            this.CommodityTypes = commodityTypes;// Repository.CommodityType.GetAllCommodityTypeForReprot();
            this.Programs = programs;// Repository.Program.GetAllProgramsForReport();
            this.Stores = stores;// Repository.Hub.GetAllStoreByUser(user);
            this.Areas = areas;// Repository.AdminUnit.GetAllAreasForReport();
            this.BidRefs = bidRefs;// Repository.DispatchAllocation.GetAllBidRefsForReport();
        }

        public List<PeriodViewModel> GetAllPeriod()
        {
            List<PeriodViewModel> periodes = new List<PeriodViewModel>();
            periodes.Add(new PeriodViewModel { PeriodId = 1, PeriodName = "Q1" });
            periodes.Add(new PeriodViewModel { PeriodId = 2, PeriodName = "Q2" });
            periodes.Add(new PeriodViewModel { PeriodId = 3, PeriodName = "Q3" });
            periodes.Add(new PeriodViewModel { PeriodId = 4, PeriodName = "Q4" });
            return periodes;
        }
    }
}
