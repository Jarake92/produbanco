using System.Net;
using api.orquestador.Contracts;
using api.orquestador.Dominio.Entidades;
using api.orquestador.Servicios;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;

namespace api.orquestador.tests;

public class ConexionManagerTests
{
    private readonly Mock<IProxyOperacion<Cliente>> _proxyClienteMock = new();
    private readonly Mock<IProxyListadoOperacion<Direccion>> _proxyDireccionMock = new();
    private readonly Mock<IProxyListadoOperacion<Telefono>> _proxyTelefonoMock = new();
    private readonly Mock<ILogger<ClienteManager>> _loggerMock = new();

    private readonly Cliente _cliente = new()
    {
        Id = Guid.NewGuid().ToString(),
        Name = "John",
        LastName = "Doe",
        DateBirth = DateTime.Now.AddYears(-18)
    };

    private readonly Direccion _direccion = new()
    {
        CallePrincipal = "Lorem",
        Provincia = "Ipsum",
        Canton = "Dolor",
    };

    private readonly Telefono _telefono = new()
    {
        Numero = "123456789",
        Tipo = TipoTelefono.Celular,
        Operadora = Operadora.Claro
    };

    [Fact]
    public async Task CrearCliente_ReturnsNewCliente()
    {
        // Arrange
        var clienteResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(_cliente))
        };
        var direccionResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(_direccion))
        };
        var telefonoResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(_telefono))
        };

        _proxyClienteMock.Setup(x => x.PostAsync(_cliente))
            .ReturnsAsync(await clienteResponse.Content.ReadAsStringAsync());
        _proxyDireccionMock.Setup(x => x.PostAsync(_direccion))
            .ReturnsAsync(await direccionResponse.Content.ReadAsStringAsync());
        _proxyTelefonoMock.Setup(x => x.PostAsync(_telefono))
            .ReturnsAsync(await telefonoResponse.Content.ReadAsStringAsync());

        var conexionManager = new ClienteManager(
            _proxyClienteMock.Object,
            _proxyDireccionMock.Object,
            _proxyTelefonoMock.Object,
            _loggerMock.Object);

        var newClienteInfo = new AlmacenarCliente()
        {
            Cliente = _cliente,
            Direccion = _direccion,
            Telefono = _telefono
        };

        // Act
        var result = await conexionManager.CrearCliente(newClienteInfo);

        // Assert
        var cliente = JsonConvert.DeserializeObject<Cliente>(result["cliente"]?.ToString() ?? string.Empty);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(_cliente.Name, cliente?.Name);
    }

    [Fact]
    public async Task CrearCliente_ApiClientesError_CompensatesClientCreation()
    {
        // Arrange
        _proxyClienteMock.Setup(x => x.PostAsync(_cliente))
            .ThrowsAsync(new Exception("Error al crear el cliente"));

        var conexionManager = new ClienteManager(
            _proxyClienteMock.Object,
            _proxyDireccionMock.Object,
            _proxyTelefonoMock.Object,
            _loggerMock.Object);

        var newClienteInfo = new AlmacenarCliente()
        {
            Cliente = _cliente,
            Direccion = _direccion,
            Telefono = _telefono
        };

        // Act
        var result = await conexionManager.CrearCliente(newClienteInfo);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task CrearCliente_ApiDireccionesError_CompensatesClientCreation()
    {
        // Arrange
        var clienteResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(_cliente))
        };

        _proxyClienteMock.Setup(x => x.PostAsync(_cliente))
            .ReturnsAsync(await clienteResponse.Content.ReadAsStringAsync());
        _proxyDireccionMock.Setup(x => x.PostAsync(_direccion))
            .ThrowsAsync(new Exception("Error al crear la dirección"));

        var conexionManager = new ClienteManager(
            _proxyClienteMock.Object,
            _proxyDireccionMock.Object,
            _proxyTelefonoMock.Object,
            _loggerMock.Object);

        var newClienteInfo = new AlmacenarCliente()
        {
            Cliente = _cliente,
            Direccion = _direccion,
            Telefono = _telefono
        };

        // Act
        var result = await conexionManager.CrearCliente(newClienteInfo);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task CrearCliente_ApiTelefonosError_CompensatesClientCreation()
    {
        // Arrange
        var clienteResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(_cliente))
        };
        var direccionResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(_direccion))
        };

        _proxyClienteMock.Setup(x => x.PostAsync(_cliente))
            .ReturnsAsync(await clienteResponse.Content.ReadAsStringAsync());
        _proxyDireccionMock.Setup(x => x.PostAsync(_direccion))
            .ReturnsAsync(await direccionResponse.Content.ReadAsStringAsync());
        _proxyTelefonoMock.Setup(x => x.PostAsync(_telefono))
            .ThrowsAsync(new Exception("Error al crear el teléfono"));

        var conexionManager = new ClienteManager(
            _proxyClienteMock.Object,
            _proxyDireccionMock.Object,
            _proxyTelefonoMock.Object,
            _loggerMock.Object);

        var newClienteInfo = new AlmacenarCliente()
        {
            Cliente = _cliente,
            Direccion = _direccion,
            Telefono = _telefono
        };

        // Act
        var result = await conexionManager.CrearCliente(newClienteInfo);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task BorrarCliente_ReturnsDeletedCliente()
    {
        // Arrange
        _proxyClienteMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        _proxyDireccionMock.Setup(x => x.DeleteByIdCliente(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        _proxyTelefonoMock.Setup(x => x.DeleteByIdCliente(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var conexionManager = new ClienteManager(
            _proxyClienteMock.Object,
            _proxyDireccionMock.Object,
            _proxyTelefonoMock.Object,
            _loggerMock.Object);

        // Act
        await conexionManager.BorrarCliente(Guid.Parse(_cliente.Id));

        // Assert
        _proxyClienteMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        _proxyDireccionMock.Verify(x => x.DeleteByIdCliente(It.IsAny<Guid>()), Times.Once);
        _proxyTelefonoMock.Verify(x => x.DeleteByIdCliente(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task ObtenerCliente_ReturnsCliente()
    {
        // Arrange
        var clienteResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(_cliente))
        };
        var direccionResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(_direccion))
        };
        var telefonoResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(_telefono))
        };

        _proxyClienteMock.Setup(x => x.ObtenerById(It.IsAny<Guid>()))
            .ReturnsAsync(await clienteResponse.Content.ReadAsStringAsync());
        _proxyDireccionMock.Setup(x => x.ObtenerPorIdCliente(It.IsAny<Guid>()))
            .ReturnsAsync(await direccionResponse.Content.ReadAsStringAsync());
        _proxyTelefonoMock.Setup(x => x.ObtenerPorIdCliente(It.IsAny<Guid>()))
            .ReturnsAsync(await telefonoResponse.Content.ReadAsStringAsync());

        var conexionManager = new ClienteManager(
            _proxyClienteMock.Object,
            _proxyDireccionMock.Object,
            _proxyTelefonoMock.Object,
            _loggerMock.Object);

        // Act
        var result = await conexionManager.ObtenerCliente(Guid.Parse(_cliente.Id));

        // Assert
        var cliente = JsonConvert.DeserializeObject<Cliente>(result["cliente"]?.ToString() ?? string.Empty);
        Assert.NotNull(result);
        Assert.Equal(_cliente.Name, cliente?.Name);
    }

    [Fact]
    public async Task ObtenerClientes_ReturnsClientes()
    {
        // Arrange
        var clientesResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(
                new
                {
                    value = new List<Cliente> { _cliente }
                }))
        };

        _proxyClienteMock.Setup(x => x.Obtener(It.IsAny<string>()))
            .ReturnsAsync((await clientesResponse.Content.ReadAsStringAsync(), string.Empty));

        var conexionManager = new ClienteManager(
            _proxyClienteMock.Object,
            _proxyDireccionMock.Object,
            _proxyTelefonoMock.Object,
            _loggerMock.Object);

        // Act
        var result = await conexionManager.ObtenerClientes(string.Empty);

        // Assert
        var clientes =
            JsonConvert.DeserializeObject<List<Cliente>>(result.Items["clientes"]?.ToString() ?? string.Empty);
        Assert.NotNull(result);
        Assert.NotEmpty(clientes);
        Assert.Equal(_cliente.Name, clientes[0].Name);
    }
}