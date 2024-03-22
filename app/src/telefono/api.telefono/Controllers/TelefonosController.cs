using api.telefono.Contracts;
using api.telefono.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shared.comun;
using shared.comun.hetoas;
using shared.comun.hetoas.link;

namespace api.telefono.Controllers;

[ApiVersion("1.0")]
public class TelefonosController : ApiBaseController
{
    private readonly ITelefonoHelper _telefonoHelper;
    private readonly ITelefonoRepository _telefonoRepository;
    private const string SuffixAction = "Telefono";

    public TelefonosController(
        ITelefonoHelper clienteHelper,
        LinkGenerator linkGenerator,
        ITelefonoRepository telefonoRepository) : base(linkGenerator)
    {
        _telefonoHelper = clienteHelper;
        _telefonoRepository = telefonoRepository;
    }

    [HttpGet]
    [Route("all")]
    public ActionResult TelefonoAll([FromQuery] TelefonoParameters telefonoParameters)
    {
        var telefonos = _telefonoRepository.FindAll();
        var informationTelefonos = _telefonoHelper.GetFilteredEntities(telefonos, telefonoParameters);
        var resultTelefonos = _telefonoHelper.GetShapedEntities(informationTelefonos, telefonoParameters);
        var shaped = resultTelefonos.Select(o => o.Entity).ToList();
        var telefonosWrapper = new LinkCollectionWrapper<Entity>(shaped);

        Response
            .Headers
            .Add("X-Pagination", JsonConvert.SerializeObject(_telefonoHelper.GetPagination(resultTelefonos)));

        for (var index = 0; index < resultTelefonos.Count; index++)
        {
            var clienteLinks =
                CreateLinksForEntity(resultTelefonos[index].Id, telefonoParameters.Fields, SuffixAction);
            shaped[index].Add("Links", clienteLinks);
        }

        return Ok(CreateLinksForEntities(telefonosWrapper, SuffixAction));
    }

    [HttpGet]
    [Route("cliente/{id}")]
    public async Task<IActionResult> TelefonosByIdCliente(Guid id)
    {
        var telefonos = await _telefonoRepository.ObtenerTelefonosCliente(id);
        var resultTelefonos =
            _telefonoHelper.GetShapedEntities(telefonos, TelefonoParameters.ByIdCliente(id.ToString()));

        var shaped = resultTelefonos.Select(o => o.Entity).ToList();
        var telefonoWrapper = new LinkCollectionWrapper<Entity>(shaped);

        for (var index = 0; index < resultTelefonos.Count; index++)
        {
            var telefonoLink =
                CreateLinksForEntity(resultTelefonos[index].Id, new TelefonoParameters().Fields, SuffixAction);
            shaped[index].Add("Links", telefonoLink);
        }

        return Ok(CreateLinksForEntities(telefonoWrapper, SuffixAction));
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> TelefonoById(Guid id)
    {
        var telefono = await _telefonoRepository.GetTelefonoById(id);

        return telefono is null ? NotFound("Telefono no encontrado.") : GetShapedTelefono(telefono);    }

    [HttpPost]
    public async Task<IActionResult> TelefonoSave(TelefonoRequest request)
    {
        var newTelefono = new Telefono
        {
            IdCliente = request.IdCliente,
            Numero = request.Numero,
            Tipo = request.Tipo,
            Operadora = request.Operadora
        };
        var telefono = await _telefonoRepository.AddTelefono(newTelefono);

        return GetShapedTelefono(telefono);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> TelefonoUpdate(Guid id, TelefonoRequest request)
    {
        var telefono = await _telefonoRepository.GetTelefonoById(id);

        if (telefono is null)
        {
            return NotFound("Telefono no encontrado.");
        }

        telefono.IdCliente = request.IdCliente;
        telefono.Numero = request.Numero;
        telefono.Tipo = request.Tipo;
        telefono.Operadora = request.Operadora;

        var updatedTelefono = await _telefonoRepository.UpdateTelefono(telefono);

        return GetShapedTelefono(updatedTelefono);
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> TelefonoDelete(Guid id)
    {
        await _telefonoRepository.EliminarTelefono(id);
        return NoContent();
    }
    
    [HttpDelete]
    [Route("cliente/{id}")]
    public async Task<IActionResult> TelefonoDeleteByIdCliente(Guid id)
    {
        var telefonos = await _telefonoRepository.ObtenerTelefonosCliente(id);
        await _telefonoRepository.DeleteMany(telefonos);
        return NoContent();
    }

    private IActionResult GetShapedTelefono(Telefono telefono)
    {
        var defaultParams = TelefonoParameters.Default(telefono.Id.ToString());

        var shapedTelefono = _telefonoHelper.GetShapedEntity(telefono, defaultParams);
        shapedTelefono.Entity.Add("Links", CreateLinksForEntity(
            telefono.Id,
            string.Empty,
            SuffixAction));

        return Ok(shapedTelefono.Entity);
    }
}