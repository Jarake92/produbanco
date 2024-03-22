namespace BFF.Identidad.Dominio.Models;

public class OpenIddictSettings
{
    public string Issuer { get; set; }
    public string Sitio { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string EncryptionKey { get; set; }
    public string EncryptionCertificateThumbprint { get; set; }
    public string SigningCertificateThumbprint { get; set; }
    public string AccessTokenLifetimeMinutos { get; set; }
}