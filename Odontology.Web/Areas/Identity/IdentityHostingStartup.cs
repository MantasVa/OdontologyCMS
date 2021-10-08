using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(PhysicalTherapyProjectV2.Areas.Identity.IdentityHostingStartup))]
namespace PhysicalTherapyProjectV2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}