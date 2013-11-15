using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cats.Data.UnitWork;
using Cats.Models;
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
using Cats.Services.Transaction;
using Cats.Services.Common;
using log4net;
using Early_Warning.Security;

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
            kernel.Bind<ITransportRequisitionService>().To<TransportRequisitionService>();

           
            kernel.Bind<IBeneficiaryAllocationService>().To<BeneficiaryAllocationService>();
            kernel.Bind<IWorkflowStatusService>().To<WorkflowStatusService>();
            kernel.Bind<ITransportBidQuotationService>().To<TransportBidQuotationService>();
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

            kernel.Bind<ILocalizedTextService>().To<LocalizedTextService>();
            kernel.Bind<ILanguageService>().To<LanguageService>();
            //kernel.Bind<ILanguageService>().To<LanguageService>();
            kernel.Bind<LanguageHelpers.Localization.Data.IUnitOfWork>().To<LanguageHelpers.Localization.Data.UnitOfWork>();

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
            kernel.Bind<IAzManStorage>().To<SqlAzManStorage>().WithConstructorArgument("connectionString",
                                                                                   System.Configuration.
                                                                                       ConfigurationManager.
                                                                                       ConnectionStrings[
                                                                                           "SecurityContext"].
                                                                                       ConnectionString);
            kernel.Bind<NetSqlAzManRoleProvider>().To<NetSqlAzManRoleProvider>();
            kernel.Bind<IEarlyWarningCheckAccess>().To<EarlyWarningCheckAccess>().WithConstructorArgument("storageConnectionString",
                                                                                   System.Configuration.
                                                                                       ConfigurationManager.
                                                                                       ConnectionStrings[
                                                                                           "SecurityContext"].
                                                                                       ConnectionString);
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
        }
        private void AddBindingsHub()
        {
            kernel.Bind<Cats.Data.Hub.IUnitOfWork>().To<Cats.Data.Hub.UnitOfWork>();
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
            kernel.Bind<Cats.Services.Hub.IErrorLogService>().To<Cats.Services.Hub.ErrorLogService>();
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
           
        }
    }
}