using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Eat.Startup))]
namespace Eat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
