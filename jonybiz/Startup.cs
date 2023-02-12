using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(jonybiz.Startup))]
namespace jonybiz
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
