using Microsoft.Extensions.Configuration;

namespace shared.comun.configuracion;

public class LoadConfiguracion<T> where T : class
{
    private readonly IConfiguration _config;
    private static readonly Lazy<LoadConfiguracion<T>> instancia = new(() => new LoadConfiguracion<T>(), true);

    /// <summary>
    /// Instancias Singleton
    /// </summary>
    private LoadConfiguracion()
    {
        _config = ObtenerConfiguracionJson();
    }

    /// <summary>
    /// Instancia de trabajo tipo singleton
    /// </summary>
    public static LoadConfiguracion<T> Instancia => instancia.Value;

    /// <summary>
    /// Obtiene la informacion desde el archivo config
    /// </summary>
    /// <returns>IConfiguration</returns>
    private static IConfiguration ObtenerConfiguracionJson()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    /// <summary>
    /// Obtener la configuracion desde el archivo .json
    /// </summary>
    /// <param name="nombreConfig">Nombre del config a obtenert</param>
    /// <returns>ConexionRabbit</returns>
    public T this[string nombreConfig] => _config.GetRequiredSection(nombreConfig).Get<T>();
}