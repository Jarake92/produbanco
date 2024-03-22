using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using shared.comun.Telemetry.Middlewares;

namespace shared.comun.tests.Telemetry.MiddlewareTests;

public class ExceptionLoggingMiddlewareTests
{
    [Fact]
    public async Task Invoke_LogsErrorExceptionAndThrows()
    {
        // Arrange
        var context = new DefaultHttpContext
        {
            Response =
            {
                StatusCode = 500
            }
        };
        var loggerMock = new Mock<ILogger<ExceptionLoggingMiddleware>>();
        var nextMiddlewareMock = new Mock<RequestDelegate>();

        var exception = new Exception("Test exception");
        nextMiddlewareMock.Setup(next => next(context)).Throws(exception);

        var middleware = new ExceptionLoggingMiddleware(
            nextMiddlewareMock.Object,
            loggerMock.Object);

        // Act
        await Assert.ThrowsAsync<Exception>(() => middleware.Invoke(context));
        loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));

    }
    
    [Fact]
    public async Task Invoke_LogsWarningExceptionAndThrows()
    {
        // Arrange
        var context = new DefaultHttpContext
        {
            Response =
            {
                StatusCode = 400
            }
        };
        var loggerMock = new Mock<ILogger<ExceptionLoggingMiddleware>>();
        var nextMiddlewareMock = new Mock<RequestDelegate>();

        var exception = new Exception("Test exception");
        nextMiddlewareMock.Setup(next => next(context)).Throws(exception);

        var middleware = new ExceptionLoggingMiddleware(
            nextMiddlewareMock.Object,
            loggerMock.Object);

        // Act
        await Assert.ThrowsAsync<Exception>(() => middleware.Invoke(context));
        loggerMock.Verify(
            x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));

    }
}