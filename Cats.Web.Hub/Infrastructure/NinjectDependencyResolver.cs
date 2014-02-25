using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Data.Hub;
using Cats.Data.Hub.UnitWork;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Services.Hub.Interfaces;
using Cats.Services.Security;
using Cats.Security;
using NetSqlAzMan;
using NetSqlAzMan.Providers;
using Ninject;
using log4net;
using ForgetPasswordRequestService = Cats.Services.Hub.ForgetPasswordRequestService;
using IForgetPasswordRequestService = Cats.Services.Hub.IForgetPasswordRequestService;
using ISettingService = Cats.Services.Hub.ISettingService;
using SettingService = Cats.Services.Hub.SettingService;
using NetSqlAzMan.Interfaces;

namespace Cats.Web.Hub.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            this.kernel = new StandardKernel();
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
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IFDPService>().To<FDPService>();
            kernel.Bind<IAdminUnitService>().To<AdminUnitService>();
            kernel.Bind<ICommodityService>().To<CommodityService>();
            kernel.Bind<ITransporterService>().To<TransporterService>();
            kernel.Bind<IShippingInstructionService>().To<ShippingInstructionService>();
            kernel.Bind<ITransactionService>().To<TransactionService>();
            kernel.Bind<IUnitService>().To<UnitService>();
            kernel.Bind<IUserProfileService>().To<UserProfileService>();
            kernel.Bind<IUserRoleService>().To<UserRoleService>();
            kernel.Bind<IUserHubService>().To<UserHubService>();
            kernel.Bind<ICommodityTypeService>().To<CommodityTypeService>();
            kernel.Bind<ICommodityGradeService>().To<CommodityGradeService>();
            kernel.Bind<ICommoditySourceService>().To<CommoditySourceService>();
            kernel.Bind<IContactService>().To<ContactService>();
            kernel.Bind<IInternalMovementService>().To<InternalMovementService>();
            kernel.Bind<IStoreService>().To<StoreService>();
            kernel.Bind<IProjectCodeService>().To<ProjectCodeService>();
            kernel.Bind<IProgramService>().To<ProgramService>();
            kernel.Bind<IDispatchAllocationService>().To<DispatchAllocationService>();
            kernel.Bind<IDispatchService>().To<DispatchService>();
            kernel.Bind<IOtherDispatchAllocationService>().To<OtherDispatchAllocationService>();
            kernel.Bind<IDispatchDetailService>().To<DispatchDetailService>();
            kernel.Bind<IPeriodService>().To<PeriodService>();
            kernel.Bind<IHubService>().To<HubService>();
            kernel.Bind<IReceiveService>().To<ReceiveService>();
            kernel.Bind<IMembershipWrapper>().To<MembershipWrapper>(); ;
            kernel.Bind<IUrlHelperWrapper>().To<UrlHelperWrapper>();
            kernel.Bind<IFormsAuthenticationWrapper>().To<FormsAuthenticationWrapper>();
            kernel.Bind<IForgetPasswordRequestService>().To<ForgetPasswordRequestService>();
            kernel.Bind<ISettingService>().To<SettingService>();
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IAdjustmentReasonService>().To<AdjustmentReasonService>();
            kernel.Bind<IAdjustmentService>().To<AdjustmentService>();
            kernel.Bind<IAuditService>().To<AuditSevice>();
            kernel.Bind<ICommonService>().To<CommonService>();
            kernel.Bind<IDonorService>().To<DonorService>();
            kernel.Bind<IErrorLogService>().To<ErrorLogService>();
            kernel.Bind<IGiftCertificateDetailService>().To<GiftCertificateDetailService>();
            kernel.Bind<IGiftCertificateService>().To<GiftCertificateService>();
            kernel.Bind<IHubOwnerService>().To<HubOwnerService>();
            kernel.Bind<IHubSettingService>().To<HubSettingService>();
            kernel.Bind<IHubSettingValueService>().To<HubSettingValueService>();
            kernel.Bind<ILedgerService>().To<LedgerService>();
            kernel.Bind<ILedgerTypeService>().To<LedgerTypeService>();
            kernel.Bind<ILetterTemplateService>().To<LetterTemplateService>();
            kernel.Bind<IMasterService>().To<MasterService>();
            kernel.Bind<IPartitionService>().To<PartitionService>();
            kernel.Bind<IReceiptAllocationService>().To<ReceiptAllocationService>();
            kernel.Bind<IReceiveDetailService>().To<ReceiveDetailService>();
            kernel.Bind<IReleaseNoteService>().To<ReleaseNoteService>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<ITranslationService>().To<TranslationService>();
            kernel.Bind<ITransactionGroupService>().To<TransactionGroupService>();
            kernel.Bind<IStackEventTypeService>().To<StackEventTypeService>();
            kernel.Bind<IStackEventTypeService>().To<StackEventTypeService>();
            kernel.Bind<Cats.Services.Hub.Interfaces.IStockStatusService>().To<Cats.Services.Hub.StockStatusService>();
            kernel.Bind<ISMSService>().To<SMSService>();
            kernel.Bind<ISessionHistoryService>().To<SessionHistoryService>();
            kernel.Bind<ISessionAttemptService>().To<SessionAttemptService>();
            kernel.Bind<IDetailService>().To<DetailService>();
            kernel.Bind<IUserAccountService>().To<UserAccountService>();
            kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.DeclaringType));
            kernel.Bind<Cats.Data.Security.IUnitOfWork>().To<Cats.Data.Security.UnitOfWork>();
            kernel.Bind<IAzManStorage>().To<SqlAzManStorage>().WithConstructorArgument("connectionString",
                                                                                    System.Configuration.
                                                                                        ConfigurationManager.
                                                                                        ConnectionStrings[
                                                                                            "SecurityContext"].
                                                                                        ConnectionString);
            kernel.Bind<NetSqlAzManRoleProvider>().To<NetSqlAzManRoleProvider>();
         
        }
    }
}

