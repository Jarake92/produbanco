using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using shared.comun.Telemetry.Initializers;
using shared.comun.Telemetry.Processors;

namespace shared.comun.Telemetry.Extensiones;

public static class TelemetryExtensions
{
    public static void AddTelemetry(this IServiceCollection services, TelemetryConfiguration configuration)
    {
        services.AddApplicationInsightsTelemetry();
        services.AddSingleton<ITelemetryInitializer>(new RoleNameTelemetryInitializer(configuration.RoleName));
        
        if (configuration.LogStaticResources)
        {
            LogStaticResources(services);
        }
        
        if (configuration.LogSqlStatements)
        {
            LogSqlDependencyTracking(services);
        }
        
        if (configuration.LogSqlParameters)
        {
            LogSqlParameters(services);
        }
    }
    
    public static void AddSerilog(this WebApplicationBuilder builder, string configurationFile)
    {
        var serilogConfiguration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(configurationFile, optional: false, reloadOnChange: true)
            .Build();

        builder.Host.UseSerilog((_, services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(serilogConfiguration)
                .ReadFrom.Services(services);
        });
    }

    private static void LogStaticResources(this IServiceCollection services)
    {
        services.AddApplicationInsightsTelemetryProcessor<SuppressStaticResourcesProcessor>();
    }

    private static void LogSqlDependencyTracking(this IServiceCollection services)
    {
        services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, _) =>
        {
            module.EnableSqlCommandTextInstrumentation = true;
        });
    }
    
    private static void LogSqlParameters(this IServiceCollection services)
    {
        services.AddSingleton<ITelemetryInitializer, SqlTelemetryInitializer>();
    }
}