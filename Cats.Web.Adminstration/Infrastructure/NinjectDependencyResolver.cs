using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cats.Services.Administration;
using Cats.Services.Security;
using LanguageHelpers.Localization.Services;
using NetSqlAzMan;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.Providers;
using Ninject;
using log4net;
using CommodityTypeService = Cats.Services.Administration.CommodityTypeService;
using DonorService = Cats.Services.Administration.DonorService;
using ICommodityTypeService = Cats.Services.Administration.ICommodityTypeService;
using IDonorService = Cats.Services.Administration.IDonorService;


namespace Cats.Web.Administration.Infrastructure
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
            kernel.Bind<Data.UnitWork.IUnitOfWork>().To<Data.UnitWork.UnitOfWork>();
            kernel.Bind<Data.Security.IUnitOfWork>().To<Data.Security.UnitOfWork>();
            kernel.Bind<LanguageHelpers.Localization.Data.IUnitOfWork>().To<LanguageHelpers.Localization.Data.UnitOfWork>();
          
            kernel.Bind<IUserAccountService>().To<UserAccountService>();
           
            //kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<ILocalizedTextService>().To<LocalizedTextService>();
           // kernel.Bind<LanguageHelpers.Localization.Data.IUnitOfWork>().To<LanguageHelpers.Localization.Data.UnitOfWork>();
            kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.DeclaringType));
            kernel.Bind<ILanguageService>().To<LanguageService>();
            kernel.Bind<IDonorService>().To<DonorService>();
            kernel.Bind<IHubOwnerService>().To<HubOwnerService>();
            kernel.Bind<ICommodityTypeService>().To<CommodityTypeService>();
            kernel.Bind<IUserHubService>().To<UserHubService>();
            kernel.Bind<IProgramService>().To<ProgramService>();
            kernel.Bind<IUnitService>().To<UnitService>();
            kernel.Bind<IStoreService>().To<StoreService>();
            kernel.Bind<ICommodityGradeService>().To<CommodityGradeService>();

            kernel.Bind<IHubService>().To<HubService>();
            kernel.Bind<ICommoditySourceService>().To<CommoditySourceService>();
            kernel.Bind<ICommodityService>().To<CommodityService>();
            kernel.Bind<IAuditService>().To<AuditService>();
            kernel.Bind<IUserProfileService>().To<UserProfileService>();
            kernel.Bind<IAdminUnitService>().To<AdminUnitService>();
            kernel.Bind<IFDPService>().To<FDPService>();
            kernel.Bind<IWoredaDonorService>().To<WoredaDonorService>();
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