using System.Text.Json.Nodes;
using api.orquestador.Contracts;
using api.orquestador.Dominio.Entidades;
using api.orquestador.Dominio.Entidades.Responses;
using api.orquestador.Proxys;
using Stateless;

namespace api.orquestador.Servicios;

public class ClienteManager : IClienteManager
{
    private readonly IProxyOperacion<Cliente> _proxyCliente;
    private readonly IProxyListadoOperacion<Direccion> _proxyDireccion;
    private readonly IProxyListadoOperacion<Telefono> _proxyTelefono;
    private readonly ILogger<ClienteManager> _logger;

    public ClienteManager(
        IProxyOperacion<Cliente> proxyCliente,
        IProxyListadoOperacion<Direccion> proxyDireccion,
        IProxyListadoOperacion<Telefono> proxyTelefono,
        ILogger<ClienteManager> logger)
    {
        _proxyCliente = proxyCliente;
        _proxyDireccion = proxyDireccion;
        _proxyTelefono = proxyTelefono;
        _logger = logger;

        Inicializar();
    }

    private void Inicializar()
    {
        Task.WhenAll(
            _proxyCliente.InitializeLocal(),
            _proxyDireccion.InitializeLocal(),
            _proxyTelefono.InitializeLocal());
    }

    public async Task<Dictionary<string, object?>> CrearCliente(AlmacenarCliente informacionCliente)
    {
        var aux = string.Empty;
        var cliente = Cliente.DefaultInstance;
        var direccion = string.Empty;
        var telefono = string.Empty;

        await Task.FromResult(Estado.SinIniciar);

        var clienteStateMachine = new StateMachine<Estado, Accion>(Estado.SinIniciar);
        clienteStateMachine.SetTriggerParameters<int>(Accion.CrearCliente);

        #region Procesos Ok

        clienteStateMachine.Configure(Estado.SinIniciar) //configuracion de estado
            .PermitDynamic(Accion.CrearCliente, () => //nombre del evento
            {
                try
                {
                    aux = _proxyCliente.PostAsync(informacionCliente.Cliente).Result;
                    var obj = JsonNode.Parse(aux);
                    cliente = System.Text.Json.JsonSerializer.Deserialize<Cliente>(obj);

                    return Estado.ClienteCreado;
                }
                catch (Exception)
                {
                    _logger.LogError("Error al crear el cliente");
                    return Estado.ClienteError;
                }
            });

        clienteStateMachine.Configure(Estado.ClienteCreado) //configuracion de estado 
            .PermitDynamic(Accion.CrearDireccion, () => //nombre del evento 
            {
                try
                {
                    informacionCliente.Direccion.IdCliente = cliente.Id;
                    direccion = _proxyDireccion.PostAsync(informacionCliente.Direccion).Result;

                    return Estado.DireccionCreado;
                }
                catch (Exception)
                {
                    _logger.LogError("Error al crear la direccion");
                    return Estado.DireccionError;
                }
            })
            .OnEntryAsync(async () =>
                await clienteStateMachine.FireAsync(Accion.CrearDireccion)); //evento a ser disparado

        clienteStateMachine.Configure(Estado.DireccionCreado)
            .PermitDynamic(Accion.CrearTelefono, () =>
            {
                try
                {
                    informacionCliente.Telefono.IdCliente = cliente.Id;
                    telefono = _proxyTelefono.PostAsync(informacionCliente.Telefono).Result;

                    return Estado.TelefonoCreado;
                }
                catch (Exception)
                {
                    _logger.LogError("Error al crear el telefono");
                    return Estado.TelefonoError;
                }
            })
            .OnEntryAsync(async () => await clienteStateMachine.FireAsync(Accion.CrearTelefono));

        #endregion

        #region Compensaciones

        clienteStateMachine.Configure(Estado.ClienteError)
            .PermitDynamic(Accion.CancelarCreacionCliente, () =>
            {
                _logger.LogWarning("Creacion de cliente cancelada");
                return Estado.ClienteNoCreado;
            })
            .OnEntryAsync(async () => await clienteStateMachine.FireAsync(Accion.CancelarCreacionCliente));

        clienteStateMachine.Configure(Estado.DireccionError)
            .PermitDynamic(Accion.CompensarCliente, () =>
            {
                _proxyCliente.DeleteAsync(Guid.Parse(cliente.Id));

                _logger.LogWarning("Creacion de cliente compensada");
                return Estado.ClienteCompensado;
            }).OnEntryAsync(async () => await clienteStateMachine.FireAsync(Accion.CompensarCliente));

        clienteStateMachine.Configure(Estado.TelefonoError)
            .PermitDynamic(Accion.CompensarDireccion, () =>
            {
                _proxyCliente.DeleteAsync(Guid.Parse(cliente.Id));
                _proxyDireccion.DeleteByIdCliente(Guid.Parse(cliente.Id));

                _logger.LogWarning("Creacion de direccion compensada");
                return Estado.DireccionCompensada;
            })
            .OnEntryAsync(async () => await clienteStateMachine.FireAsync(Accion.CompensarDireccion));

        #endregion

        await clienteStateMachine.FireAsync(Accion.CrearCliente);

        if (clienteStateMachine.State
            is Estado.ClienteNoCreado
            or Estado.ClienteCompensado
            or Estado.DireccionCompensada)
        {
            return new Dictionary<string, object?>();
        }

        var obj = new Dictionary<string, object?>
        {
            { "cliente", string.IsNullOrEmpty(aux) ? null : JsonNode.Parse(aux) },
            { "direccion", string.IsNullOrEmpty(direccion) ? null : JsonNode.Parse(direccion ?? string.Empty) },
            { "telefono", string.IsNullOrEmpty(telefono) ? null : JsonNode.Parse(telefono) }
        };

        return obj;
    }

