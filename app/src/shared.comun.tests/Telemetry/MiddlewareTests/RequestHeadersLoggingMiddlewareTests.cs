using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using shared.comun.Telemetry.Middlewares;

namespace shared.comun.tests.Telemetry.MiddlewareTests;

public class RequestHeadersLoggingMiddlewareTests
{
    [Fact]
    public async Task Invoke_LogsRequestHeaders()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<RequestHeadersLoggingMiddleware>>();
        var diagnosticContextMock = new Mock<IDiagnosticContext>();
        
        // Set log level to Information to enable logging
        loggerMock.Setup(x => x.IsEnabled(LogLevel.Information)).Returns(true);
        
        var middleware = new RequestHeadersLoggingMiddleware(
            _ => Task.FromResult(0), loggerMock.Object, diagnosticContextMock.Object);

        var context = new DefaultHttpContext();
        context.Request.Headers.Add("Header1", "Value1");
        context.Request.Headers.Add("Header2", "Value2");

        // Act
        await middleware.Invoke(context);

        // Assert
        diagnosticContextMock.Verify(
            x => x.Set("RequestHeaders", context.Request.Headers, true),
            Times.Once);
    }
}