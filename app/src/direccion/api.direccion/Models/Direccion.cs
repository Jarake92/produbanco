using shared.comun.hetoas;

namespace api.direccion.Models;

public class Direccion : IEntity
{
    public Guid Id { get; set; }
    public Guid IdCliente { get; set; }
    public string Provincia { get; set; } = string.Empty;
    public string Canton { get; set; } = string.Empty;
    public string CallePrincipal { get; set; } = string.Empty;
}