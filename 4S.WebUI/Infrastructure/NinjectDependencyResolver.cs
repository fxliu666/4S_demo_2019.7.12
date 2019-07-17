using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using _4S.Domain.Entities;
using _4S.Domain.Abstract;
using Moq;
using _4S.Domain.Concrete;

namespace _4S.WebUI.Infrastructure
{
    //依赖项解析类，生成程序所需对象
    public class NinjectDependencyResolver:IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
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
            kernel.Bind<ICustomersRepository>().To<EFProductRepository>();
        }
    }
}