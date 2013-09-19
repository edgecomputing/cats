﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cats.Services.Administration;
using Cats.Services.Security;
using LanguageHelpers.Localization.Services;
using Ninject;
using log4net;


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
            kernel.Bind<Data.UnitWork.IUnitOfWork>().To<Data.UnitWork.IUnitOfWork>();
            kernel.Bind<Data.Security.IUnitOfWork>().To<Data.Security.UnitOfWork>();
            kernel.Bind<LanguageHelpers.Localization.Data.IUnitOfWork>().To<LanguageHelpers.Localization.Data.UnitOfWork>();
          
            kernel.Bind<IUserAccountService>().To<UserAccountService>();
            kernel.Bind<ILocalizedTextService>().To<LocalizedTextService>();
            kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.DeclaringType));
            kernel.Bind<ILanguageService>().To<LanguageService>();
            kernel.Bind<IDonorService>().To<DonorService>();
            kernel.Bind<ICommodityTypeService>().To<CommodityTypeService>();
            kernel.Bind<IProgramService>().To<ProgramService>();



        }
    }
}