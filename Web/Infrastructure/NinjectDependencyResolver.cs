using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Services.Administration;
using Cats.Services.Hub;
using Cats.Services.Hub.Interfaces;
using Cats.Services.Security;
using LanguageHelpers.Localization.Services;
using Logistics.Security;
using NetSqlAzMan;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.Providers;
using Ninject;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Cats.Services.Logistics;
using Cats.Services.PSNP;
using Cats.Services.Common;
using Cats.Services.Dashboard;

using log4net;
using Cats.Security;
using AdminUnitService = Cats.Services.EarlyWarning.AdminUnitService;
using CommodityService = Cats.Services.EarlyWarning.CommodityService;
using CommonService = Cats.Services.Common.CommonService;
using DonorService = Cats.Services.EarlyWarning.DonorService;
using FDPService = Cats.Services.EarlyWarning.FDPService;
using ForgetPasswordRequestService = Cats.Services.Security.ForgetPasswordRequestService;
using GiftCertificateDetailService = Cats.Services.EarlyWarning.GiftCertificateDetailService;
using GiftCertificateService = Cats.Services.EarlyWarning.GiftCertificateService;
using HubService = Cats.Services.EarlyWarning.HubService;
using IAdminUnitService = Cats.Services.EarlyWarning.IAdminUnitService;
using ICommodityService = Cats.Services.EarlyWarning.ICommodityService;
using ICommonService = Cats.Services.Common.ICommonService;
using IDonorService = Cats.Services.EarlyWarning.IDonorService;
using IFDPService = Cats.Services.EarlyWarning.IFDPService;
using IForgetPasswordRequestService = Cats.Services.Security.IForgetPasswordRequestService;
using IGiftCertificateDetailService = Cats.Services.EarlyWarning.IGiftCertificateDetailService;
using IGiftCertificateService = Cats.Services.EarlyWarning.IGiftCertificateService;
using IHubService = Cats.Services.EarlyWarning.IHubService;
using ILedgerService = Cats.Services.Common.ILedgerService;
using ILetterTemplateService = Cats.Services.Common.ILetterTemplateService;
using IProgramService = Cats.Services.EarlyWarning.IProgramService;
using IProjectCodeService = Cats.Services.EarlyWarning.IProjectCodeService;
using ISettingService = Cats.Services.Security.ISettingService;
using IShippingInstructionService = Cats.Services.EarlyWarning.IShippingInstructionService;
using ITransactionService = Cats.Services.Transaction.ITransactionService;
using ITransporterService = Cats.Services.Procurement.ITransporterService;
using IUnitService = Cats.Services.EarlyWarning.IUnitService;
using LedgerService = Cats.Services.Common.LedgerService;
using LetterTemplateService = Cats.Services.Common.LetterTemplateService;
using ProgramService = Cats.Services.EarlyWarning.ProgramService;
using ProjectCodeService = Cats.Services.EarlyWarning.ProjectCodeService;
using SettingService = Cats.Services.Security.SettingService;
using ShippingInstructionService = Cats.Services.EarlyWarning.ShippingInstructionService;
using TransactionService = Cats.Services.Transaction.TransactionService;
using TransporterService = Cats.Services.Procurement.TransporterService;
using UnitService = Cats.Services.EarlyWarning.UnitService;
using Cats.Localization;
using Cats.Localization.Services;


//using Cats.Services.Hub.Interfaces;
//using Cats.Services.Hub;

