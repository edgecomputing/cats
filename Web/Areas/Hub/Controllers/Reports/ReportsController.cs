using System.Linq;
using System.Web.Mvc;
using System;
using Cats.Models.Hub;
using Cats.Models.Hub.ViewModels.Report;
using Cats.Models.Hub.ViewModels.Report.Data;
using System.Collections.Generic;
using Cats.Web.Hub.Reports;
using Cats.Web.Hub;
using DevExpress.XtraReports.UI;
using Cats.Services.Hub;

namespace Cats.Web.Hub.Controllers.Reports
{
     [Authorize]
    public partial class ReportsController : BaseController
     {
         
         private readonly IDispatchService _dispatchService;
         private readonly IReceiveService _receiveService;
         private readonly IUserProfileService _userProfileService;
         private readonly IHubService _hubService;
         private readonly ITransactionService _transactionService;
         private readonly ICommodityService _commodityService;
         private readonly ICommodityTypeService _commodityTypeService;
         private readonly IProgramService _programService;
         private readonly IAdminUnitService _adminUnitService;
         private readonly IDispatchAllocationService _dispatchAllocationService;
         private readonly ICommoditySourceService _commoditySourceService;

         public ReportsController(IDispatchService dispatchService,
             IReceiveService receiveService,
             IUserProfileService userProfileService,
             IHubService hubService,
             ITransactionService transactionService,
             ICommodityService commodityService,
             ICommodityTypeService commodityTypeService,
             IProgramService programService,
             IAdminUnitService adminUnitService,
             IDispatchAllocationService dispatchAllocationService,
             ICommoditySourceService commoditySourceService
             
             )
         {
             this._dispatchService = dispatchService;
             this._receiveService = receiveService;
             this._userProfileService = userProfileService;
             this._hubService = hubService;
             this._transactionService = transactionService;
             _commodityService = commodityService;
             _commodityTypeService = commodityTypeService;
             _programService = programService;
             _adminUnitService = adminUnitService;
             _dispatchAllocationService = dispatchAllocationService;
             _commoditySourceService = commoditySourceService;
         }
        //
        // GET: /Reports/

        public virtual ActionResult Index()
        {
            return View();
        }

       // Cats.Models.Hub.CTSContext db = new CTSContext();
        public virtual ActionResult SIReport(string siNumber)
        {
            if (!string.IsNullOrEmpty(siNumber))
            {
                // TODO: redo this report
                var dispatches = from dis in _dispatchService.GetAllDispatch()
                                 where dis.DispatchDetails.FirstOrDefault().ToString() == siNumber.Trim()
                                 select dis;

                // TODO: redo this report
                var recieves = from res in _receiveService.GetAllReceive()
                               where res.ReceiveDetails.FirstOrDefault().TransactionGroup.Transactions.FirstOrDefault().ShippingInstruction.Value == siNumber.Trim()
                               select res;

               SIReportModel model = new SIReportModel();
                foreach(Dispatch p in dispatches)
                {
                    foreach(DispatchDetail com in p.DispatchDetails)
                    {
                        TransactedStock dis = new TransactedStock();
                        dis.Warehouse = p.Hub.Name;
                        //dis.Store = p.Store.Name;
                        dis.GIN = p.GIN;
                        dis.Commodity = com.Commodity.Name;
                        dis.Date = p.DispatchDate;
                        dis.FDP = p.FDP.Name;
                        //dis.Quantity = com.DispatchedQuantityInMT;
                        dis.Region = p.FDP.AdminUnit.AdminUnit2.AdminUnit2.Name;
                        dis.Woreda = p.FDP.AdminUnit.Name;
                        dis.Zone = p.FDP.AdminUnit.AdminUnit2.Name;
                        model.Dispatched.Add(dis);
                    }
                }
                foreach (Receive p in recieves)
                {
                    foreach(ReceiveDetail com in p.ReceiveDetails)
                    {
                       TransactedStock dis = new TransactedStock();
                        dis.Warehouse = p.Hub.Name;
                        //dis.Store = p.Store.Name;
                        dis.GRN = p.GRN;
                        dis.Commodity = com.Commodity.Name;
                        dis.Date = p.ReceiptDate;
                        //dis.Quantity = com.ReceivedQuantityInMT;
                        model.Recieved.Add(dis);
                    }
                }
                return View(model);

            }
            return View(new SIReportModel());
        }

