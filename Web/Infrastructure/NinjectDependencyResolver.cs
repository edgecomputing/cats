using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cats.Data.UnitWork;
using Cats.Services.Security;
using LanguageHelpers.Localization.Services;
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

            kernel.Bind<IUserDashboardPreferenceService>().To<UserDashboardPreferenceService>();
            kernel.Bind<IForgetPasswordRequestService>().To<ForgetPasswordRequestService>();
            kernel.Bind<IDashboardWidgetService>().To<DashboardWidgetService>();
            kernel.Bind<ISettingService>().To<SettingService>();
            kernel.Bind<ILedgerService>().To<LedgerService>();
            kernel.Bind<ITransReqWithoutTransporterService>().To<TransReqWithoutTransporterService>();
            kernel.Bind<ITransportOrderDetailService>().To<TransportOrderDetailService>();
            kernel.Bind<IAllocationByRegionService>().To<AllocationByRegionService>();
            kernel.Bind<IHRDPlanService>().To<HRDPlanService>();
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
    }
}