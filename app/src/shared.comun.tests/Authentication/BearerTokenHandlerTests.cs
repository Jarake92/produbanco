using System.Net;
using Microsoft.AspNetCore.Http;
using shared.comun.Authentication.HttpMessageHandlers;

namespace shared.comun.tests.Authentication;

public class BearerTokenHandlerTests
{
    private const string TestToken = "testToken";

    [Fact]
    public async Task RequestWithToken_ShouldAddAuthorizationHeader()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = $"Bearer {TestToken}";
        var httpContextAccessor = new HttpContextAccessor { HttpContext = httpContext };

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://example.com/");
        var handler = new BearerTokenHandler(httpContextAccessor)
        {
            InnerHandler = new TestHandler()
        };

        // Act
        var invoker = new HttpMessageInvoker(handler);
        var response = await invoker.SendAsync(httpRequestMessage, CancellationToken.None);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    private class TestHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Assert.Equal($"Bearer {TestToken}", request.Headers.GetValues("Authorization").First());

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }
}