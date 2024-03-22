using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace shared.comun.Telemetry.Middlewares;

public class ResponseBodyLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ResponseBodyLoggingMiddleware> _logger;
    private readonly int _maxBodySizeInBytes;

    public ResponseBodyLoggingMiddleware(
        RequestDelegate next,
        ILogger<ResponseBodyLoggingMiddleware> logger,
        int maxBodySizeInBytes = 1024 * 4)
    {
        _next = next;
        _logger = logger;
        _maxBodySizeInBytes = maxBodySizeInBytes;
    }

    public async Task Invoke(HttpContext context)
    {
        if (_logger.IsEnabled(LogLevel.Information))
            await AddResponseBodyToTelemetry(context);
        else
            await _next(context);
    }

    private async Task AddResponseBodyToTelemetry(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        try
        {
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context);

            memoryStream.Position = 0;
            var reader = new StreamReader(memoryStream);

            string responseBody;
            if (memoryStream.Length > _maxBodySizeInBytes)
            {
                var buffer = new char[_maxBodySizeInBytes];
                _ = await reader.ReadAsync(buffer, 0, _maxBodySizeInBytes);
                responseBody = $"{new string(buffer)} ...";
            }
            else
            {
                responseBody = await reader.ReadToEndAsync();
            }

            memoryStream.Position = 0;
            await memoryStream.CopyToAsync(originalBodyStream);

            var requestTelemetry = context.Features.Get<RequestTelemetry>();
            requestTelemetry?.Properties.Add("ResponseBody", responseBody);
        }
        finally
        {
            context.Response.Body = originalBodyStream;
        }
    }
}