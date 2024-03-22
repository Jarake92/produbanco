using api.cliente.Contracts;
using api.cliente.Models;
using api.cliente.Services;
using shared.comun.hetoas.extensions;

namespace api.cliente.Extensions;

public static class ClientesInformacionExtension
{
    public static void PersistenciaMemoria(this IServiceCollection services)
    {
        services.AddScoped<IClienteRepository, ClientesRepository>();
        services.AddSingleton(typeof(IClienteHelper), typeof(ClienteHelper));
        services.AddSingleton(typeof(ISortHelper<Cliente>), typeof(SortHelper<Cliente>));
        services.AddSingleton(typeof(IDataShaper<Cliente>), typeof(DataShaper<Cliente>));
    }
}