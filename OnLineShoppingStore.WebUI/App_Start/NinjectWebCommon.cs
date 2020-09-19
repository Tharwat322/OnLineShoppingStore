[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(OnLineShoppingStore.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(OnLineShoppingStore.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace OnLineShoppingStore.WebUI.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Moq;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using OnLineShoppingStore.Abstract;
    using OnLineShoppingStore.Concrete;
    using OnLineShoppingStore.Domain.Concrete;
    using OnLineShoppingStore.Domain.Entities;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IProductRepository>().To<EFProductRepository>();
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>();
            kernel.Bind<IAuthentication>().To<FormsAuthenticationProvider>();
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product { Name = "FootBall",Price = 100},
            //    new Product { Name = "Surf Board",Price = 150},
            //    new Product { Name = "Running Shoes",Price = 200}

            //});
            //kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }        
    }
}
