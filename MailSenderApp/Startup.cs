using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MailSenderApp.Startup))]
namespace MailSenderApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
