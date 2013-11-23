using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(dziennik_asp_mvc.Startup))]
namespace dziennik_asp_mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