        public static MasterReportBound GetContainerReport(Cats.Models.Hub.ViewModels.Report.Data.BaseReport baseReportType)
        {
          MasterReportBound report = new MasterReportBound();
            report.DataSource = baseReportType;
            return report;
        }

        public static MasterReportBound GetFreeStockReport(Cats.Models.Hub.ViewModels.Report.Data.FreeStockReport freestockreport)
        {
            var rpt = new Cats.Web.Hub.Reports.FreeStockReport();
            rpt.DataSource = freestockreport.Programs;
            var report = new MasterReportBound();
            //report.DataSource = freestockreport.Programs ;
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

        public static MasterReportBound GetOffloadingReport(Cats.Models.Hub.ViewModels.Report.Data.OffloadingReport offloadingreport)
        {
            var rpt = new OffLoadingReport();
            rpt.DataSource = offloadingreport;
            MasterReportBound report = new MasterReportBound();
            report.DataSource = offloadingreport;
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

        public ActionResult FreeStock()
        {
            MasterReportBound report= GetFreeStock(new FreeStockFilterViewModel());
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
           var codes=ConstantsService.GetAllCodes();
            var commodityTypes=_commodityTypeService.GetAllCommodityTypeForReprot();
            var programs = _programService.GetAllProgramsForReport();
            var commodities = _commodityService.GetAllCommodityForReprot();
            var areas = _adminUnitService.GetAllAreasForReport();

            FreeStockFilterViewModel ViewModel = new FreeStockFilterViewModel(codes, commodityTypes, programs, commodities, areas);
            ViewBag.Filters = ViewModel;
            return View(report);
        }

        public ActionResult FreeStockPartial(FreeStockFilterViewModel freeStockFilterViewModel)
        {
            MasterReportBound report = GetFreeStock(freeStockFilterViewModel);
            ViewBag.ProgramID = freeStockFilterViewModel.ProgramId;
            return PartialView("FreeStockPartial",report);
        }

        public MasterReportBound GetFreeStock(FreeStockFilterViewModel freeStockFilterViewModel)
        {
            List<Cats.Models.Hub.ViewModels.Report.Data.FreeStockReport> reports = new List<Cats.Models.Hub.ViewModels.Report.Data.FreeStockReport>();
            Cats.Models.Hub.ViewModels.Report.Data.FreeStockReport freestockreport = new Cats.Models.Hub.ViewModels.Report.Data.FreeStockReport();
            UserProfile user =_userProfileService.GetUser(User.Identity.Name);

            freestockreport.Programs = _hubService.GetFreeStockGroupedByProgram(user.DefaultHub.HubID, freeStockFilterViewModel);
            freestockreport.PreparedBy = user.GetFullName();
            freestockreport.HubName = user.DefaultHub.HubNameWithOwner;
            freestockreport.ReportDate = System.DateTime.Now;
            freestockreport.ReportName = "FreeStockStatusReport";
            freestockreport.ReportTitle = "Free Stock Status";
            reports.Add(freestockreport);

            Cats.Web.Hub.Reports.FreeStockReport rpt = new Cats.Web.Hub.Reports.FreeStockReport() { DataSource = freestockreport.Programs };
            // XtraReport1 rpt = new XtraReport1() { DataSource = freestockreport.Programs[2].Details };
            MasterReportBound report = new MasterReportBound() { DataSource = reports };
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

         public MasterReportBound GetOffloading(DispatchesViewModel dispatchesViewModel)
           {
               UserProfile user = _userProfileService.GetUser(User.Identity.Name);
               OffloadingReportMain main = new OffloadingReportMain();
               List<OffloadingReport> reports = _transactionService.GetOffloadingReport(user.DefaultHub.HubID, dispatchesViewModel);
               main.reports = reports;
               main.PreparedBy = user.GetFullName();
               main.HubName = user.DefaultHub.HubNameWithOwner;
               main.ReportDate = DateTime.Now;
               main.ReportName = "OffloadingReport";
               main.ReportTitle = "Offloading";
               List<OffloadingReportMain> coll = new List<OffloadingReportMain>();
               coll.Add(main);
               OffLoadingReport rpt = new OffLoadingReport() { DataSource = reports };
               // XtraReport1 rpt = new XtraReport1() { DataSource = freestockreport.Programs[2].Details };
               MasterReportBound report = new MasterReportBound() { Name = "Offloading Report " + DateTime.Now.ToShortDateString(), DataSource = coll };
               report.rptSubReport.ReportSource = rpt;
               return report;
           }

         public ActionResult OffloadingReport()
        {
            MasterReportBound report = GetOffloading(new DispatchesViewModel());
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);

             var codes =ConstantsService.GetAllCodes();
             var commodityTypes =_commodityTypeService.GetAllCommodityTypeForReprot();;
             var programs =_programService.GetAllProgramsForReport();
             var stores =_hubService.GetAllStoreByUser(user);
             var areas = _adminUnitService.GetAllAreasForReport();
             var bidRefs = _dispatchAllocationService.GetAllBidRefsForReport();
            DispatchesViewModel ViewModel = new DispatchesViewModel(codes,commodityTypes,programs,stores,areas,bidRefs);
            ViewBag.Filters = ViewModel;
            return View(report);
        }

         public ActionResult OffloadingReportPartial(DispatchesViewModel dispatchesViewModel)
        {
           MasterReportBound report = GetOffloading(dispatchesViewModel);
           return PartialView("OffloadingReportPartial", report);
        }

        public ActionResult FreeStockReportViewerExportTo(FreeStockFilterViewModel freeStockFilterViewModel)
        {
            MasterReportBound report = GetFreeStock(freeStockFilterViewModel);
            //TODO:Install DevexpressV11  return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(report);
            return View();
        }

        public ActionResult OffloadingReportViewerExportTo(DispatchesViewModel dispatchesViewModel)
        {
            Cats.Web.Hub.Reports.MasterReportBound rep = GetOffloading(dispatchesViewModel);
            //TODO:Install DevexpressV11 return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(rep);
            return View();
        }

        public ActionResult Receive()
        {
            MasterReportBound report = GetReceiveReportByBudgetYear(new ReceiptsViewModel());
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
          
            var commoditySources = _commoditySourceService.GetAllCommoditySourceForReport();
           var ports = _receiveService.GetALlPorts();
           var codes =ConstantsService.GetAllCodes();
            var commodityTypes =  _commodityTypeService.GetAllCommodityTypeForReprot();
           var programs = _programService.GetAllProgramsForReport();
            var stores = _hubService.GetAllStoreByUser(user);
            var viewModel = new ReceiptsViewModel(codes,commodityTypes,programs,stores,commoditySources,ports);
            ViewBag.Filters = viewModel;
            return View(report);
        }

        public ActionResult ReceivePartial(ReceiptsViewModel receiptsViewModel)
        {
            MasterReportBound report = GetReceiveReportByBudgetYear(receiptsViewModel);
            return PartialView("ReceivePartial", report);
        }

        public MasterReportBound GetReceiveReport(ReceiptsViewModel receiptsViewModel)
        {
            List<Cats.Models.Hub.ViewModels.Report.Data.ReceiveReportMain> reports = new List<ReceiveReportMain>();
            Cats.Models.Hub.ViewModels.Report.Data.ReceiveReportMain receivereport = new Cats.Models.Hub.ViewModels.Report.Data.ReceiveReportMain();
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);

            receivereport.receiveReports = _transactionService.GetReceiveReport(user.DefaultHub.HubID, receiptsViewModel);//new List<ViewModels.Report.Data.ReceiveReport>();
            receivereport.PreparedBy = user.GetFullName();
            receivereport.HubName = user.DefaultHub.HubNameWithOwner;
            receivereport.ReportDate = System.DateTime.Now;
            receivereport.ReportCode = DateTime.Now.ToString();
            receivereport.ReportName = "ReceiveReport";
            receivereport.ReportTitle = "Receive Report";

            Cats.Web.Hub.Reports.ReceiveReportByBudgetYear rpt = new ReceiveReportByBudgetYear() { DataSource = receivereport.receiveReports };
            MasterReportBound report = new MasterReportBound() { Name = "Receive Report - " + DateTime.Now.ToShortDateString(), DataSource = reports };
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

        public MasterReportBound GetReceiveReportByBudgetYear(ReceiptsViewModel receiptsViewModel)
        {
            List<Cats.Models.Hub.ViewModels.Report.Data.ReceiveReportMain> reports = new List<ReceiveReportMain>();
            Cats.Models.Hub.ViewModels.Report.Data.ReceiveReportMain receivereport = new ReceiveReportMain();
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);

            receivereport.receiveReports = _transactionService.GetReceiveReport(user.DefaultHub.HubID, receiptsViewModel);//new List<ViewModels.Report.Data.ReceiveReport>();
            receivereport.PreparedBy = user.GetFullName();
            receivereport.HubName = user.DefaultHub.HubNameWithOwner;
            receivereport.ReportDate = System.DateTime.Now;
            receivereport.ReportCode = DateTime.Now.ToString();
            receivereport.ReportName = "ReceiveReport";
            receivereport.ReportTitle = "Receive Report";
            reports.Add(receivereport);

           
            Cats.Web.Hub.Reports.ReceiveReportByBudgetYear rpt = new ReceiveReportByBudgetYear() { DataSource = receivereport.receiveReports };
            MasterReportBound report = new MasterReportBound() { Name = "Receive Report - " + DateTime.Now.ToShortDateString(), DataSource = reports };
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

        public ActionResult ReceiveReportViewerExportTo(ReceiptsViewModel receiptsViewModel)
        {
            //Cats.Web.Hub.Reports.MasterReportBound rep = GetReceiveReport(receiptsViewModel);
            Cats.Web.Hub.Reports.MasterReportBound rep = GetReceiveReportByBudgetYear(receiptsViewModel);
            //TODO:Install DevexpressV11 return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(rep);
            return View();
        }

        #region Distribution

        public ActionResult DistributionReport()
        {
            DistributionViewModel newDistributionViewModel = new DistributionViewModel();
            newDistributionViewModel.PeriodId = (DateTime.Now.Month - 1/3) + 1;// current quarter by default 
            MasterReportBound report = GetDistributionReportPivot(newDistributionViewModel);
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            var codes=ConstantsService.GetAllCodes();
            var commodityTypes=_commodityTypeService.GetAllCommodityTypeForReprot();
            var programs=_programService.GetAllProgramsForReport();
            var stores=_hubService.GetAllStoreByUser(user);
            var areas=_adminUnitService.GetAllAreasForReport();
            var bidRefs=_dispatchAllocationService.GetAllBidRefsForReport();
            var viewModel = new DistributionViewModel(codes,commodityTypes,programs,stores,areas,bidRefs);
            ViewBag.Filters = viewModel;
            
            return View(report);
        }

        public ActionResult DistributionReportPartial(DistributionViewModel distributionViewModel)
        {
            MasterReportBound report = GetDistributionReportPivot(distributionViewModel);
            return PartialView("DistributionReportPartial", report);
        }

        public MasterReportBound GetDistributionReport()
        {
           var reports = new List<Cats.Models.Hub.ViewModels.Report.Data.DistributionReport>();
            var distribution = new Cats.Models.Hub.ViewModels.Report.Data.DistributionReport();
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);

            distribution.PreparedBy = user.GetFullName();
            distribution.ReportCode = DateTime.Now.ToString();
            distribution.ReportDate = DateTime.Now;
            distribution.ReportName = "DistributionReport";
            distribution.ReportTitle = "Distribution Report";
            Random ran = new Random(1);
            distribution.Rows = new List<DistributionRows>();
            for (int i = 1; i < 2; i++)
            {
                DistributionRows r = new DistributionRows();
                r.BudgetYear = DateTime.Now.Year;
                r.Region = (i % 2 == 0) ? "Amhara" : "Benshangul";
                r.Program = "Program " + i.ToString();
                r.DistributedAmount = i * decimal.Parse("2340.43674") * 45;
                int month = ran.Next(4);
                r.Quarter = 1;
                distribution.Rows.Add(r);
            }

            reports.Add(distribution);

            Cats.Web.Hub.Reports.DistributionReport rpt = new Cats.Web.Hub.Reports.DistributionReport() { DataSource = reports[0].Rows };
            MasterReportBound report = new MasterReportBound() { Name = "Distribution Report - " + DateTime.Now.ToShortDateString(), DataSource = reports };
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

        public MasterReportBound GetDistributionReportPivot(DistributionViewModel distributionViewModel)
        {
            List<Cats.Models.Hub.ViewModels.Report.Data.DistributionReport> reports = new List<Cats.Models.Hub.ViewModels.Report.Data.DistributionReport>();
            Cats.Models.Hub.ViewModels.Report.Data.DistributionReport distribution = new Cats.Models.Hub.ViewModels.Report.Data.DistributionReport();
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);

            distribution.PreparedBy = user.GetFullName();
            distribution.HubName = user.DefaultHub.HubNameWithOwner;
            distribution.ReportCode = DateTime.Now.ToString();
            distribution.ReportDate = DateTime.Now;
            distribution.ReportName = "DistributionReport";
            distribution.ReportTitle = "Distribution Report";
            distribution.Rows = new List<DistributionRows>();
            
            distribution.Rows = _transactionService.GetDistributionReport(user.DefaultHub.HubID, distributionViewModel);
             //   new List<DistributionRows>();
            //for (int i = 1; i < 5; i++)
            //{
            //    DistributionRows r = new DistributionRows();
            //    r.BudgetYear = DateTime.Now.Year;
            //    r.Region = (i % 2 == 0) ? "Amhara" : "Benshangul";
            //    r.Program = "Program " + i.ToString();
            //    r.DistributedAmount = i * decimal.Parse("2340.43674") * 45;
            //    int month = i;
            //    if (month == 0)
            //        month++;
            //    r.Month = month.ToString();
            //    r.Quarter = (i % 3 > 0) ? (i / 3) + 1 : i / 3;
            //    distribution.Rows.Add(r);
            //}

            reports.Add(distribution);

            Cats.Web.Hub.Reports.DistributionReportPivot rpt = new Cats.Web.Hub.Reports.DistributionReportPivot();
            rpt.xrPivotGrid1.DataSource = reports[0].Rows;
            MasterReportBound report = new MasterReportBound() { Name = "Distribution Report - " + DateTime.Now.ToShortDateString(), DataSource = reports };
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

        public ActionResult DistributionReportViewerExportTo(DistributionViewModel distributionViewModel)
        {
            //Cats.Web.Hub.Reports.MasterReportBound rep = GetDistributionReport();
            MasterReportBound report = GetDistributionReportPivot(distributionViewModel);
            //TODO:Install DevexpressV11  return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(report);
            return View();
        }

        #endregion

        #region Delivery

        public ActionResult DonationReport()
        {
            MasterReportBound report = GetDonationReport();

            return View(report);
        }

        public ActionResult DonationReportPartial()
        {
            MasterReportBound report = GetDonationReport();
            return PartialView("DistributionReportPartial", report);
        }

        public MasterReportBound GetDonationReport()
        {
           var reports = new List<DeliveryReport>();
            var donation = new DeliveryReport();
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);

            donation.PreparedBy = user.GetFullName();
            donation.ReportCode = DateTime.Now.ToString();
            donation.ReportDate = DateTime.Now;
            donation.ReportName = "DistributionReport";
            donation.ReportTitle = "Distribution Report";
            Random ran = new Random(1);
            donation.Rows = new List<DeliveryRows>();
            for (int i = 1; i < 200; i++)
            {
                DeliveryRows r = new DeliveryRows();
                r.SINumber = "00001283";
                r.Hub = donation.HubName;
                r.DeliveryOrderNumber = i.ToString().PadLeft(8, '0');
                r.HubOwner = "DRMFSS";
                r.PortName = "Djibuti";
                r.ShippedBy = "WFP";
                r.Vessel = "Liberty Sun";
                r.Project = "DRMFSS 4765";
                r.Commodity = "Cereal";
                r.SubCommodity = "Wheat";
                r.WareHouseNumber = i / 50 + 1;
                r.Unit = "mt";
                r.DeliveryBag = 99 * i * decimal.Parse("12");
                r.DeliveryQuantity = 67 * i * decimal.Parse("34.89");
                r.DeliveryNet = 23 * i * decimal.Parse("81");
                r.Donor = "US Aid";
                r.DeliveryType = "Donation";
                r.DeliveryReferenceNumber = i.ToString().PadLeft(8, '0');
                r.Date = DateTime.Now.ToShortDateString();
                r.TransportedBy = ((i % 3 == 0) ? " DRMFSS " : "Another Trasporter");
                r.VehiclePlateNumber = "03-A0012" + (i / 24).ToString();
                donation.Rows.Add(r);
            }

            reports.Add(donation);

            Cats.Web.Hub.Reports.DonationReportByProgram rpt = new Web.Hub.Reports.DonationReportByProgram() { DataSource = reports[0].Rows };
            MasterReportBound report = new MasterReportBound() { Name = "Donation Report - " + DateTime.Now.ToShortDateString(), DataSource = reports };
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

        public ActionResult DonationReportViewerExportTo()
        {
            MasterReportBound report = GetDonationReport();
           //TODO:Install DevexpressV11 return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(report);
            return View();
        }

        #endregion
         
    }
}
