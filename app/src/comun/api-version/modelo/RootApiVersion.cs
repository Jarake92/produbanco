using Cons = shared.comun.api_version.constantes.ApiVersionConstantes;

namespace shared.comun.api_version.modelo;

public class RootApiVersion
{
    public Apiversion ApiVersion { get; set; } = new();
}

public class Apiversion
{
    public bool AsumirVersionVigente { get; set; } = Cons.ValoresDefecto.VALOR_ASUMIR_VERSION_VIGENTE;
    public int[] VersionVigente { get; set; } = Cons.ValoresDefecto.VALOR_VERSION_VIGENTE;
    public bool ReportarVersion { get; set; } = Cons.ValoresDefecto.VALOR_REPORTAR_VERSION;
    public string NombreVersion { get; set; } = Cons.ValoresDefecto.VALOR_NOMBRE_VERSION;
}