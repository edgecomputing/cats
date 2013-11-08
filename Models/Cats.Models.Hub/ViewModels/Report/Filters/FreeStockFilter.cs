using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cats.Models.Hubs.Repository;
using Cats.Models.Hubs.ViewModels.Common;

namespace Cats.Models.Hubs.ViewModels.Report
{
    /// <summary>
    /// view for FreeStockFilter view and Wrapping  filtering criteria objects
    /// </summary>
    public class FreeStockFilterViewModel
    {
        public List<ProgramViewModel> Programs { get; set; }
        public List<CodesViewModel> Cods { get; set; }
        public List<CommodityTypeViewModel> CommodityTypes { get; set; }
        public List<PeriodViewModel> Periods { get; set; }
        public List<CommodityViewModel> Commodities { get; set; }
        public List<AreaViewModel> Areas { get; set; }



        public int? ProgramId { get; set; }
        public int? CodesId { get; set; }
        public int? CommodityTypeId { get; set; }
        public int? PeriodId { get; set; }
        public int? CommodityId { get; set; }
        public int? AreaId { get; set; }
        public int? ProjectCodeId { get; set; }
        public int? ShippingInstructionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }



        public FreeStockFilterViewModel()
        {
        }

        public FreeStockFilterViewModel(List<CodesViewModel> codes, List<CommodityTypeViewModel> commodityTypes, List<ProgramViewModel> programs, List<CommodityViewModel> commodities,
            List<AreaViewModel> areas)
        {
            this.Periods = GetAllPeriod();
            this.Cods = codes;// ConstantsRepository.GetAllCodes();
            this.CommodityTypes = commodityTypes;// Repository.CommodityType.GetAllCommodityTypeForReprot();
            this.Programs = programs;// Repository.Program.GetAllProgramsForReport();
            this.Commodities = commodities;// Repository.Commodity.GetAllCommodityForReprot();
            this.Areas = areas;// Repository.AdminUnit.GetAllAreasForReport();
        }

        public List<PeriodViewModel> GetAllPeriod()
        {
            List<PeriodViewModel> periodes = new List<PeriodViewModel>();
            periodes.Add(new PeriodViewModel { PeriodId = 6, PeriodName = "Current Week" });
            periodes.Add(new PeriodViewModel { PeriodId = 8, PeriodName = "Custome Date Range" });
            periodes.Add(new PeriodViewModel { PeriodId = 9, PeriodName = "Allocation Round" });
            periodes.Add(new PeriodViewModel { PeriodId = 10, PeriodName = "Allocation Year + Month" });
            return periodes;
        }
    }
}
