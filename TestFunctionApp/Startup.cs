using LoggerService;
using Serilog;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

[assembly: WebJobsStartup(typeof(Startup))]
namespace LoggerService
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
           builder.Services.TryAddScoped<ILoggerService, LoggerService>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddScoped<ILoggerService, LoggerService>();
        }
    }
}

