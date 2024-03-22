namespace api.direccion.Models;

public record DireccionRequest(
    Guid IdCliente,
    string Provincia,
    string Canton,
    string CallePrincipal);