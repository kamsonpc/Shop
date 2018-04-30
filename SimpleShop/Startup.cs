using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleShop.Startup))]
namespace SimpleShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
