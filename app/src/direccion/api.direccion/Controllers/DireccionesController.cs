using api.direccion.Models;
using Microsoft.AspNetCore.Mvc;
using api.direccion.Contracts;
using shared.comun.hetoas;
using shared.comun.hetoas.link;
using Newtonsoft.Json;
using shared.comun;

namespace api.direccion.controllers;

[ApiVersion("1.0")]
public class DireccionesController : ApiBaseController
{
    private readonly IDireccionHelper _direccionHelper;
    private readonly IDireccionesRepository _direccionesRepository;
    private const string SuffixAction = "Direccion";

    public DireccionesController(
        IDireccionHelper clienteHelper,
        IDireccionesRepository direccionesRepository,
        LinkGenerator linkGenerator
    )
        : base(linkGenerator)
    {
        _direccionHelper = clienteHelper;
        _direccionesRepository = direccionesRepository;
    }

    [HttpGet]
    [Route("all")]
    public ActionResult DireccionAll([FromQuery] DireccionParameters direccionParameters)
    {
        var direcciones = _direccionesRepository.FindAll();
        var informationDireccion = _direccionHelper.GetFilteredEntities(direcciones, direccionParameters);
        var resultDireccion = _direccionHelper.GetShapedEntities(informationDireccion, direccionParameters);
        var shaped = resultDireccion.Select(o => o.Entity).ToList();
        var direccionWrapper = new LinkCollectionWrapper<Entity>(shaped);

        Response
            .Headers
            .Add("X-Pagination", JsonConvert.SerializeObject(_direccionHelper.GetPagination(resultDireccion)));

        for (var index = 0; index < resultDireccion.Count; index++)
        {
            var direccionLink =
                CreateLinksForEntity(resultDireccion[index].Id, direccionParameters.Fields, SuffixAction);
            shaped[index].Add("Links", direccionLink);
        }

        return Ok(CreateLinksForEntities(direccionWrapper, SuffixAction));
    }

    [HttpGet]
    [Route("cliente/{id:guid}")]
    public async Task<IActionResult> DireccionesByIdCliente(Guid id)
    {
        var direcciones = await _direccionesRepository.ObtenerDireccionesCliente(id);
        var resultDireccion =
            _direccionHelper.GetShapedEntities(direcciones, DireccionParameters.ByIdCliente(id.ToString()));

        var shaped = resultDireccion.Select(o => o.Entity).ToList();
        var direccionWrapper = new LinkCollectionWrapper<Entity>(shaped);

        for (var index = 0; index < resultDireccion.Count; index++)
        {
            var direccionLink =
                CreateLinksForEntity(resultDireccion[index].Id, new DireccionParameters().Fields, SuffixAction);
            shaped[index].Add("Links", direccionLink);
        }

        return Ok(CreateLinksForEntities(direccionWrapper, SuffixAction));
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> DireccionById(Guid id)
    {
        var direccion = await _direccionesRepository.GetDireccionById(id);

        return direccion is null ? NotFound("Direccion no encontrada.") : GetShapedDireccion(direccion);
    }

    [HttpPost]
    public async Task<IActionResult> DireccionSave(DireccionRequest request)
    {
        var newDireccion = new Direccion
        {
            IdCliente = request.IdCliente,
            Provincia = request.Provincia,
            Canton = request.Canton,
            CallePrincipal = request.CallePrincipal
        };
        var direccion = await _direccionesRepository.AddDireccion(newDireccion);

        return GetShapedDireccion(direccion);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> DireccionUpdate(Guid id, DireccionRequest request)
    {
        var direccion = await _direccionesRepository.GetDireccionById(id);

        if (direccion is null)
        {
            return NotFound("Direccion no encontrada.");
        }

        direccion.IdCliente = request.IdCliente;
        direccion.Provincia = request.Provincia;
        direccion.Canton = request.Canton;
        direccion.CallePrincipal = request.CallePrincipal;

        var updatedDireccion = await _direccionesRepository.UpdateDireccion(direccion);

        return GetShapedDireccion(updatedDireccion);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DireccionDelete(Guid id)
    {
        await _direccionesRepository.DeleteDireccion(id);
        return NoContent();
    }
    
    [HttpDelete]
    [Route("cliente/{id:guid}")]
    public async Task<IActionResult> TelefonoDeleteByIdCliente(Guid id)
    {
        var direcciones = await _direccionesRepository.ObtenerDireccionesCliente(id);
        await _direccionesRepository.DeleteMany(direcciones);
        return NoContent();
    }

    private IActionResult GetShapedDireccion(Direccion direccion)
    {
        var defaultParams = DireccionParameters.Default(direccion.Id.ToString());

        var shapedDireccion = _direccionHelper.GetShapedEntity(direccion, defaultParams);
        shapedDireccion.Entity.Add("Links", CreateLinksForEntity(
            direccion.Id,
            string.Empty,
            SuffixAction));

        return Ok(shapedDireccion.Entity);
    }
}