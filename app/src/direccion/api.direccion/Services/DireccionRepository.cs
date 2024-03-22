using api.direccion.Contracts;
using api.direccion.Database;
using api.direccion.Models;
using Microsoft.EntityFrameworkCore;
using shared.comun.EntityFramework;

namespace api.direccion.Services;

public class DireccionRepository : GenericRepository<Direccion, DireccionesDbContext>, IDireccionesRepository
{
    public DireccionRepository(DireccionesDbContext context) : base(context)
    {
    }

    public async Task<Direccion?> GetDireccionById(Guid id)
    {
        return await FindByCondition(direccion => direccion.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Direccion> AddDireccion(Direccion direccion)
    {
        direccion.Id = Guid.NewGuid();
        await Create(direccion);

        return direccion;
    }
    
    public async Task<Direccion> UpdateDireccion(Direccion direccion)
    {
        await Update(direccion);
        return direccion;
    }

    public async Task<bool> DeleteDireccion(Guid id)
    {
        var direccion = await GetDireccionById(id);
        if (direccion is null) return false;

        await Delete(direccion);
        return true;
    }

    public async Task<IList<Direccion>> ObtenerDireccionesCliente(Guid id)
    {
        return await FindByCondition(x => x.IdCliente == id).ToListAsync();
    }
}