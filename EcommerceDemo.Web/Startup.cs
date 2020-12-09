using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EcommerceDemo.Web.Startup))]
namespace EcommerceDemo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
