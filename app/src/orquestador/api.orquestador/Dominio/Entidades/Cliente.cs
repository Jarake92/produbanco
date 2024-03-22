namespace api.orquestador.Dominio.Entidades;

public class Cliente
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public DateTime DateBirth { get; set; }

    internal static Cliente DefaultInstance => new();
}