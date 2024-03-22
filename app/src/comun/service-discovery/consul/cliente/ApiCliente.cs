using Consul;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using shared.comun.configuracion;
using shared.comun.service_discovery.consul.server;
using Polly.CircuitBreaker;
using Polly.Wrap;
using shared.comun.proxy.modelo;
using Policy = Polly.Policy;

namespace shared.comun.service_discovery.consul.cliente;

public abstract class ApiClient
{
    private const int ServerRetries = 3;
    private const int ServerCircuitBreakerRetries = 2;

    private readonly List<Uri> _serverUrls = new();

    private AsyncRetryPolicy? _serverRetryPolicy;
    private AsyncCircuitBreakerPolicy? _circuitBreakerPolicy;
    private AsyncPolicyWrap? _policyWrap;

    private int _currentConfigIndex;
    private readonly ILogger<ApiClient> _logger;
    private readonly HttpClient _httpClient;

    private readonly Configuraciones? _configurationDiscovery;

    private readonly string _nameService;
    private readonly string? _contentType;

    private IEnumerable<Uri> ServerUrls => _serverUrls;
    private int CurrentConfigIndex => _currentConfigIndex;

    protected HttpClient ClientHttp => _httpClient;
    protected Configuraciones ConfigurationDiscovery => _configurationDiscovery ?? new Configuraciones();
    protected string ContentTypeHttp => _contentType ?? string.Empty;

    protected AsyncPolicyWrap PolicyWrap =>
        _policyWrap ?? Policy.WrapAsync(CreateCircuitBreakerPolicy(), CreateRetryPolicy());

    protected ApiClient(
        IHttpClientFactory httpClient,
        IOptions<Rootobject> configuration,
        string nameService,
        ILogger<ApiClient> logger)
    {
        _nameService = nameService;
        _logger = logger;
        _httpClient = httpClient.CreateClient(nameService);
        _configurationDiscovery = configuration
            .Value
            .ConfiguracionClient?
            .FirstOrDefault(x => x.Nombre == nameService, new Configuraciones());
        _contentType = configuration.Value.ContentType;

        _logger.LogInformation($"Iniciando configuracion hacia Consul: {nameService}", nameService);
    }

    public async Task InitializeLocal()
    {
        _logger.LogInformation("Retry count set to {serverRetries}", ServerRetries);

        _circuitBreakerPolicy = CreateCircuitBreakerPolicy();
        _serverRetryPolicy = CreateRetryPolicy();

        _policyWrap = Policy.WrapAsync(_circuitBreakerPolicy, _serverRetryPolicy);

        await Task.CompletedTask;
    }

    public async Task Initialize()
    {
        var tagServicio = LoadConfiguracion<Consulconfig>.Instancia[ConfiguracionSeccion.SeccionConsul].services
            .FirstOrDefault(x => x.name == _nameService, new Servicio()).tag;
        var consulClient = new ConsulClient(c =>
        {
            var uri = new Uri(LoadConfiguracion<Consulconfig>.Instancia[ConfiguracionSeccion.SeccionConsul]
                .address);
            c.Address = uri;
        });

        _logger.LogInformation("Discovering Services from Consul.");

        var services = await consulClient.Agent.Services();

        _serverUrls.AddRange(from service in services.Response
            where service.Value.Tags.Any(t => t == tagServicio)
            let serviceUri = new Uri($"{service.Value.Address}:{service.Value.Port}")
            select serviceUri);

        _logger.LogInformation("{count} endpoints found.", _serverUrls.Count);

        var retries = _serverUrls.Count * 2 - 1;
        _logger.LogInformation("Retry count set to {retries}", retries);

        _serverRetryPolicy = Policy.Handle<HttpRequestException>()
            .RetryAsync(retries, (_, retrycount) => { ChooseNextServer(retrycount); });

        _httpClient.BaseAddress = (Uri?)ServerUrls.ElementAt(CurrentConfigIndex);
    }

    private void ChooseNextServer(int retryCount)
    {
        if (retryCount % 2 != 0) return;

        _logger.LogWarning("Trying next server...");
        _currentConfigIndex++;

        if (_currentConfigIndex > _serverUrls.Count - 1)
            _currentConfigIndex = 0;
    }

    private AsyncRetryPolicy CreateRetryPolicy()
    {
        return Policy.Handle<HttpRequestException>()
            .RetryAsync(ServerRetries, (_, retrycount) => { _logger.LogError(message: $"Attemp: {retrycount}"); });
    }

    private AsyncCircuitBreakerPolicy CreateCircuitBreakerPolicy()
    {
        return Policy.Handle<HttpRequestException>()
            .CircuitBreakerAsync(ServerCircuitBreakerRetries, TimeSpan.FromSeconds(30),
                onBreak: (_, breakDelay) => _logger.LogError("Circuit broken: {seconds}s", breakDelay.TotalSeconds),
                onReset: () => _logger.LogInformation("Circuit reset")
            );
    }
}