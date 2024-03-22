namespace api.orquestador.Dominio.Entidades.Dto;

public class Informacion<T> where T : class
{
    public T[] value { get; set; } = Array.Empty<T>();

    public static T? DefaultInstance => default;
}