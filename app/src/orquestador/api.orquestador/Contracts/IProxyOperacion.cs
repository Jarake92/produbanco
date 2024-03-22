using shared.comun.hetoas;

namespace api.orquestador.Contracts;

public interface IProxyOperacion<in T> where T : class
{
    Task InitializeLocal();
    Task<(string, string?)> Obtener(string? parameters);
    Task<string> ObtenerById(Guid id);
    Task<string> PostAsync(T value);

    Task<bool> DeleteAsync(Guid id);
}