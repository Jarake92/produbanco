using api.cliente.Contracts;
using api.cliente.Controllers;
using api.cliente.Models;
using api.controllers.specflow.Drivers;
using api.direccion.Contracts;
using api.direccion.controllers;
using api.direccion.Models;
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
public sealed class DireccionesControllerStepDefinitions
{
    private const string IdDireccion = "1444c789-5cca-4790-997a-0615194f7bde";
    
    private readonly DireccionesController _direccionesController;
    private readonly Mock<IDireccionesRepository> _direccionRepositoryMock = new();
    private readonly Mock<IDireccionHelper> _direccionHelperMock = new();
    private readonly Mock<LinkGenerator> _linkGeneratorMock = new();
    private readonly GenericDriver<Direccion> _genericDriver;
    private List<Direccion> _direcciones = new();

    private ActionResult<LinkCollectionWrapper<Direccion>>? _result;
    private IActionResult? _actionResult;

    public DireccionesControllerStepDefinitions(GenericDriver<Direccion> genericDriver)
    {
        _genericDriver = genericDriver;
        _direccionesController = new DireccionesController(
            _direccionHelperMock.Object, _direccionRepositoryMock.Object, _linkGeneratorMock.Object)
        {
            ControllerContext =
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Given("I have a list of direcciones")]
    public void GivenIHaveAListOfDirecciones()
    {
        _direcciones = new List<Direccion>
        {
            new() { Id = Guid.NewGuid(), Provincia = "Lorem", Canton = "Ipsum", CallePrincipal = "Dolor" },
            new() { Id = Guid.NewGuid(), Provincia = "Sit", Canton = "Amet", CallePrincipal = "Consectetur" }
        };

        _direccionRepositoryMock.Setup(x => x.FindAll())
            .Returns(_direcciones.AsQueryable());
    }

    [When("I retrieve the list of direcciones")]
    public void WhenIRetrieveTheListOfDirecciones()
    {
        _direccionHelperMock.Setup(x =>
                x.GetFilteredEntities(It.IsAny<IQueryable<Direccion>>(), It.IsAny<DireccionParameters>()))
            .Returns(_direcciones);

        var shapedEntities = _genericDriver.GetShapedEntities(_direcciones, string.Empty);
        _direccionHelperMock.Setup(x =>
                x.GetShapedEntities(It.IsAny<IEnumerable<Direccion>>(), It.IsAny<DireccionParameters>()))
            .Returns(new PagedList<ShapedEntity>(shapedEntities, shapedEntities.Count, 1, 10));

        _result = _direccionesController.DireccionAll(new DireccionParameters());
    }

    [Then("the result should be a list of direcciones")]
    public void ThenTheResultShouldBeAListOfDirecciones()
    {
        var okResult = _result?.Result as OkObjectResult;
        var direccionWrapper = okResult?.Value as LinkCollectionWrapper<Entity>;

        direccionWrapper.Should().NotBeNull();
        direccionWrapper?.Links.Should().NotBeNull();
        direccionWrapper?.Value.Should().HaveCount(2);
    }

    [Given("I have a direccion with id (.*)")]
    public void GivenIHaveADireccionWithId(Guid id)
    {
        var direccionId = Guid.Parse(IdDireccion);
        _direcciones = new List<Direccion>
        {
            new() { Id = direccionId, Provincia = "lorem", Canton = "ipsum", CallePrincipal = "dolor" },
            new() { Id = Guid.NewGuid(), Provincia = "sit", Canton = "amet", CallePrincipal = "donsectetur" }
        };

        _direccionRepositoryMock.Setup(x => x.GetDireccionById(id))
            .ReturnsAsync(_direcciones.FirstOrDefault(x => x.Id == id));
    }

    [When("I retrieve the direccion with id (.*)")]
    public async Task WhenIRetrieveTheDireccionWithId(Guid id)
    {
        _direccionHelperMock.Setup(x => x.GetShapedEntity(It.IsAny<Direccion>(), It.IsAny<DireccionParameters>()))
            .Returns(_genericDriver.GetShapedEntity(_direcciones.FirstOrDefault(x => x.Id == id), string.Empty));

        _actionResult = await _direccionesController.DireccionById(id);
    }

    [Then("the result should be a direccion with id (.*)")]
    public void ThenTheResultShouldBeADireccionWithId(Guid id)
    {
        var okResult = _actionResult as OkObjectResult;
        var entity = okResult?.Value as Entity;

        entity.Should().NotBeNull();
        entity?.Values.ToArray()[0].Should().Be(Guid.Parse(IdDireccion));
    }

    [When("I add a new direccion with the following data")]
    public async Task WhenIAddANewDireccionWithTheFollowingData(Table table)
    {
        var direccionRequest = table.CreateInstance<DireccionRequest>();
        var direccion = new Direccion
        {
            Provincia = direccionRequest.Provincia,
            Canton = direccionRequest.Canton,
            CallePrincipal = direccionRequest.CallePrincipal
        };

        _direccionRepositoryMock.Setup(x => x.AddDireccion(It.IsAny<Direccion>()))
            .ReturnsAsync(direccion);

        _direccionHelperMock.Setup(x => x.GetShapedEntity(It.IsAny<Direccion>(), It.IsAny<DireccionParameters>()))
            .Returns(_genericDriver.GetShapedEntity(direccion, string.Empty));

        _actionResult = await _direccionesController.DireccionSave(direccionRequest);
    }

    [Then("the result should be a new direccion with the following data")]
    public void ThenTheResultShouldBeANewDireccionWithTheFollowingData(Table table)
    {
        var okResult = _actionResult as OkObjectResult;
        var entity = okResult?.Value as Entity;

        entity.Should().NotBeNull();
        entity?.Values.ToArray()[0].Should().BeOfType<Guid>();
        entity?.Values.ToArray()[2].Should().Be("lorem");
        entity?.Values.ToArray()[3].Should().Be("ipsum");
    }

    [When("I update the direccion with id (.*) with the following data")]
    public async Task WhenIUpdateTheDireccionWithIdWithTheFollowingData(Guid id, Table table)
    {
        var direccionRequest = table.CreateInstance<DireccionRequest>();
        var direccion = new Direccion
        {
            Provincia = direccionRequest.Provincia,
            Canton = direccionRequest.Canton,
            CallePrincipal = direccionRequest.CallePrincipal
        };

        _direccionRepositoryMock.Setup(x => x.GetDireccionById(It.IsAny<Guid>()))
            .ReturnsAsync(direccion);

        _direccionRepositoryMock.Setup(x => x.UpdateDireccion(It.IsAny<Direccion>()))
            .ReturnsAsync(direccion);

        _direccionHelperMock.Setup(x => x.GetShapedEntity(It.IsAny<Direccion>(), It.IsAny<DireccionParameters>()))
            .Returns(_genericDriver.GetShapedEntity(direccion, string.Empty));

        _actionResult = await _direccionesController.DireccionUpdate(id, direccionRequest);
    }

    [Then("the result should be a direccion with the following data")]
    public void ThenTheResultShouldBeADireccionWithTheFollowingData(Table table)
    {
        var okResult = _actionResult as OkObjectResult;
        var entity = okResult?.Value as Entity;

        entity.Should().NotBeNull();
        entity?.Values.ToArray()[0].Should().BeOfType<Guid>();
        entity?.Values.ToArray()[2].Should().Be("Modified");
        entity?.Values.ToArray()[3].Should().Be("ipsum");
    }

    [When("I delete the direccion with id (.*)")]
    public async Task WhenIDeleteTheDireccionWithId(Guid id)
    {
        _actionResult = await _direccionesController.DireccionDelete(id);
    }

    [Then("the result from direcciones controller should be No Content")]
    public void ThenTheResultFromDireccionesControllerShouldBeNoContent()
    {
        _actionResult.Should().BeOfType<NoContentResult>();
    }
}