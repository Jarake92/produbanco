using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using shared.comun.proxy.modelo;
using shared.comun.service_discovery.consul.cliente;

namespace shared.comun.tests.ServiceDiscovery;

public class ApiClientTests
{
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock = new();
    private readonly Mock<ILogger<ApiClient>> _loggerMock = new();

    [Fact]
    public async Task InitializeLocal_ShouldLogInformation()
    {
        // Arrange
        var configuration = new Rootobject
        {
            ContentType = "application/json",
            ConfiguracionClient = new[]
            {
                new Configuraciones
                {
                    Nombre = "serviceName",
                    Url = "http://localhost:5000",
                    Path = "/api/v1/endpoint"
                }
            }
        };
        
        var apiClient = new TestApiClient(
            _httpClientFactoryMock.Object,
            Options.Create(configuration),
            "serviceName",
            _loggerMock.Object);

        // Act
        await apiClient.InitializeLocal();

        // Assert
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == "Iniciando configuracion hacia Consul: serviceName"),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>) It.IsAny<object>()),
            Times.Once);
    }

    private class TestApiClient : ApiClient
    {
        public TestApiClient(
            IHttpClientFactory httpClient,
            IOptions<Rootobject> configuration,
            string nameService,
            ILogger<ApiClient> logger) : base(httpClient, configuration, nameService, logger)
        {
        }
    }
}