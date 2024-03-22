using Microsoft.AspNetCore.Builder;
using Serilog;
using shared.comun.Telemetry.Middlewares;
using shared.comun.Telemetry.Serilog.Enrichers;

namespace shared.comun.Telemetry.Extensiones;

public static class MiddlewareExtensions
{
    public static void UseTelemetryMiddlewares(this IApplicationBuilder app,
        TelemetryMiddlewaresOptions telemetryOptions)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.EnrichDiagnosticContext = RequestEnricher.EnrichFromRequest;
            options.GetLevel = RequestEnricher.CustomGetLevel;
        });

        if (telemetryOptions.LogRequestHeaders)
            app.UseMiddleware<RequestHeadersLoggingMiddleware>();

        if (telemetryOptions.LogRequestBody)
            app.UseMiddleware<RequestBodyLoggingMiddleware>(telemetryOptions.MaxBodySizeInBytes);

        if (telemetryOptions.LogResponseBody)
            app.UseMiddleware<ResponseBodyLoggingMiddleware>(telemetryOptions.MaxBodySizeInBytes);

        if (telemetryOptions.LogExceptions)
            app.UseMiddleware<ExceptionLoggingMiddleware>();
    }
}