using shared.comun.hetoas;

namespace api.telefono.Models;

public class Telefono : IEntity
{
    public Guid Id { get; set; }
    public Guid IdCliente { get; set; }
    public string Numero { get; set; } = string.Empty;
    public TipoTelefono Tipo { get; set; }
    public Operadora Operadora { get; set; }
}