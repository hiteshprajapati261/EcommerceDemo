namespace EcommerceDemo.Models.Interface
{
    /// <summary>
    /// Implements an extended unity dependency resolver that exposes the Resolve method so that it can be used globally.
    /// </summary>
    public interface IExtendedUnityDependencyResolver
    {
        T Resolve<T>();
    }
}
