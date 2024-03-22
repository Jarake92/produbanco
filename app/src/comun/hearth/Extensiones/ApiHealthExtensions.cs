using Microsoft.Extensions.DependencyInjection;
using shared.comun.hearth.Logica;
using shared.comun.hearth.Modelo;

namespace shared.comun.hearth.Extensiones;

public static class ApiHealthExtensions
{
    public static void AddCustomApiHealth(
        this IServiceCollection services,
        ApiHealthSettings apiHealthSettings, bool useInMemory = false)
    {
        services.AddHealthChecks()
            .AddSqlServer(
                name: $"{apiHealthSettings.NombreServicio} - DB",
                connectionString: apiHealthSettings.UrlBaseDatos,
                tags: new[] { "dependency" }).CheckOnlyWhen($"{apiHealthSettings.NombreServicio} - DB", !useInMemory)
            .AddCheck(
                apiHealthSettings.NombreServicio,
                new ApiHealth(apiHealthSettings.NombreServicio),
                tags: new[] { "api" });
    }
}