namespace Cats.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
            AddBindingsHub();
        }

       
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        
        private void AddBindings()
        {
            kernel.Bind<IPaymentRequestService>().To<PaymentRequestService>();
            kernel.Bind<ISIPCAllocationService>().To<SIPCAllocationService>();
            kernel.Bind<IPromisedContributionService>().To<PromisedContributionService>();
            kernel.Bind<IBusinessProcessStateService>().To<BusinessProcessStateService>();
            kernel.Bind<IBusinessProcessService>().To<BusinessProcessService>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IProcessTemplateService>().To<ProcessTemplateService>();
            kernel.Bind<IStateTemplateService>().To<StateTemplateService>();
            kernel.Bind<IFlowTemplateService>().To<FlowTemplateService>();

            kernel.Bind<IRegionalRequestService>().To<RegionalRequestService>();
            kernel.Bind<IFDPService>().To<FDPService>();
            kernel.Bind<IAdminUnitService>().To<AdminUnitService>();
            kernel.Bind<IProgramService>().To<ProgramService>();
            kernel.Bind<ICommodityService>().To<CommodityService>();
            kernel.Bind<IRegionalRequestDetailService>().To<RegionalRequestDetailService>();
            kernel.Bind<IReliefRequisitionService>().To<ReliefRequisitionService>();
            kernel.Bind<IReliefRequisitionDetailService>().To<ReliefRequisitionDetailService>();
            kernel.Bind<IBidService>().To<BidService>();
          
            kernel.Bind<IHubService>().To<HubService>();
            kernel.Bind<ITransporterService>().To<TransporterService>();
            kernel.Bind<ITransportBidPlanService>().To<TransportBidPlanService>();
            kernel.Bind<ITransportBidPlanDetailService>().To<TransportBidPlanDetailService>();
            kernel.Bind<IBidDetailService>().To<BidDetailService>();
            kernel.Bind<IStatusService>().To<StatusService>();
            kernel.Bind<IHubAllocationService>().To<HubAllocationService>();
           
            // Security service registration
            kernel.Bind<IUserAccountService>().To<UserAccountService>();
            kernel.Bind<Cats.Data.Security.IUnitOfWork>().To<Cats.Data.Security.UnitOfWork>();

            kernel.Bind<ITransportOrderService>().To<TransportOrderService>();
            kernel.Bind<IProjectCodeService>().To<ProjectCodeService>();
            kernel.Bind<IProjectCodeAllocationService>().To<ProjectCodeAllocationService>();
            kernel.Bind<IBidWinnerService>().To<BidWinnerService>();
            kernel.Bind<IShippingInstructionService>().To<ShippingInstructionService>();

            kernel.Bind<ITransactionService>().To<TransactionService>();
            //kernel.Bind<ITransactionService>().To<TransactionService>();
            //kernel.Bind<ITransactionGroupService>().To<TransactionGroupService>();
            kernel.Bind<ITransportRequisitionService>().To<TransportRequisitionService>();

            kernel.Bind<IBeneficiaryAllocationService>().To<BeneficiaryAllocationService>();
            kernel.Bind<IWorkflowStatusService>().To<WorkflowStatusService>();
            kernel.Bind<ITransportBidQuotationService>().To<TransportBidQuotationService>();
            kernel.Bind<ITransportBidQuotationHeaderService>().To<TransportBidQuotationHeaderService>();
            kernel.Bind<IApplicationSettingService>().To<ApplicationSettingService>();
            kernel.Bind<IRationService>().To<RationService>();
            kernel.Bind<IRationDetailService>().To<RationDetailService>();

            kernel.Bind<INeedAssessmentHeaderService>().To<NeedAssessmentHeaderService>();
            kernel.Bind<INeedAssessmentDetailService>().To<NeedAssessmentDetailService>();
            kernel.Bind<INeedAssessmentService>().To<NeedAssessmentService>();

            kernel.Bind<IHRDService>().To<HRDService>();
            kernel.Bind<IHRDDetailService>().To<HRDDetailService>();
            kernel.Bind<IRegionalPSNPPlanService>().To<RegionalPSNPPlanService>();
            kernel.Bind<IRegionalPSNPPlanDetailService>().To<RegionalPSNPPlanDetailService>();

            //kernel.Bind<ILocalizedTextService>().To<LocalizedTextService>();
            //kernel.Bind<ILanguageService>().To<LanguageService>();
            //kernel.Bind<LanguageHelpers.Localization.Data.IUnitOfWork>().To<LanguageHelpers.Localization.Data.UnitOfWork>();

            kernel.Bind<Cats.Localization.Data.UnitOfWork.IUnitOfWork>().To<Cats.Localization.Data.UnitOfWork.UnitOfWork>();
            kernel.Bind<ILocalizationService>().To<LocalizationService>();

            kernel.Bind<IGiftCertificateService>().To<GiftCertificateService>();
            kernel.Bind<IGiftCertificateDetailService>().To<GiftCertificateDetailService>();
            
            kernel.Bind<ISeasonService>().To<SeasonService>();
            kernel.Bind<IDonorService>().To<DonorService>();
            kernel.Bind<ICommonService>().To<CommonService>();
            kernel.Bind<IRegionalPSNPPledgeService>().To<RegionalPSNPPledgeService>();

            kernel.Bind<IContributionService>().To<ContributionService>();
            kernel.Bind<IContributionDetailService>().To<ContributionDetailService>();
            kernel.Bind<IInkindContributionDetailService>().To<InKindContributionDetailService>();

            kernel.Bind<ITypeOfNeedAssessmentService>().To<TypeOfNeedAssessmentService>();

            kernel.Bind<IUnitService>().To<UnitService>();
            kernel.Bind<ILetterTemplateService>().To<LetterTemplateService>();
            //kernel.Bind<ILog>().To<Log>();
            kernel.Bind<ICurrencyService>().To<CurrencyService>();

            kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.DeclaringType));

            kernel.Bind<ILogReadService>().To<LogReadService>();
            kernel.Bind<INotificationService>().To<NotificationService>();
            kernel.Bind<ISMSGatewayService>().To<SMSGatewayService>();
            kernel.Bind<IUserDashboardPreferenceService>().To<UserDashboardPreferenceService>();
            kernel.Bind<IForgetPasswordRequestService>().To<ForgetPasswordRequestService>();
            kernel.Bind<IDashboardWidgetService>().To<DashboardWidgetService>();
            kernel.Bind<ISettingService>().To<SettingService>();
            kernel.Bind<ILedgerService>().To<LedgerService>();
            kernel.Bind<ITransReqWithoutTransporterService>().To<TransReqWithoutTransporterService>();
            kernel.Bind<ITransportOrderDetailService>().To<TransportOrderDetailService>();
            kernel.Bind<IAllocationByRegionService>().To<AllocationByRegionService>();
            kernel.Bind<IPlanService>().To<PlanService>();
            kernel.Bind<IDashboardService>().To<DashboardService>();
            kernel.Bind<IHrdDonorCoverageService>().To<HrdDonorCoverageService>();
            kernel.Bind<IHrdDonorCoverageDetailService>().To<HrdDonorCoverageDetailService>();
            kernel.Bind<IIDPSReasonTypeServices>().To<IDPSReasonTypeServices>();
            kernel.Bind<IActionTypesService>().To<ActionTypesService>();

            kernel.Bind<IWoredaHubLinkService>().To<WoredaHubLinkService>();
            kernel.Bind<IWoredaHubService>().To<WoredaHubService>();
            kernel.Bind<ITransporterAgreementVersionService>().To<TransporterAgreementVersionService>();

            kernel.Bind<ITemplateService>().To<TemplateService>();

            kernel.Bind<IAzManStorage>().To<SqlAzManStorage>().WithConstructorArgument("connectionString",
                                                                                   System.Configuration.
                                                                                       ConfigurationManager.
                                                                                       ConnectionStrings[
                                                                                           "SecurityContext"].
                                                                                       ConnectionString);
            kernel.Bind<NetSqlAzManRoleProvider>().To<NetSqlAzManRoleProvider>();

            kernel.Bind<ILogisticsCheckAccess>().To<LogisticsCheckAccess>().WithConstructorArgument("storageConnectionString",
                                                                                  System.Configuration.
                                                                                      ConfigurationManager.
                                                                                      ConnectionStrings[
                                                                                          "SecurityContext"].
                                                                                      ConnectionString);
            kernel.Bind<IProcurementCheckAccess>().To<ProcurementCheckAccess>().WithConstructorArgument("storageConnectionString",
                                                                                  System.Configuration.
                                                                                      ConfigurationManager.
                                                                                      ConnectionStrings[
                                                                                          "SecurityContext"].
                                                                                      ConnectionString);
            kernel.Bind<IPSNPCheckAccess>().To<PSNPCheckAccess>().WithConstructorArgument("storageConnectionString",
                                                                                  System.Configuration.
                                                                                      ConfigurationManager.
                                                                                      ConnectionStrings[
                                                                                          "SecurityContext"].
                                                                                      ConnectionString);
            kernel.Bind<Cats.Services.Hub.Interfaces.IStockStatusService>().To<Cats.Services.Hub.StockStatusService>();
            kernel.Bind<Cats.Services.Logistics.IDeliveryService>().To<Cats.Services.Logistics.DeliveryService>();
            kernel.Bind<Cats.Services.Logistics.IDeliveryDetailService>().To<Cats.Services.Logistics.DeliveryDetailService>();
            kernel.Bind<ITransportBidQuotationHeader>().To<TransportBidQuotationHeaderHeaderService>();

            kernel.Bind<IUtilizationHeaderSerivce>().To<UtilizationHeaderSerivce>();
            kernel.Bind<IUtilizationDetailSerivce>().To<UtilizationDetailService>();
            kernel.Bind<IDistributionByAgeDetailService>().To<DistributionByAgeDetailService>();
          
            kernel.Bind<Cats.Services.EarlyWarning.ICommodityTypeService>().To<Cats.Services.EarlyWarning.CommodityTypeService>();
            kernel.Bind<IReceiptPlanService>().To<ReceiptPlanService>();
            kernel.Bind<IReceiptPlanDetailService>().To<ReceiptPlanDetailService>();
            kernel.Bind<IDeliveryReconcileService>().To<DeliveryReconcileService>();
            kernel.Bind<ILocalPurchaseService>().To<LocalPurchaseService>();
            kernel.Bind<ILocalPurchaseDetailService>().To<LocalPurchaseDetailService>();
            kernel.Bind<IDonationPlanDetailService>().To<DonationPlanDetailService>();
            kernel.Bind<IDonationPlanHeaderService>().To<DonationPlanHeaderService>();
            kernel.Bind<ILoanReciptPlanService>().To<LoanReciptPlanService>();
            kernel.Bind<IRegionalDashboard>().To<RegionalDashboard>();
            kernel.Bind<ILoanReciptPlanDetailService>().To<LoanReciptPlanDetailService>();
            kernel.Bind<ITransferService>().To<TransferService>();
            kernel.Bind<IEWDashboardService>().To<EWDashboardService>();
        }
        private void AddBindingsHub()
        {
            kernel.Bind<Data.Hub.UnitWork.IUnitOfWork>().To<Cats.Data.Hub.UnitOfWork>();
            kernel.Bind<Cats.Services.Hub.IFDPService>().To<Cats.Services.Hub.FDPService>();
            kernel.Bind<Cats.Services.Hub.IAdminUnitService>().To<Cats.Services.Hub.AdminUnitService>();
            kernel.Bind<Cats.Services.Hub.ICommodityService>().To<Cats.Services.Hub.CommodityService>();
            kernel.Bind<Cats.Services.Hub.ITransporterService>().To<Cats.Services.Hub.TransporterService>();
            kernel.Bind<Cats.Services.Hub.IShippingInstructionService>().To<Cats.Services.Hub.ShippingInstructionService>();
            kernel.Bind<Cats.Services.Hub.ITransactionService>().To<Cats.Services.Hub.TransactionService>();
            kernel.Bind<Cats.Services.Hub.IUnitService>().To<Cats.Services.Hub.UnitService>();
            kernel.Bind<Cats.Services.Hub.IUserProfileService>().To<Cats.Services.Hub.UserProfileService>();
            kernel.Bind<Cats.Services.Hub.IUserRoleService>().To<Cats.Services.Hub.UserRoleService>();
            kernel.Bind<Cats.Services.Hub.IUserHubService>().To<Cats.Services.Hub.UserHubService>();
            kernel.Bind<Cats.Services.Hub.ICommodityTypeService>().To<Cats.Services.Hub.CommodityTypeService>();
            kernel.Bind<Cats.Services.Hub.ICommodityGradeService>().To<Cats.Services.Hub.CommodityGradeService>();
            kernel.Bind<Cats.Services.Hub.ICommoditySourceService>().To<Cats.Services.Hub.CommoditySourceService>();
            kernel.Bind<Cats.Services.Hub.IContactService>().To<Cats.Services.Hub.ContactService>();
            kernel.Bind<Cats.Services.Hub.IInternalMovementService>().To<Cats.Services.Hub.InternalMovementService>();
            kernel.Bind<Cats.Services.Hub.IStoreService>().To<Cats.Services.Hub.StoreService>();
            kernel.Bind<Cats.Services.Hub.IProjectCodeService>().To<Cats.Services.Hub.ProjectCodeService>();
            kernel.Bind<Cats.Services.Hub.IProgramService>().To<Cats.Services.Hub.ProgramService>();
            kernel.Bind<Cats.Services.Hub.IDispatchAllocationService>().To<Cats.Services.Hub.DispatchAllocationService>();
            kernel.Bind<Cats.Services.Hub.IDispatchService>().To<Cats.Services.Hub.DispatchService>();
            kernel.Bind<Cats.Services.Hub.IOtherDispatchAllocationService>().To<Cats.Services.Hub.OtherDispatchAllocationService>();
            kernel.Bind<Cats.Services.Hub.IDispatchDetailService>().To<Cats.Services.Hub.DispatchDetailService>();
            kernel.Bind<Cats.Services.Hub.IPeriodService>().To<Cats.Services.Hub.PeriodService>();
            kernel.Bind<Cats.Services.Hub.IHubService>().To<Cats.Services.Hub.HubService>();
            kernel.Bind<Cats.Services.Hub.IReceiveService>().To<Cats.Services.Hub.ReceiveService>();
            kernel.Bind<Cats.Web.Hub.IMembershipWrapper>().To<Cats.Web.Hub.MembershipWrapper>(); ;
            kernel.Bind<Cats.Web.Hub.IUrlHelperWrapper>().To<Cats.Web.Hub.UrlHelperWrapper>();
            kernel.Bind<Cats.Web.Hub.IFormsAuthenticationWrapper>().To<Cats.Web.Hub.FormsAuthenticationWrapper>();
            kernel.Bind<Cats.Services.Hub.IForgetPasswordRequestService>().To<Cats.Services.Hub.ForgetPasswordRequestService>();
            kernel.Bind<Cats.Services.Hub.ISettingService>().To<Cats.Services.Hub.SettingService>();
            kernel.Bind<Cats.Services.Hub.IAccountService>().To<Cats.Services.Hub.AccountService>();
            kernel.Bind<Cats.Services.Hub.IAdjustmentReasonService>().To<Cats.Services.Hub.AdjustmentReasonService>();
            kernel.Bind<Cats.Services.Hub.IAdjustmentService>().To<Cats.Services.Hub.AdjustmentService>();
            kernel.Bind<Cats.Services.Hub.IAuditService>().To<Cats.Services.Hub.AuditSevice>();
            kernel.Bind<Cats.Services.Hub.ICommonService>().To<Cats.Services.Hub.CommonService>();
            kernel.Bind<Cats.Services.Hub.IDonorService>().To<Cats.Services.Hub.DonorService>();
            kernel.Bind<IErrorLogService>().To<Cats.Services.Hub.ErrorLogService>();
            kernel.Bind<Cats.Services.Hub.IGiftCertificateDetailService>().To<Cats.Services.Hub.GiftCertificateDetailService>();
            kernel.Bind<Cats.Services.Hub.IGiftCertificateService>().To<Cats.Services.Hub.GiftCertificateService>();
            kernel.Bind<Cats.Services.Hub.IHubOwnerService>().To<Cats.Services.Hub.HubOwnerService>();
            kernel.Bind<Cats.Services.Hub.IHubSettingService>().To<Cats.Services.Hub.HubSettingService>();
            kernel.Bind<Cats.Services.Hub.IHubSettingValueService>().To<Cats.Services.Hub.HubSettingValueService>();
            kernel.Bind<Cats.Services.Hub.ILedgerService>().To<Cats.Services.Hub.LedgerService>();
            kernel.Bind<Cats.Services.Hub.ILedgerTypeService>().To<Cats.Services.Hub.LedgerTypeService>();
            kernel.Bind<Cats.Services.Hub.ILetterTemplateService>().To<Cats.Services.Hub.LetterTemplateService>();
            kernel.Bind<Cats.Services.Hub.IMasterService>().To<Cats.Services.Hub.MasterService>();
            kernel.Bind<Cats.Services.Hub.IPartitionService>().To<Cats.Services.Hub.PartitionService>();
            kernel.Bind<Cats.Services.Hub.IReceiptAllocationService>().To<Cats.Services.Hub.ReceiptAllocationService>();
            kernel.Bind<Cats.Services.Hub.IReceiveDetailService>().To<Cats.Services.Hub.ReceiveDetailService>();
            kernel.Bind<Cats.Services.Hub.IReleaseNoteService>().To<Cats.Services.Hub.ReleaseNoteService>();
            kernel.Bind<Cats.Services.Hub.IRoleService>().To<Cats.Services.Hub.RoleService>();
            kernel.Bind<Cats.Services.Hub.ITranslationService>().To<Cats.Services.Hub.TranslationService>();
            kernel.Bind<Cats.Services.Hub.ITransactionGroupService>().To<Cats.Services.Hub.TransactionGroupService>();
            kernel.Bind<Cats.Services.Hub.IStackEventTypeService>().To<Cats.Services.Hub.StackEventTypeService>();
            kernel.Bind<Cats.Services.Hub.IStackEventService>().To<Cats.Services.Hub.StackEventService>();
            kernel.Bind<Cats.Services.Hub.ISMSService>().To<Cats.Services.Hub.SMSService>();
            kernel.Bind<Cats.Services.Hub.ISessionHistoryService>().To<Cats.Services.Hub.SessionHistoryService>();
            kernel.Bind<Cats.Services.Hub.ISessionAttemptService>().To<Cats.Services.Hub.SessionAttemptService>();
            kernel.Bind<Cats.Services.Hub.IDetailService>().To<Cats.Services.Hub.DetailService>();
           
            //kernel.Bind<Cats.Services.Hub.Interfaces.IStockStatusService>().To<Cats.Services.Hub.DetailService>();
           
        }
    }
}