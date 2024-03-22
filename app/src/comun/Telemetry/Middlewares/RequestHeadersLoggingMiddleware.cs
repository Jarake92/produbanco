using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;

namespace shared.comun.Telemetry.Middlewares;

public class RequestHeadersLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestHeadersLoggingMiddleware> _logger;
    private readonly IDiagnosticContext _diagnosticContext;

    public RequestHeadersLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestHeadersLoggingMiddleware> logger,
        IDiagnosticContext diagnosticContext)
    {
        _next = next;
        _logger = logger;
        _diagnosticContext = diagnosticContext;
    }

    public async Task Invoke(HttpContext context)
    {
        if (_logger.IsEnabled(LogLevel.Information))
            _diagnosticContext.Set("RequestHeaders", context.Request.Headers, true);

        await _next(context);
    }
}