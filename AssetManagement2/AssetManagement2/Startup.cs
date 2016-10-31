using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AssetManagement2.Startup))]
namespace AssetManagement2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
