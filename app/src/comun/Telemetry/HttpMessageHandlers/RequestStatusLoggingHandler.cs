using System.Net.Sockets;
using Microsoft.Extensions.Logging;

namespace shared.comun.Telemetry.HttpMessageHandlers;

public class RequestStatusLoggingHandler : DelegatingHandler
{
    private readonly ILogger<RequestStatusLoggingHandler> _logger;

    public RequestStatusLoggingHandler(ILogger<RequestStatusLoggingHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Received a non-success status code {StatusCode} from {Url}",
                    (int)response.StatusCode, response.RequestMessage?.RequestUri);
            }

            return response;
        }
        catch (HttpRequestException ex)
            when (ex.InnerException is SocketException { SocketErrorCode: SocketError.ConnectionRefused })
        {
            var hostWithPort = request.RequestUri is not null && request.RequestUri.IsDefaultPort
                ? request.RequestUri.DnsSafeHost
                : $"{request.RequestUri?.DnsSafeHost}:{request.RequestUri?.Port}";

            _logger.LogCritical(ex,
                "Unable to connect to {Host}. Please check the configuration to ensure the correct URL for the service has been configured.",
                hostWithPort);
            
            throw;
        }
    }
}