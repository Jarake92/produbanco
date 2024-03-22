using BFF.Identidad.API.Initializers;
using Microsoft.ApplicationInsights.Extensibility;
using Serilog;

namespace BFF.Identidad.API.Extensiones
{
    public static class LogsExtensions
    {
        
        public static void UsarSerilog(this LoggerConfiguration hostBuilder, IConfigurationRoot logConfig, IServiceProvider services)
        {
            hostBuilder
                .ReadFrom.Configuration(logConfig)
                .ReadFrom.Services(services);
        }

        public static void ConfigurarApplicationInsight(this IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddSingleton<ITelemetryInitializer>(new RoleNameTelemetryInitializer("Identidad"));
        }
    }
}
