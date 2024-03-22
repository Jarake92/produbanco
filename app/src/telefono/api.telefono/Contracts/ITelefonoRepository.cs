using api.telefono.Models;
using shared.comun.EntityFramework;

namespace api.telefono.Contracts;

public interface ITelefonoRepository : IGenericRepository<Telefono>
{
    Task<Telefono?> GetTelefonoById(Guid id);
    Task<Telefono> AddTelefono(Telefono telefono);
    Task<Telefono> UpdateTelefono(Telefono telefono);
    Task<bool> EliminarTelefono(Guid id);
    Task<IList<Telefono>> ObtenerTelefonosCliente(Guid idCliente);
}