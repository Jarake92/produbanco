namespace api.orquestador.Dominio.Entidades.Responses;

public class PaginatedItemsResponse
{
    public Dictionary<string, object?> Items { get; set; } = new();
    public string PaginationHeader { get; set; } = string.Empty;
}