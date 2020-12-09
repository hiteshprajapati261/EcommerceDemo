using EcommerceDemo.Data.IoC;
using EcommerceDemo.Models.Interface;
using EcommerceDemo.Services.Interfaces;
using EcommerceDemo.Services.Services;
using Microsoft.Practices.Unity;

namespace EcommerceDemo.Services.IoC
{
    public class ServiceConfiguration : IUnityConfiguration
    {
        public void Configure(IUnityContainer container)
        {
            var dataAccessConfiguration = new DataConfiguration();
            dataAccessConfiguration.Configure(container);
                       
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IProductCategoryService, ProductCategoryService>();
            container.RegisterType<IProductAttributeLookupService, ProductAttributeLookupService>();
        }
    }
}
