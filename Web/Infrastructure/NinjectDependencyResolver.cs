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
            kernel.Bind<ITransportRequisitionService>().To<TransportRequisitionService>();

            kernel.Bind<IHubService>().To<HubService>();
            kernel.Bind<ITransporterService>().To<TransporterService>();

            kernel.Bind<IBidDetailService>().To<BidDetailService>();
            kernel.Bind <IHubAllocationService>().To<HubAllocationService>();
            
            

            kernel.Bind<ITransportBidPlanService>().To<TransportBidPlanService>();
            kernel.Bind<ITransportBidPlanDetailService>().To<TransportBidPlanDetailService>();
            kernel.Bind<IBidDetailService>().To<BidDetailService>();  

            // Security service registration
            kernel.Bind<IUserAccountService>().To<UserAccountService>();
            kernel.Bind<Cats.Data.Security.IUnitOfWork>().To<Cats.Data.Security.UnitOfWork>();



        }
    }
}