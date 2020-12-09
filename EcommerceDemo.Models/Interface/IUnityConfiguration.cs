namespace EcommerceDemo.Models.Interface
{
    using Microsoft.Practices.Unity;
    
    public interface IUnityConfiguration
    {
        void Configure(IUnityContainer container);
    }
}
