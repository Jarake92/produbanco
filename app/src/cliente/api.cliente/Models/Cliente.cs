using shared.comun.hetoas;

namespace api.cliente.Models;

public class Cliente : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateBirth { get; set; }

    public static Cliente Default => new();
}