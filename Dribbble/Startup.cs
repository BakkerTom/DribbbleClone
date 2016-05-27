using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dribbble.Startup))]
namespace Dribbble
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
