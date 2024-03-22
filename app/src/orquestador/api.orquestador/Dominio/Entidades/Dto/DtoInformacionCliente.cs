namespace api.orquestador.Dominio.Entidades.Dto;

public record class DtoInformacionCliente
{
    private readonly Cliente _cliente;
    private readonly IEnumerable<Direccion> _direcciones;
    private readonly IEnumerable<Telefono> _telefonos;

    public DtoInformacionCliente(Cliente cliente, IEnumerable<Direccion> direcciones,
        IEnumerable<Telefono> telefonos)
    {
        _cliente = cliente ?? Cliente.DefaultInstance;
        _direcciones = direcciones == null || !direcciones.Any() ? new List<Direccion>() : direcciones;
        _telefonos = telefonos == null || !telefonos.Any() ? new List<Telefono>() : telefonos;
    }

    public Cliente cliente => _cliente;
    public IReadOnlyCollection<Telefono> telefonos => _telefonos.ToList().AsReadOnly();
    public IReadOnlyCollection<Direccion> direcciones => _direcciones.ToList().AsReadOnly();
}