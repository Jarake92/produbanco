﻿using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace shared.comun.service_discovery.consul.server;

public class ConsulHostedService : IHostedService
{
    private CancellationTokenSource? _cts;
    private readonly IConsulClient _consulClient;
    private readonly IOptions<ConsulConfigModel> _consulConfig;
    private readonly ILogger<ConsulHostedService> _logger;
    private readonly IConfiguration _configuration;
    private string? _registrationID;

    public ConsulHostedService(IConsulClient consulClient, IOptions<ConsulConfigModel> consulConfig,
        ILogger<ConsulHostedService> logger, IConfiguration configuration)
    {
        _configuration = configuration;
        _logger = logger;
        _consulConfig = consulConfig;
        _consulClient = consulClient;

    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Create a linked token so we can trigger cancellation outside of this token's cancellation
        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        var address = _configuration.GetValue<string>(Constantes.ValoresDefecto.VariableEntorno)
                                        .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                            .First(x => x.StartsWith(Constantes.ValoresDefecto.EsquemaDefecto)); //addresses.Addresses.First();

        var uri = new Uri(address);
        _registrationID = $"{_consulConfig.Value.ServiceID}-{uri.Port}";

        var registration = new AgentServiceRegistration()
        {
            ID = _registrationID,
            Name = _consulConfig.Value.ServiceName,
            Address = $"{uri.Scheme}://{uri.Host}",
            Port = uri.Port,
            Tags = _consulConfig.Value.Tags
        };

        _logger.LogInformation($"Registering in Consul ID: {registration.ID} - direccion: {address}");
        await _consulClient.Agent.ServiceDeregister(registration.ID, _cts.Token);
        await _consulClient.Agent.ServiceRegister(registration, _cts.Token);

    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _cts?.Cancel();
        _logger.LogInformation($"Deregistering from Consul register id: {_registrationID}");
        try
        {
            await _consulClient.Agent.ServiceDeregister(_registrationID, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Deregisteration failed");
        }
    }
}
