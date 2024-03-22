namespace api.orquestador.Proxys;

public enum Accion
{
    CrearCliente,
    CrearDireccion,
    CrearTelefono,
    CompensarCliente,
    CompensarDireccion,
    BorrarCliente,
    BorrarDirecciones,
    BorrarTelefonos,
    CancelarCreacionCliente,
}

public enum AccionObtenerInformacion
{
    Cliente,
    Clientes,
    Direccion,
    Telefono,
    Mapear,
    //Fin,
    Error
}

public enum Estado
{
    SinIniciar,
    ClienteCreado,
    ClienteError,
    ClienteNoCreado,
    ClienteBorrado,
    ClienteCompensado,
    DireccionCreado,
    DireccionError,
    DireccionCompensada,
    DireccionesBorradas,
    TelefonoCreado,
    TelefonoError,
    TelefonoCompensado,
    TelefonosBorrados,
}

public enum EstadoObtenerInformacion
{
    SinIniciar,
    ClienteObtenido,
    ClientesObtenidos,
    DireccionObtenido,
    TelefonoObtenido,
    Fin,
    Error
}