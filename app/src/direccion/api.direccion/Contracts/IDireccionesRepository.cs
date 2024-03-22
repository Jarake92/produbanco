using api.direccion.Models;
using shared.comun.EntityFramework;

namespace api.direccion.Contracts;

public interface IDireccionesRepository : IGenericRepository<Direccion>
{
    Task<Direccion?> GetDireccionById(Guid id);
    Task<Direccion> AddDireccion(Direccion direccion);
    Task<Direccion> UpdateDireccion(Direccion direccion);
    Task<bool> DeleteDireccion(Guid id);
    Task<IList<Direccion>> ObtenerDireccionesCliente(Guid id);
}