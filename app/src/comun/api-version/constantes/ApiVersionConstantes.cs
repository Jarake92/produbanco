namespace shared.comun.api_version.constantes;

internal class ApiVersionConstantes
{
    internal class ConfiguracionGeneral
    {
        public const string VERSION_VIGENTE = "VersionVigente";
        public const string REPORTAR_VERSION = "ReportarVersion";
        public const string ASUMIR_VERSION_VIGENTE = "AsumirVersionVigente";
        public const string NOMBRE_VERSION = "NombreVersion";
    }

    internal class ValoresDefecto
    {
        public static int[] VALOR_VERSION_VIGENTE = { 1, 0 };
        public const bool VALOR_REPORTAR_VERSION = true;
        public const bool VALOR_ASUMIR_VERSION_VIGENTE = true;
        public const string VALOR_NOMBRE_VERSION = "version";
    }
}