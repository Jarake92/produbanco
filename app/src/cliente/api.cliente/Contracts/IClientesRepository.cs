using api.cliente.Models;
using shared.comun.EntityFramework;

namespace api.cliente.Contracts;

public interface IClienteRepository : IGenericRepository<Cliente>
{
    Task<Cliente?> GetClienteById(Guid id);
    Task<Cliente> AddCliente(Cliente cliente);
    Task<Cliente> UpdateCliente(Cliente cliente);
    Task<bool> DeleteCliente(Guid id);
}