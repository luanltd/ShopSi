using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopSi.Startup))]
namespace ShopSi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
