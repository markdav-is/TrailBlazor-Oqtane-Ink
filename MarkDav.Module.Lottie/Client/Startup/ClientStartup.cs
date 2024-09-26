using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using MarkDav.Module.Lottie.Services;

namespace MarkDav.Module.Lottie.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILottieService, LottieService>();
        }
    }
}
