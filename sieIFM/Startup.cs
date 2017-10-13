using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCSignalRtest2.Startup))]
namespace MVCSignalRtest2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
            
        }
        
    }
}
