using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using shared.comun.api_version.modelo;

namespace shared.comun.api_version.extensiones;

public static class VersionManagerExtension
{
    public static void VersionMicroservicio(this IServiceCollection services, RootApiVersion configurationSection)
    {
        services.AddApiVersioning(opt =>
        {
            opt.AssumeDefaultVersionWhenUnspecified = configurationSection.ApiVersion.AsumirVersionVigente;
            opt.DefaultApiVersion = new ApiVersion(configurationSection.ApiVersion.VersionVigente[0],
                configurationSection.ApiVersion.VersionVigente[1]);
            opt.ReportApiVersions = configurationSection.ApiVersion.ReportarVersion;
            opt.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader(configurationSection.ApiVersion.NombreVersion),
                new HeaderApiVersionReader(configurationSection.ApiVersion.NombreVersion),
                new MediaTypeApiVersionReader(configurationSection.ApiVersion.NombreVersion));
        });
    }
}