namespace api.telefono.Models;

public record TelefonoRequest(
    Guid IdCliente,
    string Numero,
    TipoTelefono Tipo,
    Operadora Operadora);