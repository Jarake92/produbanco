namespace api.orquestador.Dominio.Entidades;

public class Direccion
{
    public string Id { get; set; }
    public string IdCliente { get; set; }
    public string? Provincia { get; set; }
    public string? Canton { get; set; }
    public string? CallePrincipal { get; set; }

    public static Direccion DefaultInstance => new ();
}