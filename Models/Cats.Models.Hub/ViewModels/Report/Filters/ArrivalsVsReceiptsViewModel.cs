using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cats.Models.Hub.Repository;
using Cats.Models.Hub.ViewModels.Common;

namespace Cats.Models.Hub.ViewModels.Report
{
    /// <summary>
    /// View Model for ArrivalVsReceipt view and Wrapping  filtering criteria objects
    /// </summary>
    public class ArrivalsVsReceiptsViewModel
    {
        public List<ProgramViewModel> Programs { get; set; }
        public List<CodesViewModel> Cods { get; set; }
        public List<PortViewModel> Ports { get; set; }
        public List<CommoditySourceViewModel> CommoditySources { get; set; }
        public List<CommodityTypeViewModel> CommodityTypes { get; set; }
        public List<PeriodViewModel> Periods { get; set; }
        public List<StoreViewModel> Stores { get; set; }

        public int? ProgramId { get; set; }
        public int? CodesId { get; set; }
        public int? PortId { get; set; }
        public int? CommoditySourceId { get; set; }
        public int? CommodityTypeId { get; set; }
        public int? PeriodId { get; set; }
        public int? ProjectCodeId { get; set; }
        public int? ShippingInstructionId { get; set; }


        public ArrivalsVsReceiptsViewModel()
        {
        }

        public ArrivalsVsReceiptsViewModel(List<CommoditySourceViewModel> commoditySourceViewModels,List<PortViewModel> portViewModels,List<CodesViewModel>  codesViewModels,List<CommodityTypeViewModel> commodityTypeViewModels,List<ProgramViewModel> programViewModels  , UserProfile user)
        {
            this.Periods = GetAllPeriod();
            this.CommoditySources = commoditySourceViewModels;
            this.Ports = portViewModels;
            this.Cods = codesViewModels;
            this.CommodityTypes = commodityTypeViewModels;// repository.CommodityType.GetAllCommodityTypeForReprot();
            this.Programs = programViewModels; //repository.Program.GetAllProgramsForReport();
        }


        public List<PeriodViewModel> GetAllPeriod()
        {
            List<PeriodViewModel> periodes = new List<PeriodViewModel>();
            periodes.Add(new PeriodViewModel { PeriodId = 1, PeriodName = "Today" });
            periodes.Add(new PeriodViewModel { PeriodId = 2, PeriodName = "Today and Tomorrow " });
            periodes.Add(new PeriodViewModel { PeriodId = 3, PeriodName = "Coming Week" });
            periodes.Add(new PeriodViewModel { PeriodId = 4, PeriodName = "Coming Month" });
            periodes.Add(new PeriodViewModel { PeriodId = 5, PeriodName = "forever" });
            return periodes;
        }

        public PeriodViewModel GetPeriod(int periodId)
        {
            PeriodViewModel period = new PeriodViewModel();
            if (periodId == 1)
            {
                period.PeriodType = PeriodType.Daily;
                period.IsRange = false;
                period.SelectedDate = DateTime.Now;
            }
            else if (periodId == 2)
            {
                period.PeriodType = PeriodType.Daily;
                period.IsRange = true;
                period.StartDate = DateTime.Now;
                period.EndDate = DateTime.Now.AddDays(1);
            }
            else if (periodId == 3)
            {
                period.PeriodType = PeriodType.Daily;
                period.IsRange = true;
                period.StartDate = DateTime.Now;
                period.EndDate = DateTime.Now.AddDays(7);
            }
            else if (periodId == 4)
            {
                period.PeriodType = PeriodType.Monthly;
                period.IsRange = true;
                period.StartYear = DateTime.Now.Year;
                period.StartMonth = DateTime.Now.Month;
                period.EndYear = DateTime.Now.AddMonths(1).Year;
                period.EndMonth = DateTime.Now.AddMonths(1).Month;
            }
            else if (periodId == 5)
            {
                period.IsForever = true;
            }
            return period;
        }
    }
}
