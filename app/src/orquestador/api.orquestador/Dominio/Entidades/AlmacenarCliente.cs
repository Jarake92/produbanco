namespace api.orquestador.Dominio.Entidades;

public class AlmacenarCliente
{
    private Cliente _cliente = Cliente.DefaultInstance;
    private Telefono _telefono = Telefono.DefaultInstance;
    private Direccion _direccion = Direccion.DefaultInstance;

    public Cliente Cliente
    {
        get => _cliente;
        set => _cliente = value;
    }

    public Telefono Telefono
    {
        get => _telefono;
        set => _telefono = value;
    }

    public Direccion Direccion
    {
        get => _direccion;
        set => _direccion = value;
    }
}