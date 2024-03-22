using System.Text.Json.Serialization;

namespace shared.comun.service_discovery.consul.server;

public class ConsulConfigModel
{
    public string Address { get; set; } = string.Empty;
    public string ServiceName { get; set; } = Constantes.ValoresDefecto.ServiceNameDefecto;
    public string ServiceID { get; set; } = string.Empty;
    public string[] Tags { get; set; } = Array.Empty<string>();
}

public class ConsulConfigClientModel
{
    [JsonPropertyName("consulConfig")]
    public Consulconfig consulConfig { get; set; } = new();
}

public class Consulconfig
{
    [JsonPropertyName("address")]
    public string address { get; set; } = string.Empty;

    [JsonPropertyName("serviceName")]
    public string serviceName { get; set; } = string.Empty;

    [JsonPropertyName("services-api")]
    public Servicio[] services { get; set; } = Array.Empty<Servicio>();
}

public class Servicio
{
    [JsonPropertyName("name")]
    public string name { get; set; } = string.Empty;

    [JsonPropertyName("tag")]
    public string tag { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string url { get; set; } = string.Empty;
}


//public class Rootobject
//{
//    public Consulconfig consulConfig { get; set; }
//}

//public class Consulconfig
//{
//    public string address { get; set; }
//    public string serviceName { get; set; }
//    public ServicesApi[] servicesapi { get; set; }
//}

//public class ServicesApi
//{
//    public string name { get; set; }
//    public string tag { get; set; }
//}