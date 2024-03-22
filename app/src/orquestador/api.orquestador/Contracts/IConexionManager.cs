using api.orquestador.Dominio.Entidades;
using api.orquestador.Dominio.Entidades.Responses;

namespace api.orquestador.Contracts;

public interface IClienteManager
{
    Task<Dictionary<string, object?>> CrearCliente(AlmacenarCliente informacionCliente);
    Task<Dictionary<string, object?>> ObtenerCliente(Guid idCliente);
    Task<PaginatedItemsResponse> ObtenerClientes(string? parameters);
    Task BorrarCliente(Guid idCliente);
}