    public async Task BorrarCliente(Guid idCliente)
    {
        await Task.FromResult(Estado.SinIniciar);

        var clienteStateMachine = new StateMachine<Estado, Accion>(Estado.SinIniciar);
        clienteStateMachine.SetTriggerParameters<int>(Accion.BorrarCliente);

        clienteStateMachine.Configure(Estado.SinIniciar) //configuracion de estado
            .PermitDynamic(Accion.BorrarCliente, () => //nombre del evento
            {
                try
                {
                    _proxyCliente.DeleteAsync(idCliente);
                    return Estado.ClienteBorrado;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al borrar el cliente");
                    return Estado.ClienteError;
                }
            });

        clienteStateMachine.Configure(Estado.ClienteBorrado) //configuracion de estado
            .PermitDynamic(Accion.BorrarDirecciones, () => //nombre del evento
            {
                try
                {
                    _proxyDireccion.DeleteByIdCliente(idCliente);
                    return Estado.DireccionesBorradas;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al borrar las direcciones del cliente {Id}", idCliente);
                    return Estado.DireccionError;
                }
            })
            .OnEntryAsync(async () => await clienteStateMachine.FireAsync(Accion.BorrarDirecciones));

        clienteStateMachine.Configure(Estado.DireccionesBorradas) //configuracion de estado
            .PermitDynamic(Accion.BorrarTelefonos, () => //nombre del evento
            {
                try
                {
                    _proxyTelefono.DeleteByIdCliente(idCliente);
                    return Estado.TelefonosBorrados;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al borrar los telefonos del cliente {Id}", idCliente);
                    return Estado.TelefonoError;
                }
            })
            .OnEntryAsync(async () => await clienteStateMachine.FireAsync(Accion.BorrarTelefonos));

        await clienteStateMachine.FireAsync(Accion.BorrarCliente);
    }

