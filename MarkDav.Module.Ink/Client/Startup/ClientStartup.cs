using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using MarkDav.Module.Ink.Services;

namespace MarkDav.Module.Ink.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IInkService, InkService>();
        }
    }
}
