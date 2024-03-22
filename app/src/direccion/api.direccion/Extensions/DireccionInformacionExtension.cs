using api.direccion.Contracts;
using api.direccion.Models;
using api.direccion.Services;
using shared.comun.hetoas.extensions;

namespace api.direccion.Extensions;

public static class DireccionInformacionExtension
{
    public static void PersistenciaMemoria(this IServiceCollection services)
    {
        services.AddScoped<IDireccionesRepository, DireccionRepository>();
        services.AddSingleton(typeof(IDireccionHelper), typeof(DireccionHelper));
        services.AddSingleton(typeof(ISortHelper<Direccion>), typeof(SortHelper<Direccion>));
        services.AddSingleton(typeof(IDataShaper<Direccion>), typeof(DataShaper<Direccion>));
    }
}