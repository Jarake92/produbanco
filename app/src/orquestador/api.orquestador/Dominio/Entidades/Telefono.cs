namespace api.orquestador.Dominio.Entidades;

public class Telefono
{
    public string Id { get; set; }
    public string IdCliente { get; set; }
    public string? Numero { get; set; }
    public TipoTelefono Tipo { get; set; }
    public Operadora Operadora { get; set; }

    internal static Telefono DefaultInstance => new();
}