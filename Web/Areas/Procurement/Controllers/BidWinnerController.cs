using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Cats.Areas.Procurement.Models;
using Cats.Data.UnitWork;
using Cats.Helpers;
using Cats.Models.Constant;
using Cats.Models.ViewModels.Bid;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Cats.Services.Common;
using Cats.Models;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;

namespace Cats.Areas.Procurement.Controllers
{
    public class BidWinnerController : Controller
    {
        // GET: /Procurement/Bid/
        private readonly IBidService _bidService;
        //private readonly IApplicationSettingService _applicationSettingService;
        //private readonly ITransportBidQuotationService _bidQuotationService;
        private readonly ITransporterService _transporterService;
        private readonly IBidWinnerService _bidWinnerService;
        private readonly IUnitOfWork _unitofwork;
        private readonly ITransporterAgreementVersionService _transporterAgreementVersionService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly IUserAccountService _userAccountService;

        public BidWinnerController(IBidService bidService, ITransporterService transporterService, IBidWinnerService bidWinnerService,
            IUnitOfWork unitofwork, ITransporterAgreementVersionService transporterAgreementVersionService, IWorkflowStatusService workflowStatusService, 
            IUserAccountService userAccountService)
        {
            _bidService = bidService;
            //_applicationSettingService = applicationSettingService;
            //_bidQuotationService = bidQuotationService;
            this._bidWinnerService = bidWinnerService;
            this._unitofwork = unitofwork;
            this._transporterAgreementVersionService = transporterAgreementVersionService;
            _transporterService = transporterService;
            _workflowStatusService = workflowStatusService;
            _userAccountService = userAccountService;
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Bid_Read([DataSourceRequest] DataSourceRequest request)
        {

            var bid = _bidWinnerService.GetBidsWithWinner().OrderByDescending(m => m.BidID);
            var winnerToDisplay = GetBids(bid).ToList();
            return Json(winnerToDisplay.ToDataSourceResult(request));
        }
        private IEnumerable<BidWithWinnerViewModel> GetBids(IEnumerable<Bid> bids)
        {
             var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from bid in bids
                    select new BidWithWinnerViewModel()
                        {
                            BidID = bid.BidID,
                            BidNumber = bid.BidNumber,
                            Year = bid.OpeningDate.Year,
                            OpeningDate = bid.OpeningDate.ToCTSPreferedDateFormat(datePref)
                            
                        });
        }

        public ActionResult Details(int id)
        {
            var bidWinners = _bidWinnerService.FindBy(m => m.BidID == id);
            ViewBag.BidNumber = bidWinners.First().Bid.BidNumber;
            if (bidWinners == null)
            {
                return HttpNotFound();
            }
            var bidWinnersViewModel = new WinnersByBidViewModel
                {
                    BidID = id,
                    BidWinners = GetBidWinner(bidWinners)
                };

            return View(bidWinnersViewModel);
        }
        
        public ActionResult BidWinningTransporters_read([DataSourceRequest] DataSourceRequest request)
        {
            var winningTransprters = _bidWinnerService.Get(t => t.Position == 1 && t.Status == 1).Select(t => t.Transporter).Distinct();
            var winningTransprterViewModels = TransporterListViewModelBinder(winningTransprters.ToList());
            return Json(winningTransprterViewModels.ToDataSourceResult(request));
        }
        public ActionResult BidWinner_Read([DataSourceRequest] DataSourceRequest request,int id=0)
        {

            var bidWinner = _bidWinnerService.FindBy(m=>m.BidID==id).OrderByDescending(m => m.BidWinnerID);
            var winnerToDisplay = GetBidWinner(bidWinner).ToList();
            return Json(winnerToDisplay.ToDataSourceResult(request));
        }

        private  IEnumerable<BidWinnerViewModel> GetBidWinner(IEnumerable<BidWinner> bidWinners)
        {
            return (from bidWinner in bidWinners
                    select new BidWinnerViewModel()
                        {
                            BidWinnnerID = bidWinner.BidWinnerID,
                            TransporterID = bidWinner.TransporterID,
                            TransporterName = bidWinner.Transporter.Name,
                            SourceWarehouse = bidWinner.Hub.Name,
                            Woreda = bidWinner.AdminUnit.Name,
                            WinnerTariff = bidWinner.Tariff,
                            Quantity = bidWinner.Amount,
                            StatusID = bidWinner.Status,
                            Status =_workflowStatusService.GetStatusName(WORKFLOW.BidWinner,bidWinner.Status)

                        });
        }
     
        public ActionResult Edit(int id)
        {
            var bidWinner = _bidWinnerService.FindById(id);
            if (bidWinner==null)
            {
                return HttpNotFound();
            }
            ViewBag.Status = new SelectList(_workflowStatusService.GetStatus(WORKFLOW.BidWinner),"WorkflowID","Description");
            return View(bidWinner);
        }
        [HttpPost]
        public ActionResult Edit(BidWinner bidWinner)
        {
            if (ModelState.IsValid)
            {
                _bidWinnerService.EditBidWinner(bidWinner);
                return RedirectToAction("Index");
            }
            return View(bidWinner);
        }

