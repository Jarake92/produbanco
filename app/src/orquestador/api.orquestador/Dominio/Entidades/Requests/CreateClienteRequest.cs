namespace api.orquestador.Dominio.Entidades.Requests;

public record CreateClienteRequest(
    string Name,
    string LastName,
    DateTime DateBirth,
    string Provincia,
    string Canton,
    string CallePrincipal,
    string Numero,
    TipoTelefono Tipo,
    Operadora Operadora);