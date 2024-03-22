using api.cliente.Contracts;
using api.cliente.Database;
using api.cliente.Models;
using Microsoft.EntityFrameworkCore;
using shared.comun.EntityFramework;

namespace api.cliente.Services;

public class ClientesRepository : GenericRepository<Cliente, ClientesDbContext>, IClienteRepository
{
    public ClientesRepository(ClientesDbContext context) : base(context)
    {
    }

    public async Task<Cliente?> GetClienteById(Guid id)
    {
        return await FindByCondition(cliente => cliente.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Cliente> AddCliente(Cliente cliente)
    {
        cliente.Id = Guid.NewGuid();
        await Create(cliente);

        return cliente;
    }

    public async Task<Cliente> UpdateCliente(Cliente cliente)
    {
        await Update(cliente);
        return cliente;
    }

    public async Task<bool> DeleteCliente(Guid id)
    {
        var cliente = await GetClienteById(id);
        if (cliente is null) return false;

        await Delete(cliente);
        return true;
    }
}