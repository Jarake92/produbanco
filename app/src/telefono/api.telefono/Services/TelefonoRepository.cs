using api.telefono.Contracts;
using api.telefono.Database;
using api.telefono.Models;
using Microsoft.EntityFrameworkCore;
using shared.comun.EntityFramework;

namespace api.telefono.Services;

public class TelefonoRepository : GenericRepository<Telefono, TelefonosDbContext>, ITelefonoRepository
{
    public TelefonoRepository(TelefonosDbContext context) : base(context)
    {
    }

    public async Task<Telefono?> GetTelefonoById(Guid id)
    {
        return await FindByCondition(telefono => telefono.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Telefono> AddTelefono(Telefono telefono)
    {
        telefono.Id = Guid.NewGuid();

        await Create(telefono);

        return telefono;
    }
    
    public async Task<Telefono> UpdateTelefono(Telefono telefono)
    {
        await Update(telefono);
        return telefono;
    }

    public async Task<bool> EliminarTelefono(Guid id)
    {
        var telefono = await GetTelefonoById(id);
        if (telefono is null) return false;

        await Delete(telefono);
        return true;
    }

    public async Task<IList<Telefono>> ObtenerTelefonosCliente(Guid idCliente)
    {
        return await FindByCondition(x => x.IdCliente == idCliente).ToListAsync();
    }
}