        public ActionResult SignedContract(int id)
        {
            var bidWinner = _bidWinnerService.FindById(id);
            if(bidWinner!=null)
            {
                _bidWinnerService.SignContract(bidWinner);
                return RedirectToAction("Details", "BidWinner", new {id = bidWinner.BidID});
            }
            ModelState.AddModelError("Errors","Unable to change status");
            return RedirectToAction("Index");
        }

        public ActionResult DisqualifiedWinner(int id)
        {
            var bidWinner = _bidWinnerService.FindById(id);
            if (bidWinner != null)
            {
                _bidWinnerService.Disqualified(bidWinner);
                return RedirectToAction("Details", "BidWinner", new { id = bidWinner.BidID });
            }
            ModelState.AddModelError("Errors","Unable to change Status");
            return RedirectToAction("Index");
        }

        public List<TransporterViewModel> TransporterListViewModelBinder(List<Transporter> transporters)
        {
            return transporters.Select(transporter =>
                {
                    var firstOrDefault = _bidWinnerService.Get(t => t.TransporterID == transporter.TransporterID && t.Status == 1, null, "Bid").FirstOrDefault();
                    return firstOrDefault != null ? new TransporterViewModel
                                                              {
                                                                  TransporterID = transporter.TransporterID,
                                                                  TransporterName = transporter.Name,
                                                                  BidContract = firstOrDefault.Bid.BidNumber
                                                              } : null;
                }).ToList();
        }

        public ActionResult BidWinningTransporters()
        {
            return View();
        }

        public ActionResult ShowAllAgreements(int transporterID)
        {
            ViewBag.TransporterID = transporterID;
            ViewBag.TransporterName = _transporterService.FindById(transporterID).Name;
            return View();
        }

        public ActionResult ShowAllAgreements_read([DataSourceRequest] DataSourceRequest request, int transporterID)
        {
            var allTransprterAgreements = _transporterAgreementVersionService.Get(t => t.TransporterID == transporterID).OrderByDescending(t => t.IssueDate).ToList();
            var agreementVersionViewModels = AgreementVersionListViewModelBinder(allTransprterAgreements);
            return Json(agreementVersionViewModels.ToDataSourceResult(request));
        }

        public List<AgreementVersionViewModel> AgreementVersionListViewModelBinder(List<TransporterAgreementVersion> transporterAgreementVersions)
        {
            
            return transporterAgreementVersions.Select(transporterAgreementVersion => new AgreementVersionViewModel()
            {
                TransporterID = transporterAgreementVersion.TransporterID,
                Transporter = _transporterService.FindById(transporterAgreementVersion.TransporterID).Name,
                BidNo = _bidService.FindById(transporterAgreementVersion.BidID).BidNumber,
                BidID = transporterAgreementVersion.BidID,
                IssueDate = transporterAgreementVersion.IssueDate.ToString(),
                TransportAgreementVersionID = transporterAgreementVersion.TransporterAgreementVersionID
            }).ToList();
        }


        //public ActionResult ShowLetterTemplates([DataSourceRequest] DataSourceRequest request)
        //{
        //    //return Json(_letterTemplateService.GetAllLetterTemplates().ToDataSourceResult(request));

        //}

        public void GenerateAgreementTemplate(int transporterID)
        {
            // TODO: Make sure to use DI to get the template generator instance

            var template = new TemplateHelper(_unitofwork);
            var filePath = template.GenerateTemplate(transporterID, 7, "FrameworkPucrhaseContract"); //here you have to send the name of the tempalte and the id of the TransporterID

            var bidID = new int();
            var firstOrDefault = _bidWinnerService.Get(t => t.TransporterID == transporterID && t.Status == 1).FirstOrDefault();
            if (firstOrDefault != null)
            {
                bidID = firstOrDefault.BidID;
            }
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var fileLen = Convert.ToInt32(fs.Length);
            var data = new byte[fileLen];
            fs.Read(data, 0, fileLen);
            fs.Close();

            var transporterAgreementVersion = new TransporterAgreementVersion
                {BidID = bidID, TransporterID = transporterID, AgreementDocxFile = data, IssueDate = DateTime.Now};
            _transporterAgreementVersionService.AddTransporterAgreementVersion(transporterAgreementVersion);

            Response.Clear();
            Response.ContentType = "application/text";
            Response.AddHeader("Content-Disposition", @"filename= FrameworkPucrhaseContract.docx");
            Response.TransmitFile(filePath);
            Response.End();
        }

        public void ViewAgreementTemplate(int transportAgreementVersionID)
        {
            // TODO: Make sure to use DI to get the template generator instance

            var transportAgreementVersionObj = _transporterAgreementVersionService.FindById(transportAgreementVersionID);
            //var filePath = template.GenerateTemplate(transporterID, 7, "FrameworkPucrhaseContract"); //here you have to send the name of the tempalte and the id of the TransporterID

            var data = (byte[])transportAgreementVersionObj.AgreementDocxFile;
            var guid = new Guid();
            var documentPath =
                System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Templates/{0}.docx", Guid.NewGuid().ToString()));
            using (var stream = new FileStream(documentPath, FileMode.Create))
            {
                stream.Write(data, 0, data.Length);
            };

            Response.Clear();
            Response.ContentType = "application/text";
            Response.AddHeader("Content-Disposition", @"filename= FrameworkPucrhaseContract.docx");
            Response.TransmitFile(documentPath);
            Response.End();
        }

    }
}
