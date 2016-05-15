using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(com.GreenThumb.MVC.Startup))]
namespace com.GreenThumb.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
