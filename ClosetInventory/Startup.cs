using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClosetInventory.Startup))]
namespace ClosetInventory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
