using api.cliente.Contracts;
using api.cliente.Controllers;
using api.cliente.Models;
using api.controllers.specflow.Drivers;
using api.telefono.Contracts;
using api.telefono.Controllers;
using api.telefono.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using shared.comun.hetoas;
using shared.comun.hetoas.extensions;
using shared.comun.hetoas.link;
using TechTalk.SpecFlow.Assist;

namespace api.controllers.specflow.Steps;

[Binding]
public sealed class TelefonosControllerStepDefinitions
{
    private const string IdTelefono = "07f65539-57a4-46b0-a7ec-540bbda4d468";

    private readonly TelefonosController _telefonosController;
    private readonly Mock<ITelefonoRepository> _telefonoRepositoryMock = new();
    private readonly Mock<ITelefonoHelper> _telefonoHelperMock = new();
    private readonly Mock<LinkGenerator> _linkGeneratorMock = new();
    private readonly GenericDriver<Telefono> _genericDriver;
    private List<Telefono> _telefonos = new();

    private ActionResult<LinkCollectionWrapper<Telefono>>? _result;
    private IActionResult? _actionResult;

    public TelefonosControllerStepDefinitions(GenericDriver<Telefono> genericDriver)
    {
        _genericDriver = genericDriver;
        _telefonosController = new TelefonosController(
            _telefonoHelperMock.Object, _linkGeneratorMock.Object, _telefonoRepositoryMock.Object)
        {
            ControllerContext =
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Given("I have a list of telefonos")]
    public void GivenIHaveAListOfTelefonos()
    {
        _telefonos = new List<Telefono>
        {
            new()
            {
                Id = Guid.NewGuid(), Numero = "123456789", Operadora = Operadora.Claro, Tipo = TipoTelefono.Celular
            },
            new()
            {
                Id = Guid.NewGuid(), Numero = "987654321", Operadora = Operadora.Claro, Tipo = TipoTelefono.Celular
            }
        };

        _telefonoRepositoryMock.Setup(x => x.FindAll())
            .Returns(_telefonos.AsQueryable());
    }

    [When("I retrieve the list of telefonos")]
    public void WhenIRetrieveTheListOfTelefonos()
    {
        _telefonoHelperMock.Setup(x =>
                x.GetFilteredEntities(It.IsAny<IQueryable<Telefono>>(), It.IsAny<TelefonoParameters>()))
            .Returns(_telefonos);

        var shapedEntities = _genericDriver.GetShapedEntities(_telefonos, string.Empty);
        _telefonoHelperMock.Setup(x =>
                x.GetShapedEntities(It.IsAny<IEnumerable<Telefono>>(), It.IsAny<TelefonoParameters>()))
            .Returns(new PagedList<ShapedEntity>(shapedEntities, shapedEntities.Count, 1, 10));

        _result = _telefonosController.TelefonoAll(new TelefonoParameters());
    }

    [Then("the result should be a list of telefonos")]
    public void ThenTheResultShouldBeAListOfTelefonos()
    {
        var okResult = _result?.Result as OkObjectResult;
        var telefonoWrapper = okResult?.Value as LinkCollectionWrapper<Entity>;

        telefonoWrapper.Should().NotBeNull();
        telefonoWrapper?.Links.Should().NotBeNull();
        telefonoWrapper?.Value.Should().HaveCount(2);
    }

    [Given("I have a telefono with id (.*)")]
    public void GivenIHaveATelefonoWithId(Guid id)
    {
        var clienteId = Guid.Parse(IdTelefono);
        _telefonos = new List<Telefono>
        {
            new() { Id = clienteId, Numero = "123456789", Operadora = Operadora.Claro, Tipo = TipoTelefono.Celular },
            new()
            {
                Id = Guid.NewGuid(), Numero = "987654321", Operadora = Operadora.Claro, Tipo = TipoTelefono.Celular
            }
        };

        _telefonoRepositoryMock.Setup(x => x.GetTelefonoById(id))
            .ReturnsAsync(_telefonos.FirstOrDefault(x => x.Id == id));
    }

    [When("I retrieve the telefono with id (.*)")]
    public async Task WhenIRetrieveTheTelefonoWithId(Guid id)
    {
        _telefonoHelperMock.Setup(x => x.GetShapedEntity(It.IsAny<Telefono>(), It.IsAny<TelefonoParameters>()))
            .Returns(_genericDriver.GetShapedEntity(_telefonos.FirstOrDefault(x => x.Id == id), string.Empty));

        _actionResult = await _telefonosController.TelefonoById(id);
    }

    [Then("the result should be a telefono with id (.*)")]
    public void ThenTheResultShouldBeATelefonoWithId(Guid id)
    {
        var okResult = _actionResult as OkObjectResult;
        var entity = okResult?.Value as Entity;

        entity.Should().NotBeNull();
        entity?.Values.ToArray()[0].Should().Be(Guid.Parse(IdTelefono));
    }

    [When("I add a new telefono with the following data")]
    public async Task WhenIAddANewTelefonoWithTheFollowingData(Table table)
    {
        var telefonoRequest = table.CreateInstance<TelefonoRequest>();
        var telefono = new Telefono
        {
            Numero = telefonoRequest.Numero,
            Operadora = telefonoRequest.Operadora,
            Tipo = telefonoRequest.Tipo
        };

        _telefonoRepositoryMock.Setup(x => x.AddTelefono(It.IsAny<Telefono>()))
            .ReturnsAsync(telefono);

        _telefonoHelperMock.Setup(x => x.GetShapedEntity(It.IsAny<Telefono>(), It.IsAny<TelefonoParameters>()))
            .Returns(_genericDriver.GetShapedEntity(telefono, string.Empty));

        _actionResult = await _telefonosController.TelefonoSave(telefonoRequest);
    }

    [Then("the result should be a new telefono with the following data")]
    public void ThenTheResultShouldBeANewTelefonoWithTheFollowingData(Table table)
    {
        var okResult = _actionResult as OkObjectResult;
        var entity = okResult?.Value as Entity;

        entity.Should().NotBeNull();
        entity?.Values.ToArray()[0].Should().BeOfType<Guid>();
        entity?.Values.ToArray()[2].Should().Be("123456789");
        entity?.Values.ToArray()[3].Should().Be(TipoTelefono.Celular);
    }

    [When("I update the telefono with id (.*) with the following data")]
    public async Task WhenIUpdateTheTelefonoWithIdWithTheFollowingData(Guid id, Table table)
    {
        var telefonoRequest = table.CreateInstance<TelefonoRequest>();
        var telefono = new Telefono
        {
            Id = id,
            Numero = telefonoRequest.Numero,
            Operadora = telefonoRequest.Operadora,
            Tipo = telefonoRequest.Tipo
        };

        _telefonoRepositoryMock.Setup(x => x.GetTelefonoById(It.IsAny<Guid>()))
            .ReturnsAsync(telefono);

        _telefonoRepositoryMock.Setup(x => x.UpdateTelefono(It.IsAny<Telefono>()))
            .ReturnsAsync(telefono);

        _telefonoHelperMock.Setup(x => x.GetShapedEntity(It.IsAny<Telefono>(), It.IsAny<TelefonoParameters>()))
            .Returns(_genericDriver.GetShapedEntity(telefono, string.Empty));

        _actionResult = await _telefonosController.TelefonoUpdate(id, telefonoRequest);
    }

    [Then("the result should be a telefono with the following data")]
    public void ThenTheResultShouldBeATelefonoWithTheFollowingData(Table table)
    {
        var okResult = _actionResult as OkObjectResult;
        var entity = okResult?.Value as Entity;

        entity.Should().NotBeNull();
        entity?.Values.ToArray()[0].Should().BeOfType<Guid>();
        entity?.Values.ToArray()[2].Should().Be("Modified");
        entity?.Values.ToArray()[3].Should().Be(TipoTelefono.Celular);
    }

    [When("I delete the telefono with id (.*)")]
    public async Task WhenIDeleteTheTelefonoWithId(Guid id)
    {
        _actionResult = await _telefonosController.TelefonoDelete(id);
    }

    [Then("the result from telefonos controller should be No Content")]
    public void ThenTheResultFromTelefonosControllerShouldBeNoContent()
    {
        _actionResult.Should().BeOfType<NoContentResult>();
    }
}