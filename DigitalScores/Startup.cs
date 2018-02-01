using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DigitalScores.Startup))]
namespace DigitalScores
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
