using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using MarkDav.Module.Lottie.Repository;
using MarkDav.Module.Lottie.Services;

namespace MarkDav.Module.Lottie.Startup
{
    public class ServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ILottieService, ServerLottieService>();
            services.AddDbContextFactory<LottieContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}
