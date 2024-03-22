namespace shared.comun.Authentication;

public class OpenIddictConfiguration
{
    public string Authority { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string EncryptionKey { get; set; } = default!;
}