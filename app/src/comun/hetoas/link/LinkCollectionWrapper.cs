namespace shared.comun.hetoas.link;

public class LinkCollectionWrapper<T> : LinkResourceBase
{
    public List<T> Value { get; set; }

    public LinkCollectionWrapper(List<T> value)
    {
        Value = value;
    }
}