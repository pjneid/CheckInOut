using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CheckInOut.Web.Startup))]
namespace CheckInOut.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
