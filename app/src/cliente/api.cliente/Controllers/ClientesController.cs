using api.cliente.Models;
using Microsoft.AspNetCore.Mvc;
using shared.comun.hetoas;
using Newtonsoft.Json;
using shared.comun.hetoas.link;
using api.cliente.Contracts;
using shared.comun;

namespace api.cliente.Controllers;

[ApiVersion("1.0")]
public class ClientesController : ApiBaseController
{
    private readonly IClienteHelper _clienteHelper;
    private readonly IClienteRepository _clienteRepository;
    private const string SuffixAction = "Cliente";

    public ClientesController(
        IClienteHelper clienteHelper,
        LinkGenerator linkGenerator,
        IClienteRepository clienteRepository
    )
        : base(linkGenerator)
    {
        _clienteHelper = clienteHelper;
        _clienteRepository = clienteRepository;
    }

    [HttpGet]
    public ActionResult ClienteAll([FromQuery] ClienteParameters clienteParameters)
    {
        var clientes = _clienteRepository.FindAll();
        var informationCliente = _clienteHelper.GetFilteredEntities(clientes, clienteParameters);
        var resultCliente = _clienteHelper.GetShapedEntities(informationCliente, clienteParameters);
        var shaped = resultCliente.Select(o => o.Entity).ToList();
        var clienteWrapper = new LinkCollectionWrapper<Entity>(shaped);

        Response
            .Headers
            .Add("X-Pagination", JsonConvert.SerializeObject(_clienteHelper.GetPagination(resultCliente)));

        for (var index = 0; index < resultCliente.Count; index++)
        {
            var clienteLinks =
                CreateLinksForEntity(resultCliente[index].Id, clienteParameters.Fields, SuffixAction);
            shaped[index].Add("Links", clienteLinks);
        }

        return Ok(CreateLinksForEntities(clienteWrapper, SuffixAction));
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> ClienteById(Guid id)
    {
        var cliente = await _clienteRepository.GetClienteById(id);

        return cliente is null ? NotFound("Cliente no encontrado.") : GetShapedCliente(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> ClienteSave(ClienteRequest request)
    {
        var newCliente = new Cliente
        {
            Name = request.Name,
            LastName = request.LastName,
            DateBirth = request.DateBirth
        };
        var cliente = await _clienteRepository.AddCliente(newCliente);

        return GetShapedCliente(cliente);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> ClienteUpdate(Guid id, ClienteRequest request)
    {
        var cliente = await _clienteRepository.GetClienteById(id);

        if (cliente is null)
        {
            return NotFound("Cliente no encontrado.");
        }

        cliente.Name = request.Name;
        cliente.LastName = request.LastName;
        cliente.DateBirth = request.DateBirth;

        var updatedCliente = await _clienteRepository.UpdateCliente(cliente);

        return GetShapedCliente(updatedCliente);
    }


    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> ClienteDelete(Guid id)
    {
        await _clienteRepository.DeleteCliente(id);
        return NoContent();
    }

    private IActionResult GetShapedCliente(Cliente cliente)
    {
        var defaultParams = ClienteParameters.Default(cliente.Id.ToString());

        var shapedCliente = _clienteHelper.GetShapedEntity(cliente, defaultParams);
        shapedCliente.Entity.Add("Links", CreateLinksForEntity(
            cliente.Id,
            string.Empty,
            SuffixAction));

        return Ok(shapedCliente.Entity);
    }
}