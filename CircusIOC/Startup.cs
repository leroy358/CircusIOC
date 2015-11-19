using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CircusIOC.Startup))]
namespace CircusIOC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
