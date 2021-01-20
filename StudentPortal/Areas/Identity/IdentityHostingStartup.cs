using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(StudentPortal.UI.Areas.Identity.IdentityHostingStartup))]
namespace StudentPortal.UI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}