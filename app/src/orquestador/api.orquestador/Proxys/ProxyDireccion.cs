using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using shared.comun.proxy.constantes;
using shared.comun.service_discovery.consul.cliente;
using System.Text;
using api.orquestador.Contracts;
using api.orquestador.Dominio.Entidades;
using shared.comun.proxy.modelo;

namespace api.orquestador.Proxys;

public sealed class ProxyDireccion : ApiClient, IProxyListadoOperacion<Direccion>
{
    public ProxyDireccion(IHttpClientFactory httpClient, IOptions<Rootobject> configuration, ILogger<ApiClient> logger)
        : base(httpClient, configuration, ProxyConstantes.NombresHttpClient.Direccion, logger)
    {
    }

    public async Task<(string, string?)> Obtener(string? parameters)
    {
        return await PolicyWrap.ExecuteAsync(async () =>
        {
            var respuestaMicro = await ClientHttp.GetAsync($"{ConfigurationDiscovery.Path}{parameters}");
            respuestaMicro.EnsureSuccessStatusCode();

            return (await respuestaMicro.Content.ReadAsStringAsync(),
                respuestaMicro.Headers.GetValues("X-Pagination").FirstOrDefault());
        });
    }

    public async Task<string> ObtenerPorIdCliente(Guid id)
    {
        return await PolicyWrap.ExecuteAsync(async () =>
        {
            var respuestaMicro = await ClientHttp.GetAsync($"{ConfigurationDiscovery?.Path}cliente/{id}");
            respuestaMicro.EnsureSuccessStatusCode();

            return await respuestaMicro.Content.ReadAsStringAsync();
        });
    }

    public async Task<string> ObtenerById(Guid id)
    {
        return await PolicyWrap.ExecuteAsync(async () =>
        {
            var respuestaMicro = await ClientHttp.GetAsync($"{ConfigurationDiscovery.Path}{id}");
            respuestaMicro.EnsureSuccessStatusCode();

            return await respuestaMicro.Content.ReadAsStringAsync();
        });
    }

    public async Task<string> PostAsync(Direccion value)
    {
        return await PolicyWrap.ExecuteAsync(async () =>
        {
            var request = JsonConvert.SerializeObject(value);
            var respuestaMicro = await ClientHttp.PostAsync($"{ConfigurationDiscovery?.Path}",
                new StringContent(request, Encoding.UTF8, ContentTypeHttp));
            respuestaMicro.EnsureSuccessStatusCode();
            return await respuestaMicro.Content.ReadAsStringAsync();
        });
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await PolicyWrap.ExecuteAsync(async () =>
        {
            var respuestaMicro = await ClientHttp.DeleteAsync($"{ConfigurationDiscovery?.Path}{id}");
            respuestaMicro.EnsureSuccessStatusCode();
            
            return await respuestaMicro.Content.ReadFromJsonAsync<bool>();
        });
    }
    
    public async Task<bool> DeleteByIdCliente(Guid id)
    {
        return await PolicyWrap.ExecuteAsync(async () =>
        {
            var respuestaMicro = await ClientHttp.DeleteAsync($"{ConfigurationDiscovery.Path}cliente/{id}");
            respuestaMicro.EnsureSuccessStatusCode();
            
            return await respuestaMicro.Content.ReadFromJsonAsync<bool>();
        });
    }
}