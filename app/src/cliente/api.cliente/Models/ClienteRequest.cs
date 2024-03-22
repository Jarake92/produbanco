namespace api.cliente.Models;

public record ClienteRequest(
    string Name,
    string LastName,
    DateTime DateBirth
);