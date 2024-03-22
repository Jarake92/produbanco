using System.Text;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using shared.comun.Telemetry.Middlewares;

namespace shared.comun.tests.Telemetry.MiddlewareTests;

public class RequestBodyLoggingMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_LogsRequestBody()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<RequestBodyLoggingMiddleware>>();
        var context = new DefaultHttpContext();
        var requestTelemetry = new RequestTelemetry();

        const string requestBody = "Test request body";
        var requestBodyBytes = Encoding.UTF8.GetBytes(requestBody);

        context.Request.Method = HttpMethods.Get;
        context.Request.Body = new MemoryStream(requestBodyBytes);
        context.Request.ContentLength = requestBodyBytes.Length;
        context.Features.Set(requestTelemetry);

        var nextMidlewareMock = new Mock<RequestDelegate>();

        var middleware = new RequestBodyLoggingMiddleware(nextMidlewareMock.Object, loggerMock.Object);

        // Set log level to Information to enable logging
        loggerMock.Setup(x => x.IsEnabled(LogLevel.Information)).Returns(true);

        // Act
        await middleware.Invoke(context);

        // Assert
        Assert.True(requestTelemetry.Properties.ContainsKey("RequestBody"));
        Assert.Equal(requestBody, requestTelemetry.Properties["RequestBody"]);
        nextMidlewareMock.Verify(next => next(context), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_LogsRequestBodyWithEllipsis()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<RequestBodyLoggingMiddleware>>();
        var context = new DefaultHttpContext();
        var requestTelemetry = new RequestTelemetry();

        const string requestBody = "Test request body";
        var requestBodyBytes = Encoding.UTF8.GetBytes(requestBody);

        context.Request.Method = HttpMethods.Get;
        context.Request.Body = new MemoryStream(requestBodyBytes);
        context.Request.ContentLength = requestBodyBytes.Length;
        context.Features.Set(requestTelemetry);
        const int maxBodySizeInBytes = 5;

        var nextMidlewareMock = new Mock<RequestDelegate>();

        var middleware =
            new RequestBodyLoggingMiddleware(nextMidlewareMock.Object, loggerMock.Object, maxBodySizeInBytes);

        // Set log level to Information to enable logging
        loggerMock.Setup(x => x.IsEnabled(LogLevel.Information)).Returns(true);

        // Act
        await middleware.Invoke(context);

        // Assert
        Assert.True(requestTelemetry.Properties.ContainsKey("RequestBody"));
        Assert.Equal(requestBody[..maxBodySizeInBytes] + " ...", requestTelemetry.Properties["RequestBody"]);
        nextMidlewareMock.Verify(next => next(context), Times.Once);
    }
}