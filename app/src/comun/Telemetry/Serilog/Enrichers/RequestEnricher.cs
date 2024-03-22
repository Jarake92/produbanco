using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;
using shared.comun.hearth.constantes;

namespace shared.comun.Telemetry.Serilog.Enrichers;

public static class RequestEnricher
{
    public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        var request = httpContext.Request;

        diagnosticContext.Set("Host", request.Host);
        diagnosticContext.Set("Protocol", request.Protocol);
        diagnosticContext.Set("Scheme", request.Scheme);

        if (request.QueryString.HasValue)
            diagnosticContext.Set("QueryString", request.QueryString.Value);

        diagnosticContext.Set("ContentType", httpContext.Response.ContentType);

        var endpoint = httpContext.GetEndpoint();
        if (endpoint is not null)
            diagnosticContext.Set("EndpointName", endpoint.DisplayName);
    }

    public static LogEventLevel CustomGetLevel(HttpContext ctx, double _, Exception? ex)
    {
        return ex is not null
            ? LogEventLevel.Error
            : ctx.Response.StatusCode > 499
                ? LogEventLevel.Error
                : ctx.Response.StatusCode > 399
                    ? LogEventLevel.Warning
                    : IsHealthCheckEndpoint(ctx)
                        ? LogEventLevel.Verbose
                        : LogEventLevel.Information;
    }

    private static bool IsHealthCheckEndpoint(HttpContext ctx)
    {
        var endpoint = ctx.GetEndpoint();
        return endpoint is not null && string.Equals(endpoint.DisplayName, ApiHealthConstantes.EndPoint.LiveEndpoint,
            StringComparison.OrdinalIgnoreCase);
    }
}