    public async Task<Dictionary<string, object?>> ObtenerCliente(Guid idCliente)
    {
        var cliente = string.Empty;
        var direcciones = string.Empty;
        var telefonos = string.Empty;

        await Task.FromResult(EstadoObtenerInformacion.SinIniciar);

        var clienteStateMachine =
            new StateMachine<EstadoObtenerInformacion, AccionObtenerInformacion>(EstadoObtenerInformacion.SinIniciar);

        clienteStateMachine.Configure(EstadoObtenerInformacion.SinIniciar)
            .PermitDynamic(AccionObtenerInformacion.Cliente, () =>
            {
                try
                {
                    cliente = _proxyCliente.ObtenerById(idCliente).Result;
                    return EstadoObtenerInformacion.ClienteObtenido;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener el cliente");
                    return EstadoObtenerInformacion.Error;
                }
            })
            .OnEntryAsync(async () => await clienteStateMachine.FireAsync(AccionObtenerInformacion.Cliente));

        clienteStateMachine.Configure(EstadoObtenerInformacion.ClienteObtenido)
            .PermitDynamic(AccionObtenerInformacion.Direccion, () =>
            {
                try
                {
                    direcciones = _proxyDireccion.ObtenerPorIdCliente(idCliente).Result;
                    return EstadoObtenerInformacion.DireccionObtenido;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener la direccion");
                    return EstadoObtenerInformacion.Error;
                }
            })
            .OnEntryAsync(async () => await clienteStateMachine.FireAsync(AccionObtenerInformacion.Direccion));

        clienteStateMachine.Configure(EstadoObtenerInformacion.DireccionObtenido)
            .PermitDynamic(AccionObtenerInformacion.Telefono, () =>
            {
                try
                {
                    telefonos = _proxyTelefono.ObtenerPorIdCliente(idCliente).Result;
                    return EstadoObtenerInformacion.TelefonoObtenido;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener el telefono");
                    return EstadoObtenerInformacion.Error;
                }
            })
            .OnEntryAsync(async () => await clienteStateMachine.FireAsync(AccionObtenerInformacion.Telefono));

        if (clienteStateMachine.State is EstadoObtenerInformacion.Error)
        {
            return new Dictionary<string, object?>();
        }

        await clienteStateMachine.FireAsync(AccionObtenerInformacion.Cliente);

        return new Dictionary<string, object?>
        {
            { "cliente", string.IsNullOrEmpty(cliente) ? null : JsonNode.Parse(cliente) },
            { "direcciones", string.IsNullOrEmpty(direcciones) ? null : JsonNode.Parse(direcciones)?["value"] },
            { "telefonos", string.IsNullOrEmpty(telefonos) ? null : JsonNode.Parse(telefonos)?["value"] }
        };
    }

    public async Task<PaginatedItemsResponse> ObtenerClientes(string? parameters)
    {
        (string, string?) aux = (string.Empty, string.Empty);
        var clientes = string.Empty;

        await Task.FromResult(EstadoObtenerInformacion.SinIniciar);

        var clienteStateMachine =
            new StateMachine<EstadoObtenerInformacion, AccionObtenerInformacion>(EstadoObtenerInformacion.SinIniciar);

        clienteStateMachine.Configure(EstadoObtenerInformacion.SinIniciar)
            .PermitDynamic(AccionObtenerInformacion.Clientes, () =>
            {
                try
                {
                    aux = _proxyCliente.Obtener(parameters).Result;
                    clientes = aux.Item1;
                    return EstadoObtenerInformacion.ClientesObtenidos;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener el cliente");
                    return EstadoObtenerInformacion.Error;
                }
            })
            .OnEntryAsync(async () => await clienteStateMachine.FireAsync(AccionObtenerInformacion.Clientes));

        await clienteStateMachine.FireAsync(AccionObtenerInformacion.Clientes);

        return new PaginatedItemsResponse
        {
            PaginationHeader = aux.Item2,
            Items = new Dictionary<string, object?>
            {
                { "clientes", string.IsNullOrEmpty(clientes) ? null : JsonNode.Parse(clientes)?["value"] },
            }
        };
    }
}