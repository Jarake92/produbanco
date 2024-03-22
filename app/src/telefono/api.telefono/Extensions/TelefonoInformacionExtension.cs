using api.telefono.Contracts;
using api.telefono.Models;
using api.telefono.Services;
using shared.comun.hetoas.extensions;

namespace api.telefono.Extensions;

public static class TelefonoInformacionExtension
{
    public static void PersistenciaMemoria(this IServiceCollection services)
    {
        services.AddScoped<ITelefonoRepository, TelefonoRepository>();
        services.AddSingleton(typeof(ITelefonoHelper), typeof(TelefonoHelper));
        services.AddSingleton(typeof(ISortHelper<Telefono>), typeof(SortHelper<Telefono>));
        services.AddSingleton(typeof(IDataShaper<Telefono>), typeof(DataShaper<Telefono>));
    }
}