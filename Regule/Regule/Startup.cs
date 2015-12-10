using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Regule.Startup))]
namespace Regule
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
