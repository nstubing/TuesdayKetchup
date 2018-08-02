using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TuesdayKetchup.Startup))]
namespace TuesdayKetchup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
