using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using shared.comun.Telemetry.Middlewares;

namespace shared.comun.tests.Telemetry.MiddlewareTests;

public class ResponseBodyLoggingMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_LogsResponseBody()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<ResponseBodyLoggingMiddleware>>();
        var context = new DefaultHttpContext();
        var requestTelemetry = new RequestTelemetry();
        const string responseBody = "Test response body";

        context.Features.Set(requestTelemetry);

        var nextMidlewareMock = new RequestDelegate(c =>
        {
            c.Response.WriteAsync(responseBody);
            return Task.CompletedTask;
        });

        var middleware = new ResponseBodyLoggingMiddleware(nextMidlewareMock, loggerMock.Object);

        // Set log level to Information to enable logging
        loggerMock.Setup(x => x.IsEnabled(LogLevel.Information)).Returns(true);

        // Act
        await middleware.Invoke(context);

        // Assert
        Assert.True(requestTelemetry.Properties.ContainsKey("ResponseBody"));
        Assert.Equal(responseBody, requestTelemetry.Properties["ResponseBody"]);
    }
    
    [Fact]
    public async Task InvokeAsync_LogsResponseBodyWithEllipsis()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<ResponseBodyLoggingMiddleware>>();
        var context = new DefaultHttpContext();
        var requestTelemetry = new RequestTelemetry();
        const string responseBody = "Test response body";
        const int maxBodySizeInBytes = 5;

        context.Features.Set(requestTelemetry);

        var nextMidlewareMock = new RequestDelegate(c =>
        {
            c.Response.WriteAsync(responseBody);
            return Task.CompletedTask;
        });

        var middleware = new ResponseBodyLoggingMiddleware(nextMidlewareMock, loggerMock.Object, maxBodySizeInBytes);

        // Set log level to Information to enable logging
        loggerMock.Setup(x => x.IsEnabled(LogLevel.Information)).Returns(true);

        // Act
        await middleware.Invoke(context);

        // Assert
        Assert.True(requestTelemetry.Properties.ContainsKey("ResponseBody"));
        Assert.Equal($"{responseBody[..maxBodySizeInBytes]} ...", requestTelemetry.Properties["ResponseBody"]);
    }
}