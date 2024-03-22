namespace shared.comun.proxy.modelo;

public class Rootobject
{
    public string ContentType { get; set; } = string.Empty;
    public Configuraciones[]? ConfiguracionClient { get; set; }

    public Configuraciones this[string nombreProxy]
    {
        get
        {
            if (ConfiguracionClient == null) { return Configuraciones.DefaultInstance; }
            if (!ConfiguracionClient.Any(x => x.Nombre == nombreProxy)) { return Configuraciones.DefaultInstance; }

            return ConfiguracionClient.First(x => x.Nombre == nombreProxy);
        }
    }
}