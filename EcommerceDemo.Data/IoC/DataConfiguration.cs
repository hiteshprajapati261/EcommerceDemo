using EcommerceDemo.Data.Interfaces;
using EcommerceDemo.Data.Repositories;
using EcommerceDemo.Models.Interface;
using Microsoft.Practices.Unity;

namespace EcommerceDemo.Data.IoC
{
    public class DataConfiguration : IUnityConfiguration
    {
        public void Configure(IUnityContainer container)
        {
            container.RegisterType<IDatabaseRepository, DatabaseRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDatabaseRepository, DatabaseRepository>();
                        
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IProductCategoryRepository, ProductCategoryRepository>();
            container.RegisterType<IProductAttributeLookupRepository, ProductAttributeLookupRepository>();
        }
    }
}
