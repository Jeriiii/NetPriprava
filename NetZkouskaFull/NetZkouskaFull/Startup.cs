using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NetZkouskaFull.Startup))]
namespace NetZkouskaFull
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
