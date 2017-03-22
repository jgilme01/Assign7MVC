using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assign7MVC.Startup))]
namespace Assign7MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
