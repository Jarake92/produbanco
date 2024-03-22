namespace shared.comun.hetoas;

public class ShapedEntity
{
    public Guid Id { get; set; }
    public Entity Entity { get; set; } = new();
}