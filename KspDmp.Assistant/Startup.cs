using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KspDmp.Assistant.Startup))]
namespace KspDmp.Assistant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
