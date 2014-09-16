using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using Cats.Models;
using Cats.Data.Repository;
using System.Text;
using log4net;




namespace Cats.Data.UnitWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatsContext _context;

        // TODO: Add private properties to for each repository

        private IGenericRepository<Bid> bidRepository;
        private IGenericRepository<BidDetail> bidDetailRepository;
        private IGenericRepository<Status> statusRepository;
        private IGenericRepository<BidWinner> bidWinnerRepository;
        private IGenericRepository<TransportOrderDetail> transportOrderDetailRepository;
        private IGenericRepository<RationDetail> rationDetailRepository;
        private IGenericRepository<HRD> hrdRepository;
        private IGenericRepository<HRDDetail> hrdDetailRepository;
        private IGenericRepository<RegionalPSNPPlan> regionalPSNPPlanRepository;
        private IGenericRepository<RegionalPSNPPlanDetail> regionalPSNPPlanDetailRepository;
        private IGenericRepository<Season> seasonRepository;
        private IGenericRepository<Contribution> contributionRepository;
        private IGenericRepository<ContributionDetail> contibutionDetailRepository;
        private IGenericRepository<Currency> currencyRepository;
        private IGenericRepository<InKindContributionDetail> inKindContributionDetailRepository;
        private IGenericRepository<Store> storeRepository;
        private readonly ILog _log;

        public Database Database { get { return _context.Database; } }

        public UnitOfWork()
        {
            this._context = new CatsContext();
        }

        public UnitOfWork(ILog log)
        {
            this._context = new CatsContext();
            _log = log;
        }

        private IGenericRepository<TransporterChequeDetail> _TransporterChequeDetailRepository;
        public IGenericRepository<TransporterChequeDetail> TransporterChequeDetailRepository
        {
            get
            {
                return this._TransporterChequeDetailRepository ??
                       (this._TransporterChequeDetailRepository = new GenericRepository<TransporterChequeDetail>(_context));
            }
        }

        private IGenericRepository<TransporterPaymentRequest> _TransporterPaymentRequestRepository;
        public IGenericRepository<TransporterPaymentRequest> TransporterPaymentRequestRepository
        {
            get
            {
                return this._TransporterPaymentRequestRepository ??
                       (this._TransporterPaymentRequestRepository = new GenericRepository<TransporterPaymentRequest>(_context));
            }
        }

        private IGenericRepository<TransporterCheque> _TransporterChequeRepository; 
        public IGenericRepository<TransporterCheque> TransporterChequeRepository
        {
            get
            {
                return this._TransporterChequeRepository ??
                       (this._TransporterChequeRepository = new GenericRepository<TransporterCheque>(_context));
            }
        }

        private IGenericRepository<Dispatch> _DispatchRepository = null;
        public IGenericRepository<Dispatch> DispatchRepository
        {
            get { return this._DispatchRepository ?? (this._DispatchRepository = new GenericRepository<Dispatch>(_context)); }
        }

        private IGenericRepository<DeliveryReconcile> _DeliveryReconcile = null;
        public IGenericRepository<DeliveryReconcile> DeliveryReconcileRepository
        {
            get { return this._DeliveryReconcile ?? (this._DeliveryReconcile = new GenericRepository<DeliveryReconcile>(_context)); }
        }

        private IGenericRepository<DispatchDetail> _DispatchDetailRepository = null;
        public IGenericRepository<DispatchDetail> DispatchDetailRepository
        {
            get { return this._DispatchDetailRepository ?? (this._DispatchDetailRepository = new GenericRepository<DispatchDetail>(_context)); }
        }
        private IGenericRepository<OtherDispatchAllocation> _OtherDispatchAllocationRepository = null;
        public IGenericRepository<OtherDispatchAllocation> OtherDispatchAllocationRepository
        {
            get { return this._OtherDispatchAllocationRepository ?? (this._OtherDispatchAllocationRepository = new GenericRepository<OtherDispatchAllocation>(_context)); }
        }


        private IGenericRepository<TransporterAgreementVersion> _TransporterAgreementVersionRepository = null;
        public IGenericRepository<TransporterAgreementVersion> TransporterAgreementVersionRepository
        {
            get { return this._TransporterAgreementVersionRepository ?? (this._TransporterAgreementVersionRepository = new GenericRepository<TransporterAgreementVersion>(_context)); }
        }

        private IGenericRepository<HubOwner> _HubOwnerRepository = null;
        public IGenericRepository<HubOwner> HubOwnerRepository
        {
            get { return this._HubOwnerRepository ?? (this._HubOwnerRepository = new GenericRepository<HubOwner>(_context)); }
        }

        private IGenericRepository<PaymentRequest> _PaymentRequestRepository = null;
        public IGenericRepository<PaymentRequest> PaymentRequestRepository
        {
            get { return this._PaymentRequestRepository ?? (this._PaymentRequestRepository = new GenericRepository<PaymentRequest>(_context)); }
        }
        
        private IGenericRepository<WoredaHubLink> _WoredaHubLinkRepository = null;
        public IGenericRepository<WoredaHubLink> WoredaHubLinkRepository
        {
            get { return this._WoredaHubLinkRepository ?? (this._WoredaHubLinkRepository = new GenericRepository<WoredaHubLink>(_context)); }
        }

        private IGenericRepository<WoredaHub> _WoredaHubRepository = null;
        public IGenericRepository<WoredaHub> WoredaHubRepository
        {
            get { return this._WoredaHubRepository ?? (this._WoredaHubRepository = new GenericRepository<WoredaHub>(_context)); }
        }

        private IGenericRepository<DashboardWidget> _dashboardWidgetRepository;
        public IGenericRepository<DashboardWidget> DashboardWidgetRepository
        {
            get { return this._dashboardWidgetRepository ?? (this._dashboardWidgetRepository = new GenericRepository<DashboardWidget>(_context)); }
        }

        private IGenericRepository<UserDashboardPreference> _userDashboardPreferenceRepository;
        public IGenericRepository<UserDashboardPreference> UserDashboardPreferenceRepository
        {
            get { return this._userDashboardPreferenceRepository ?? (this._userDashboardPreferenceRepository = new GenericRepository<UserDashboardPreference>(_context)); }
        }

        private IGenericRepository<Log> _logRepository;
        public IGenericRepository<Log> LogRepository
        {
            get { return this._logRepository ?? (this._logRepository = new GenericRepository<Log>(_context)); }
        }

        private IGenericRepository<Donor> _donorRepository;
        public IGenericRepository<Donor> DonorRepository
        {

            get { return this._donorRepository ?? (this._donorRepository = new GenericRepository<Donor>(_context)); }
        }


        private IGenericRepository<UserHub> _userHubRepository;
        public IGenericRepository<UserHub> UserHubRepository
        {

            get { return this._userHubRepository ?? (this._userHubRepository = new GenericRepository<UserHub>(_context)); }
        }

        private IGenericRepository<RegionalPSNPPledge> _regionalPSNPPledgeRepository;
        public IGenericRepository<RegionalPSNPPledge> RegionalPSNPPledgeRepository
        {
            get { return this._regionalPSNPPledgeRepository ?? (this._regionalPSNPPledgeRepository = new GenericRepository<RegionalPSNPPledge>(_context)); }
        }

        private IGenericRepository<BusinessProcessState> _BusinessProcessStateRepository;
        public IGenericRepository<BusinessProcessState> BusinessProcessStateRepository
        {
            get { return this._BusinessProcessStateRepository ?? (this._BusinessProcessStateRepository = new GenericRepository<BusinessProcessState>(_context)); }
        }

        private IGenericRepository<BusinessProcess> _BusinessProcessRepository;
        public IGenericRepository<BusinessProcess> BusinessProcessRepository
        {
            get { return this._BusinessProcessRepository ?? (this._BusinessProcessRepository = new GenericRepository<BusinessProcess>(_context)); }
        }

        private IGenericRepository<ProcessTemplate> _ProcessTemplateRepository;
        public IGenericRepository<ProcessTemplate> ProcessTemplateRepository
        {
            get { return this._ProcessTemplateRepository ?? (this._ProcessTemplateRepository = new GenericRepository<ProcessTemplate>(_context)); }
        }

        private IGenericRepository<StateTemplate> _StateTemplateRepository;
        public IGenericRepository<StateTemplate> StateTemplateRepository
        {
            get { return this._StateTemplateRepository ?? (this._StateTemplateRepository = new GenericRepository<StateTemplate>(_context)); }
        }
        private IGenericRepository<FlowTemplate> _FlowTemplateRepository;
        public IGenericRepository<FlowTemplate> FlowTemplateRepository
        {
            get { return this._FlowTemplateRepository ?? (this._FlowTemplateRepository = new GenericRepository<FlowTemplate>(_context)); }
        }

        // TODO: Consider adding separate properties for each repositories.

        /// <summary>
        /// ReliefRequistionRepository
        /// </summary>
        /// 
        private IGenericRepository<ReceiptAllocation> receiptAllocationReository;
        public IGenericRepository<ReceiptAllocation> ReceiptAllocationReository
        {
            get { return this.receiptAllocationReository ?? (this.receiptAllocationReository = new GenericRepository<ReceiptAllocation>(_context)); }

        }
        private IGenericRepository<Transaction> transactionReository;
        public IGenericRepository<Transaction> TransactionRepository
        {
            get { return this.transactionReository ?? (this.transactionReository = new GenericRepository<Transaction>(_context)); }

        }
        private IGenericRepository<ShippingInstruction> shippingInstructionReository;
        public IGenericRepository<ShippingInstruction> ShippingInstructionRepository
        {
            get { return this.shippingInstructionReository ?? (this.shippingInstructionReository = new GenericRepository<ShippingInstruction>(_context)); }

        }
        private IGenericRepository<ProjectCode> projectCodeRepository;
        public IGenericRepository<ProjectCode> ProjectCodeRepository
        {
            get { return this.projectCodeRepository ?? (this.projectCodeRepository = new GenericRepository<ProjectCode>(_context)); }

        }

        private IGenericRepository<ProjectCodeAllocation> projectCodeAllocationRepository;
        public IGenericRepository<ProjectCodeAllocation> ProjectCodeAllocationRepository
        {
            get { return this.projectCodeAllocationRepository ?? (this.projectCodeAllocationRepository = new GenericRepository<ProjectCodeAllocation>(_context)); }

        }


        private IGenericRepository<HubAllocation> hubAllocationRepository;
        public IGenericRepository<HubAllocation> HubAllocationRepository
        {
            get { return this.hubAllocationRepository ?? (this.hubAllocationRepository = new GenericRepository<HubAllocation>(_context)); }

        }
        private IGenericRepository<DispatchAllocation> dispatchAllocationRepository;
        public IGenericRepository<DispatchAllocation> DispatchAllocationRepository
        {
            get { return this.dispatchAllocationRepository ?? (this.dispatchAllocationRepository = new GenericRepository<DispatchAllocation>(_context)); }
        }


        private IGenericRepository<RegionalRequest> regionalRequestRepository;
        public IGenericRepository<RegionalRequest> RegionalRequestRepository
        {
            get { return this.regionalRequestRepository ?? (this.regionalRequestRepository = new GenericRepository<RegionalRequest>(_context)); }
        }



        private IGenericRepository<RegionalRequestDetail> regionalRequestDetailRepository;
        public IGenericRepository<RegionalRequestDetail> RegionalRequestDetailRepository
        {
            get { return this.regionalRequestDetailRepository ?? (this.regionalRequestDetailRepository = new GenericRepository<RegionalRequestDetail>(_context)); }
        }


        public IGenericRepository<Bid> BidRepository
        {
            get { return this.bidRepository ?? (this.bidRepository = new GenericRepository<Bid>(_context)); }
        }



        public IGenericRepository<BidDetail> BidDetailRepository
        {
            get { return this.bidDetailRepository ?? (this.bidDetailRepository = new GenericRepository<BidDetail>(_context)); }
        }

        public IGenericRepository<Status> StatusRepository
        {
            get { return this.statusRepository ?? (this.statusRepository = new GenericRepository<Status>(_context)); }
        }
        
        private IGenericRepository<AdminUnit> adminUnitRepository;

        public IGenericRepository<AdminUnit> AdminUnitRepository
        {

            get { return this.adminUnitRepository ?? (this.adminUnitRepository = new GenericRepository<AdminUnit>(_context)); }

        }
        
        private IGenericRepository<AdminUnitType> adminUnitTypeRepository;

        public IGenericRepository<AdminUnitType> AdminUnitTypeRepository
        {

            get { return this.adminUnitTypeRepository ?? (this.adminUnitTypeRepository = new GenericRepository<AdminUnitType>(_context)); }

        }

                
        private IGenericRepository<Commodity> commodityRepository;

        public IGenericRepository<Commodity> CommodityRepository
        {

            get { return this.commodityRepository ?? (this.commodityRepository = new GenericRepository<Commodity>(_context)); }

        }




        private IGenericRepository<CommodityType> commodityTypeRepository;

        public IGenericRepository<CommodityType> CommodityTypeRepository
        {

            get { return this.commodityTypeRepository ?? (this.commodityTypeRepository = new GenericRepository<CommodityType>(_context)); }

        }




        private IGenericRepository<FDP> fdpRepository;
        private IGenericRepository<Contact> contactRepository;

        public IGenericRepository<FDP> FDPRepository
        {

            get { return this.fdpRepository ?? (this.fdpRepository = new GenericRepository<FDP>(_context)); }

        }

        public IGenericRepository<Contact> ContactRepository
        {

            get { return this.contactRepository ?? (this.contactRepository = new GenericRepository<Contact>(_context)); }

        }

        private IGenericRepository<Program> programRepository;

        public IGenericRepository<Program> ProgramRepository
        {

            get { return this.programRepository ?? (this.programRepository = new GenericRepository<Program>(_context)); }

        }





        //private IGenericRepository<Round> roundRepository;


        //    get { return this.roundRepository ?? (this.roundRepository = new GenericRepository<Round>(_context)); }

        //}

        private IGenericRepository<Hub> hubRepository;
        public IGenericRepository<Hub> HubRepository
        {
            get { return this.hubRepository ?? (this.hubRepository = new GenericRepository<Hub>(_context)); }
        }

       

        private IGenericRepository<IDPSReasonType> iDPSReasonTypeRepository;
        public IGenericRepository<IDPSReasonType> IDPSReasonTypeRepository
        {
            get { return this.iDPSReasonTypeRepository ?? (this.iDPSReasonTypeRepository = new GenericRepository<IDPSReasonType>(_context)); }
        }

       


        private IGenericRepository<ReliefRequisition> reliefRequistionRepository;

        public IGenericRepository<ReliefRequisition> ReliefRequisitionRepository
        {

            get { return this.reliefRequistionRepository ?? (this.reliefRequistionRepository = new GenericRepository<ReliefRequisition>(_context)); }

        }




        private IGenericRepository<ReliefRequisitionDetail> reliefRequisitionRepository;

        public IGenericRepository<ReliefRequisitionDetail> ReliefRequisitionDetailRepository
        {

            get { return this.reliefRequisitionRepository ?? (this.reliefRequisitionRepository = new GenericRepository<ReliefRequisitionDetail>(_context)); }

        }

        private IGenericRepository<Transporter> transporterRepository;

        public IGenericRepository<Transporter> TransporterRepository
        {

            get { return this.transporterRepository ?? (this.transporterRepository = new GenericRepository<Transporter>(_context)); }

        }


        private IGenericRepository<TransportBidPlan> transportBidPlanRepository;

        public IGenericRepository<TransportBidPlan> TransportBidPlanRepository
        {

            get { return this.transportBidPlanRepository ?? (this.transportBidPlanRepository = new GenericRepository<TransportBidPlan>(_context)); }

        }

        private IGenericRepository<TransportBidPlanDetail> transportBidPlanDetailRepository;

        public IGenericRepository<TransportBidPlanDetail> TransportBidPlanDetailRepository
        {

            get { return this.transportBidPlanDetailRepository ?? (this.transportBidPlanDetailRepository = new GenericRepository<TransportBidPlanDetail>(_context)); }

        }

        //     private IGenericRepository<RequisitionViewModel> requisitionViewModelRepository;

        //private IGenericRepository<RequisitionViewModel> requisitionViewModelRepository;


        //public IGenericRepository<RequisitionViewModel> RequisitionViewModelRepository
        //{

        //    get { return this.requisitionViewModelRepository ?? (this.requisitionViewModelRepository = new GenericRepository<RequisitionViewModel>(_context)); }

        //}

        private IGenericRepository<TransportRequisition> transportRequisitionRepository;

        public IGenericRepository<TransportRequisition> TransportRequisitionRepository
        {
            get { return this.transportRequisitionRepository ?? (this.transportRequisitionRepository = new GenericRepository<TransportRequisition>(_context)); }

        }

        private IGenericRepository<TransportBidQuotation> transportBidQuotationRepository;

        public IGenericRepository<TransportBidQuotation> TransportBidQuotationRepository
        {
            get { return this.transportBidQuotationRepository ?? (this.transportBidQuotationRepository = new GenericRepository<TransportBidQuotation>(_context)); }

        }

        private IGenericRepository<TransportBidQuotationHeader> transportBidQuotationHeaderRepository;

        public IGenericRepository<TransportBidQuotationHeader> TransportBidQuotationHeaderRepository
        {

            get
            {
                return this.transportBidQuotationHeaderRepository ??
                       (this.transportBidQuotationHeaderRepository =
                        new GenericRepository<TransportBidQuotationHeader>(_context));
            }

        }

        

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                //var outputLines = new List<string>();
                //foreach (var eve in e.EntityValidationErrors)
                //{
                //    outputLines.Add(string.Format(
                //        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                //        DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                //    outputLines.AddRange(eve.ValidationErrors.Select(ve => string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage)));
                //}
                //// System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                for (var eCurrent = e; eCurrent != null; eCurrent = (DbEntityValidationException)eCurrent.InnerException)
                {
                    foreach (var eve in eCurrent.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);

                        StringBuilder errorMsg = new StringBuilder(String.Empty);
                        var s = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        errorMsg.Append(s);

                        foreach (var ve in eve.ValidationErrors)
                        {
                            errorMsg.Append(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                            _log.Error(errorMsg, eCurrent.GetBaseException());
                        }
                    }
                }
                throw;
            }

        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        private IGenericRepository<TransportOrder> transportOrderRepository;

        public IGenericRepository<TransportOrder> TransportOrderRepository
        {

            get { return this.transportOrderRepository ?? (this.transportOrderRepository = new GenericRepository<TransportOrder>(_context)); }

        }


        private IGenericRepository<UserDashboard> userDashboardRepository;

        public IGenericRepository<UserDashboard> UserDashboardRepository
        {

            get { return this.userDashboardRepository ?? (this.userDashboardRepository = new GenericRepository<UserDashboard>(_context)); }

        }

        public IGenericRepository<TransportOrderDetail> TransportOrderDetailRepository
        {

            get { return this.transportOrderDetailRepository ?? (this.transportOrderDetailRepository = new GenericRepository<TransportOrderDetail>(_context)); }

        }


        public IGenericRepository<BidWinner> BidWinnerRepository
        {

            get { return this.bidWinnerRepository ?? (this.bidWinnerRepository = new GenericRepository<BidWinner>(_context)); }
        }



        private IGenericRepository<vwTransportOrder> vwTransportOrderRepository;

        public IGenericRepository<vwTransportOrder> VwTransportOrderRepository
        {

            get { return this.vwTransportOrderRepository ?? (this.vwTransportOrderRepository = new GenericRepository<vwTransportOrder>(_context)); }

        }








        private IGenericRepository<TransportRequisitionDetail> transportRequisitionDetailRepository;

        public IGenericRepository<TransportRequisitionDetail> TransportRequisitionDetailRepository
        {

            get { return this.transportRequisitionDetailRepository ?? (this.transportRequisitionDetailRepository = new GenericRepository<TransportRequisitionDetail>(_context)); }

        }





        private IGenericRepository<WorkflowStatus> workflowStatusRepository;

        public IGenericRepository<WorkflowStatus> WorkflowStatusRepository
        {

            get { return this.workflowStatusRepository ?? (this.workflowStatusRepository = new GenericRepository<WorkflowStatus>(_context)); }

        }

        private IGenericRepository<ApplicationSetting> applicationSettingRepository;

        public IGenericRepository<ApplicationSetting> ApplicationSettingRepository
        {

            get { return this.applicationSettingRepository ?? (this.applicationSettingRepository = new GenericRepository<ApplicationSetting>(_context)); }

        }


        private IGenericRepository<Ration> rationRepository;

        public IGenericRepository<Ration> RationRepository
        {

            get { return this.rationRepository ?? (this.rationRepository = new GenericRepository<Ration>(_context)); }

        }

        public IGenericRepository<RationDetail> RationDetailRepository
        {

            get { return this.rationDetailRepository ?? (this.rationDetailRepository = new GenericRepository<RationDetail>(_context)); }

        }

        public IGenericRepository<HRD> HRDRepository
        {

            get { return this.hrdRepository ?? (this.hrdRepository = new GenericRepository<HRD>(_context)); }

        }

        public IGenericRepository<HRDDetail> HRDDetailRepository
        {

            get { return this.hrdDetailRepository ?? (this.hrdDetailRepository = new GenericRepository<HRDDetail>(_context)); }

        }
        public IGenericRepository<RegionalPSNPPlan> RegionalPSNPPlanRepository
        {


            get { return this.regionalPSNPPlanRepository ?? (this.regionalPSNPPlanRepository = new GenericRepository<RegionalPSNPPlan>(_context)); }

        }




        private IGenericRepository<RequestDetailCommodity> requestDetailCommodityRepository;

        public IGenericRepository<RequestDetailCommodity> RequestDetailCommodityRepository
        {

            get { return this.requestDetailCommodityRepository ?? (this.requestDetailCommodityRepository = new GenericRepository<RequestDetailCommodity>(_context)); }

        }





        //  IGenericRepository<ApplicationSetting> ApplicationSettingRepository


        public IGenericRepository<RegionalPSNPPlanDetail> RegionalPSNPPlanDetailRepository
        {

            get { return this.regionalPSNPPlanDetailRepository ?? (this.regionalPSNPPlanDetailRepository = new GenericRepository<RegionalPSNPPlanDetail>(_context)); }

        }
        //  IGenericRepository<RegionalPSNPPlanDetail> RegionalPSNPPlanDetailRepository { get; } 


        private IGenericRepository<NeedAssessment> needAssessmentRepository;
        public IGenericRepository<NeedAssessment> NeedAssessmentRepository
        {
            get
            {
                return this.needAssessmentRepository ?? (this.needAssessmentRepository = new GenericRepository<NeedAssessment>(_context));
            }
        }


        private IGenericRepository<NeedAssessmentHeader> needAssessmentHeaderRepository;
        public IGenericRepository<NeedAssessmentHeader> NeedAssessmentHeaderRepository
        {
            get
            {
                return this.needAssessmentHeaderRepository ?? (this.needAssessmentHeaderRepository = new GenericRepository<NeedAssessmentHeader>(_context));
            }
        }

        private IGenericRepository<NeedAssessmentDetail> needAssessmentDetailRepository;
        public IGenericRepository<NeedAssessmentDetail> NeedAssessmentDetailRepository
        {
            get
            {
                return this.needAssessmentDetailRepository ?? (this.needAssessmentDetailRepository = new GenericRepository<NeedAssessmentDetail>(_context));
            }
        }


        private IGenericRepository<vwPSNPAnnualPlan> vwPSNPAnnualPlanRepository;


        private IGenericRepository<UserProfile> userProfileRepository;
        public IGenericRepository<UserProfile> UserProfileRepository
        {
            get
            {
                return this.userProfileRepository ?? (this.userProfileRepository = new GenericRepository<UserProfile>(_context));
            }
        }



        private IGenericRepository<GiftCertificate> giftCertificateRepository;
        public IGenericRepository<GiftCertificate> GiftCertificateRepository
        {
            get
            {
                return this.giftCertificateRepository ?? (this.giftCertificateRepository = new GenericRepository<GiftCertificate>(_context));
            }
        }

        private IGenericRepository<GiftCertificateDetail> giftCertificateDetailRepository;
        public IGenericRepository<GiftCertificateDetail> GiftCertificateDetailRepository
        {
            get
            {
                return this.giftCertificateDetailRepository ?? (this.giftCertificateDetailRepository = new GenericRepository<GiftCertificateDetail>(_context));
            }
        }

        private IGenericRepository<LetterTemplate> letterTemplateRepository;
        public IGenericRepository<LetterTemplate> LetterTemplateRepository
        {
            get
            {
                return this.letterTemplateRepository ?? (this.letterTemplateRepository = new GenericRepository<LetterTemplate>(_context));
            }
        }
        public IGenericRepository<vwPSNPAnnualPlan> VwPSNPAnnualPlanRepository
        {

            get { return this.vwPSNPAnnualPlanRepository ?? (this.vwPSNPAnnualPlanRepository = new GenericRepository<vwPSNPAnnualPlan>(_context)); }

        }

        private IGenericRepository<Unit> unitRepository;

        public IGenericRepository<Unit> UnitRepository
        {

            get { return this.unitRepository ?? (this.unitRepository = new GenericRepository<Unit>(_context)); }

        }


        public IGenericRepository<Season> SeasonRepository
        {

            get { return this.seasonRepository ?? (this.seasonRepository = new GenericRepository<Season>(_context)); }

        }

        public IGenericRepository<Contribution> ContributionRepository
        {
            get
            {
                return this.contributionRepository ?? (this.contributionRepository = new GenericRepository<Contribution>(_context));
            }
        }

        public IGenericRepository<ContributionDetail> ContributionDetailRepository
        {
            get
            {
                return this.contibutionDetailRepository ??
                       (this.contibutionDetailRepository = new GenericRepository<ContributionDetail>(_context));
            }
        }

        private IGenericRepository<Detail> detailRepository;
        public IGenericRepository<Detail> DetailRepository
        {

            get { return this.detailRepository ?? (this.detailRepository = new GenericRepository<Detail>(_context)); }

        }

        private IGenericRepository<TypeOfNeedAssessment> typeOfNeedAssessmentRepository;
        public IGenericRepository<TypeOfNeedAssessment> TypeOfNeedAssessmentRepository
        {

            get { return this.typeOfNeedAssessmentRepository ?? (this.typeOfNeedAssessmentRepository = new GenericRepository<TypeOfNeedAssessment>(_context)); }

        }


        //Need Assesssment Summary (for Dashboard) Repository
        private IGenericRepository<NeedAssessmentSummary> needAssessmentSummaryRepository;
        public IGenericRepository<NeedAssessmentSummary> NeedAssessmetSummaryRepository
        {
            get { return this.needAssessmentSummaryRepository ?? (this.needAssessmentSummaryRepository = new GenericRepository<NeedAssessmentSummary>(_context)); }
        }

        public IGenericRepository<Currency> CurrencyRepository
        {
            get { return this.currencyRepository ?? (this.currencyRepository = new GenericRepository<Currency>(_context)); }
        }
        private IGenericRepository<TransactionGroup> transactionGroupRepository;
        public IGenericRepository<TransactionGroup> TransactionGroupRepository
        {
            get { return this.transactionGroupRepository ?? (this.transactionGroupRepository = new GenericRepository<TransactionGroup>(_context)); }
        }

        public IGenericRepository<InKindContributionDetail> InKindContributionDetailRepository
        {
            get { return this.inKindContributionDetailRepository ?? (this.inKindContributionDetailRepository = new GenericRepository<InKindContributionDetail>(_context)); }
        }
        public IGenericRepository<Store> StoreRepository
        {
            get { return this.storeRepository ?? (this.storeRepository = new GenericRepository<Store>(_context)); }
        }

        private IGenericRepository<CommodityGrade> commodityGradeRepository;
        public IGenericRepository<CommodityGrade> CommodityGradeRepository
        {
            get { return this.commodityGradeRepository ?? (this.commodityGradeRepository = new GenericRepository<CommodityGrade>(_context)); }
        }
        private IGenericRepository<CommoditySource> commoditySourceRepository;
        public IGenericRepository<CommoditySource> CommoditySourceRepository
        {
            get { return this.commoditySourceRepository ?? (this.commoditySourceRepository = new GenericRepository<CommoditySource>(_context)); }
        }

        private IGenericRepository<Audit> auditRepository;
        public IGenericRepository<Audit> AuditRepository
        {
            get { return this.auditRepository ?? (this.auditRepository = new GenericRepository<Audit>(_context)); }
        }

        private IGenericRepository<TransReqWithoutTransporter> transReqWithoutTransporterRepository;
        public IGenericRepository<TransReqWithoutTransporter> TransReqWithoutTransporterRepository
        {
            get { return this.transReqWithoutTransporterRepository ?? (this.transReqWithoutTransporterRepository = new GenericRepository<TransReqWithoutTransporter>(_context)); }
        }

        private IGenericRepository<AllocationByRegion> allocationByRegionRepository;
        public IGenericRepository<AllocationByRegion> AllocationByRegionRepository
        {
            get { return this.allocationByRegionRepository ?? (this.allocationByRegionRepository = new GenericRepository<AllocationByRegion>(_context)); }
        }

        private IGenericRepository<Plan>planRepository;
        public IGenericRepository<Plan> PlanRepository
         {
           get { return this.planRepository ?? (this.planRepository = new GenericRepository<Plan>(_context)); }
         }
       
        private IGenericRepository<PromisedContribution> _PromisedContributionRepository = null;
        public IGenericRepository<PromisedContribution> PromisedContributionRepository
        {
            get { return this._PromisedContributionRepository ?? (this._PromisedContributionRepository = new GenericRepository<PromisedContribution>(_context)); }
        }

        private IGenericRepository<SIPCAllocation> _SIPCAllocationRepository = null;
        public IGenericRepository<SIPCAllocation> SIPCAllocationRepository
        {
            get { return this._SIPCAllocationRepository ?? (this._SIPCAllocationRepository = new GenericRepository<SIPCAllocation>(_context)); }
        }

        private IGenericRepository<Notification> notificationRepository = null;
        public IGenericRepository<Notification> NotificationRepository
        {
            get { return this.notificationRepository ?? (this.notificationRepository = new GenericRepository<Notification>(_context)); }
        }

        private IGenericRepository<HrdDonorCoverage> hrdDonorCoverageRepository = null;
        public IGenericRepository<HrdDonorCoverage> HrdDonorCoverageRepository
        {
            get { return this.hrdDonorCoverageRepository ?? (this.hrdDonorCoverageRepository = new GenericRepository<HrdDonorCoverage>(_context)); }
        }
        private IGenericRepository<HrdDonorCoverageDetail> hrdDonorCoverageDetailRepository = null;
        public IGenericRepository<HrdDonorCoverageDetail> HrdDonorCoverageDetailRepository
        {
            get { return this.hrdDonorCoverageDetailRepository ?? (this.hrdDonorCoverageDetailRepository = new GenericRepository<HrdDonorCoverageDetail>(_context)); }
        }

        private IGenericRepository<Delivery> deliveryRepositiory;
        public IGenericRepository<Delivery> DeliveryRepository
        {
            get { return this.deliveryRepositiory ?? (this.deliveryRepositiory = new GenericRepository<Delivery>(_context)); }
      
        }
        private IGenericRepository<DeliveryDetail> deliveryDetailRepository;
        public IGenericRepository<DeliveryDetail> DeliveryDetailRepository
        {
            get { return this.deliveryDetailRepository ?? (this.deliveryDetailRepository = new GenericRepository<DeliveryDetail>(_context)); }
      
        }

        private IGenericRepository<ActionTypes> actionTypesRepository;
        public IGenericRepository<ActionTypes> ActionTypesRepository
        {
            get { return this.actionTypesRepository ?? (this.actionTypesRepository = new GenericRepository<ActionTypes>(_context)); }

        }

     
        



        private IGenericRepository<TemplateType> templateTypeRepository;

        public IGenericRepository<TemplateType> TemplateTypeRepository
        {
            get { return this.templateTypeRepository ?? (this.templateTypeRepository = new GenericRepository<TemplateType>(_context)); }

        }

        private IGenericRepository<Template> templateRepository;
        public IGenericRepository<Template> TemplateRepository
        {
            get { return this.templateRepository ?? (this.templateRepository = new GenericRepository<Template>(_context)); }

        }

        private IGenericRepository<TemplateField> templateFieldRepository;
        public IGenericRepository<TemplateField> TemplateFieldRepository
        {
            get { return this.templateFieldRepository ?? (this.templateFieldRepository = new GenericRepository<TemplateField>(_context)); }

        }

        private IGenericRepository<WoredaStockDistribution> utilizationHeaderRepository;
        public IGenericRepository<WoredaStockDistribution> WoredaStockDistributionRepository
        {
            get { return this.utilizationHeaderRepository ?? (this.utilizationHeaderRepository = new GenericRepository<WoredaStockDistribution>(_context)); }

        }

        private IGenericRepository<WoredaStockDistributionDetail> utilizationDetailRepository;
        public IGenericRepository<WoredaStockDistributionDetail> WoredaStockDistributionDetailRepository
        {
            get { return this.utilizationDetailRepository ?? (this.utilizationDetailRepository = new GenericRepository<WoredaStockDistributionDetail>(_context)); }

        }

        private IGenericRepository<DistributionByAgeDetail> _distributionByAgeDetailRepository;
        public IGenericRepository<DistributionByAgeDetail> DistributionByAgeDetailRepository
        {
            get { return this._distributionByAgeDetailRepository ?? (this._distributionByAgeDetailRepository = new GenericRepository<DistributionByAgeDetail>(_context)); }

        }



        private IGenericRepository<ReceiptAllocation> _receiptAllocationRepository;

        public IGenericRepository<ReceiptAllocation> ReceiptAllocationRepository
        {
            get
            {
                return this._receiptAllocationRepository ??
                       (this._receiptAllocationRepository = new GenericRepository<ReceiptAllocation>(_context));
            }

        }

        private IGenericRepository<ReceiptPlan> _receiptPlanRepository;
        
        public IGenericRepository<ReceiptPlan> ReceiptPlanRepository
        {
            get
            {
                return this._receiptPlanRepository ??
                       (this._receiptPlanRepository = new GenericRepository<ReceiptPlan>(_context));
            }

        }

        private IGenericRepository<ReceiptPlanDetail> _receiptPlanDetailRepository;
        public IGenericRepository<ReceiptPlanDetail> ReceiptPlanDetailRepository
        {
            get
            {
                return this._receiptPlanDetailRepository ??
                       (this._receiptPlanDetailRepository = new GenericRepository<ReceiptPlanDetail>(_context));
            }

        }

        private IGenericRepository<DonationPlanHeader> _donationPlanHeaderRepository; 
        public IGenericRepository<DonationPlanHeader> DonationPlanHeaderRepository
        {
            get
            {
                return this._donationPlanHeaderRepository ?? (this._donationPlanHeaderRepository = new GenericRepository<DonationPlanHeader>(_context));
            }
        }

        private IGenericRepository<DonationPlanDetail> _donationPlanDetailRepository;
        public IGenericRepository<DonationPlanDetail> DonationPlanDetailRepository
        {
            get
            {
                return this._donationPlanDetailRepository ?? (this._donationPlanDetailRepository = new GenericRepository<DonationPlanDetail>(_context));
            }
        }

        private IGenericRepository<SupportType> _supportTypeRepository; 
        public IGenericRepository<SupportType> SupportTypeRepository
        {
            get
            {
                return this._supportTypeRepository ?? (this._supportTypeRepository = new GenericRepository<SupportType>(_context));
            }
        }

        private IGenericRepository<LocalPurchase> _localPurchaseRepositiry;
        public IGenericRepository<LocalPurchase> LocalPurchaseRepository
        {
            get 
            { 
                return this._localPurchaseRepositiry ?? (this._localPurchaseRepositiry = new GenericRepository<LocalPurchase>(_context));
            }
        }

        private IGenericRepository<LocalPurchaseDetail> _localPurchaseDetilRepository;
        public IGenericRepository<LocalPurchaseDetail> LocalPurchaseDetailRepository
        {
            get
            {
                return this._localPurchaseDetilRepository ??
                       (this._localPurchaseDetilRepository = new GenericRepository<LocalPurchaseDetail>(_context));
            }
        }

        private IGenericRepository<LoanReciptPlan> _loanReciptPlanRepository;
        public IGenericRepository<LoanReciptPlan> LoanReciptPlanRepository
        {
            get
            {
                return this._loanReciptPlanRepository ??
                       (this._loanReciptPlanRepository = new GenericRepository<LoanReciptPlan>(_context));
            }
        }

        private IGenericRepository<LoanReciptPlanDetail> _loanReciptPlanDetailRepository;
        public IGenericRepository<LoanReciptPlanDetail> LoanReciptPlanDetailRepository
        {
            get
            {
                return this._loanReciptPlanDetailRepository ??
                       (this._loanReciptPlanDetailRepository = new GenericRepository<LoanReciptPlanDetail>(_context));
            }
        }

        private IGenericRepository<Transfer> _transferRepository;
        public IGenericRepository<Transfer> TransferRepository
        {
            get { return this._transferRepository ?? (this._transferRepository = new GenericRepository<Transfer>(_context)); }
        }

        private IGenericRepository<LossReason> _lossReasonRepository;
        public IGenericRepository<LossReason> LossReasonRepository
        {
            get { return this._lossReasonRepository ?? (this._lossReasonRepository = new GenericRepository<LossReason>(_context)); }
        }

    }
}
