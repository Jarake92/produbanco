using api.orquestador.Contracts;
using api.orquestador.Dominio.Entidades;
using api.orquestador.Dominio.Entidades.Requests;
using Microsoft.AspNetCore.Mvc;

namespace api.orquestador.Controllers;

[ApiVersion("1.0")]
public class OrquestacionesClienteController : OrquestacionBase
{
    public OrquestacionesClienteController(IClienteManager clienteManager) : base(clienteManager)
    {
    }

    [HttpPost]
    [ActionName("guardar")]
    public async Task<IActionResult> GuadarCliente(CreateClienteRequest request)
    {
        var informacionCliente = new AlmacenarCliente
        {
            Cliente = new Cliente
            {
                Name = request.Name,
                LastName = request.LastName,
                DateBirth = request.DateBirth
            },
            Direccion = new Direccion
            {
                Provincia = request.Provincia,
                Canton = request.Canton,
                CallePrincipal = request.CallePrincipal
            },
            Telefono = new Telefono
            {
                Numero = request.Numero,
                Tipo = request.Tipo,
                Operadora = request.Operadora
            }
        };

        return Ok(await ClienteManager.CrearCliente(informacionCliente));
    }

    [HttpGet]
    [ActionName("obtener/{id-cliente}")]
    [Route("{idCliente:guid}")]
    public async Task<IActionResult> ObtenerInformacionCliente(Guid idCliente)
    {
        return Ok(await ClienteManager.ObtenerCliente(idCliente));
    }

    [HttpGet]
    [ActionName("obtener")]
    public async Task<IActionResult> ObtenerInformacionClientes()
    {
        var result = await ClienteManager.ObtenerClientes(Request.QueryString.Value);

        Response.Headers.Add("X-Pagination", result.PaginationHeader);

        return Ok(result.Items);
    }

    [HttpDelete]
    [ActionName("borrar/{id-cliente}")]
    [Route("{idCliente:guid}")]
    public async Task<IActionResult> BorrarCliente(Guid idCliente)
    {
        await ClienteManager.BorrarCliente(idCliente);
        return NoContent();
    }
}