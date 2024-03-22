using api.orquestador.Contracts;
using api.orquestador.Dominio.Entidades;
using api.orquestador.Proxys;
using api.orquestador.Servicios;
using shared.comun.Authentication.HttpMessageHandlers;
using shared.comun.proxy.constantes;
using shared.comun.proxy.modelo;
using shared.comun.Telemetry.HttpMessageHandlers;

namespace api.orquestador.Extensions;

public static class ConexionManagerExtension
{
    public static void ServiciosProxy(this IServiceCollection services)
    {
        services.AddTransient<RequestStatusLoggingHandler>();
        services.AddScoped<BearerTokenHandler>();

        services.AddSingleton<IProxyOperacion<Cliente>, ProxyCliente>();
        services.AddSingleton<IProxyListadoOperacion<Telefono>, ProxyTelefono>();
        services.AddSingleton<IProxyListadoOperacion<Direccion>, ProxyDireccion>();
        services.AddSingleton<IClienteManager, ClienteManager>();
    }

    public static void ConexionHttpClienteMicros(this IServiceCollection services, Rootobject configurationSection)
    {
        services.AddHttpClient<ProxyCliente>(ProxyConstantes.NombresHttpClient.Cliente,
                cliente =>
                {
                    ConfiguracionInicialProxy(configurationSection, cliente, ProxyConstantes.NombresHttpClient.Cliente);
                })
            .AddHttpMessageHandler<RequestStatusLoggingHandler>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ProxyTelefono>(ProxyConstantes.NombresHttpClient.Telefono,
                telefono =>
                {
                    ConfiguracionInicialProxy(configurationSection, telefono,
                        ProxyConstantes.NombresHttpClient.Telefono);
                })
            .AddHttpMessageHandler<RequestStatusLoggingHandler>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ProxyDireccion>(ProxyConstantes.NombresHttpClient.Direccion,
                direccion =>
                {
                    ConfiguracionInicialProxy(configurationSection, direccion,
                        ProxyConstantes.NombresHttpClient.Direccion);
                })
            .AddHttpMessageHandler<RequestStatusLoggingHandler>()
            .AddHttpMessageHandler<BearerTokenHandler>();
    }

    private static void ConfiguracionInicialProxy(Rootobject configurationSection, HttpClient cliente, string proxy)
    {
        var configurationCliente =
            configurationSection.ConfiguracionClient?.FirstOrDefault(x => x.Nombre == proxy);
        var timeOut = configurationCliente?.Timeout < 1
            ? ProxyConstantes.General.TimeOutGenerico
            : configurationCliente?.Timeout;

        if (configurationCliente == null)
        {
            return;
        }

        cliente.BaseAddress = new Uri(configurationCliente.Url);
        cliente.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeOut));
    }
}