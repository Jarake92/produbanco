namespace api.orquestador.Contracts;

public interface IProxyListadoOperacion<in T> : IProxyOperacion<T> where T : class
{
    Task<string> ObtenerPorIdCliente(Guid id);
    Task<bool> DeleteByIdCliente(Guid id);
}