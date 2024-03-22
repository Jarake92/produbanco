using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shared.comun.configuracion;
using ConsConsul = shared.comun.service_discovery.Constantes;

namespace shared.comun.service_discovery.consul.server;

public static class ConsulExtensiones
{
    public static void ConfigurarConsul(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
        {
            var address = configuration[$"{ConfiguracionSeccion.SeccionConsul}:{ConsConsul.EntrasConfig.AddressConsul}"];
            if(!string.IsNullOrWhiteSpace(address))
                consulConfig.Address = new Uri(address);
        }));
    }
}