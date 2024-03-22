using System.Text;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace shared.comun.Telemetry.Middlewares;

public class RequestBodyLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestBodyLoggingMiddleware> _logger;
    private readonly int _maxBodySizeInBytes;

    public RequestBodyLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestBodyLoggingMiddleware> logger,
        int maxBodySizeInBytes = 1024 * 4)
    {
        _next = next;
        _logger = logger;
        _maxBodySizeInBytes = maxBodySizeInBytes;
    }

    public async Task Invoke(HttpContext context)
    {
        if (_logger.IsEnabled(LogLevel.Information))
            await AddRequestBodyToTelemetry(context);

        await _next(context);
    }

    private async Task AddRequestBodyToTelemetry(HttpContext context)
    {
        var method = context.Request.Method;

        context.Request.EnableBuffering();

        if (context.Request.Body.CanRead &&
            (method == HttpMethods.Get || method == HttpMethods.Post || method == HttpMethods.Put))
        {
            using var reader = new StreamReader(
                context.Request.Body,
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 512, leaveOpen: true);

            string requestBody;
            if (context.Request.ContentLength > _maxBodySizeInBytes)
            {
                var buffer = new char[_maxBodySizeInBytes];
                _ = await reader.ReadAsync(buffer, 0, _maxBodySizeInBytes);
                requestBody = $"{new string(buffer)} ...";
            }
            else
            {
                requestBody = await reader.ReadToEndAsync();
            }

            context.Request.Body.Position = 0;

            var requestTelemetry = context.Features.Get<RequestTelemetry>();
            requestTelemetry?.Properties.Add("RequestBody", requestBody);
        }
    }
}