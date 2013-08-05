using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Data.UnitWork;
using Cats.Services.Security;
using Ninject;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Cats.Services.Logistics;
using Cats.Services.PSNP;


namespace Cats.Infrastructure
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

            kernel.Bind<IHRDService>().To<HRDService>();
            kernel.Bind<IHRDDetailService>().To<HRDDetailService>();
            kernel.Bind<IRegionalPSNPPlanService>().To<RegionalPSNPPlanService>();
            kernel.Bind<IRegionalPSNPPlanDetailService>().To<RegionalPSNPPlanDetailService>();
        }
    }
}