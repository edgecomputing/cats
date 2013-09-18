using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cats.Models.Hub.Repository;
using Cats.Models.Hub.ViewModels.Common;

namespace Cats.Models.Hub.ViewModels.Report
{
    /// <summary>
    /// view for CommitedVsDispatched view and Wrapping  filtering criteria objects
    /// </summary>
    public class CommittedVsDispatchedViewModel
    {
        public List<ProgramViewModel> Programs { get; set; }
        public List<CodesViewModel> Cods { get; set; }
        public List<CommodityTypeViewModel> CommodityTypes { get; set; }
        public List<PeriodViewModel> Periods { get; set; }
        public List<StoreViewModel> Stores { get; set; }
        public List<AreaViewModel> Areas { get; set; }



        public int? ProgramId { get; set; }
        public int? CodesId { get; set; }
        public int? CommodityTypeId { get; set; }
        public int? PeriodId { get; set; }
        public int? StoreId { get; set; }
        public int? AreaId { get; set; }
        public int? ProjectCodeId { get; set; }
        public int? ShippingInstructionId { get; set; }
        public DateTime SelectedDate { get; set; }


        public CommittedVsDispatchedViewModel()
        {
        }

        public CommittedVsDispatchedViewModel(List<StoreViewModel> stores, List<AreaViewModel> areas, List<CodesViewModel> codes, List<CommodityTypeViewModel> commodityTypes, List<ProgramViewModel> programs, UserProfile user)
        {
            this.Cods = codes; //ConstantsRepository.GetAllCodes();
            this.CommodityTypes = commodityTypes;// Repository.CommodityType.GetAllCommodityTypeForReprot();
            this.Programs = programs;//Repository.Program.GetAllProgramsForReport();
            this.Stores =stores; //Repository.Hub.GetAllStoreByUser(user);
            this.Areas = areas;//Repository.AdminUnit.GetAllAreasForReport();
        }
    }
}
