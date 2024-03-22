using api.cliente.Contracts;
using api.cliente.Controllers;
using api.cliente.Models;
using api.controllers.specflow.Drivers;
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
public sealed class ClientesControllerStepDefinitions
{
    private const string IdCliente = "a81a773c-df44-44d1-9e29-dc79e12b293b";
    
    private readonly ClientesController _clientesController;
    private readonly Mock<IClienteRepository> _clienteRepositoryMock = new();
    private readonly Mock<IClienteHelper> _clienteHelperMock = new();
    private readonly Mock<LinkGenerator> _linkGeneratorMock = new();
    private readonly GenericDriver<Cliente> _genericDriver;
    private List<Cliente> _clientes = new();

    private ActionResult<LinkCollectionWrapper<Cliente>>? _result;
    private IActionResult? _actionResult;

    public ClientesControllerStepDefinitions(GenericDriver<Cliente> genericDriver)
    {
        _genericDriver = genericDriver;
        _clientesController = new ClientesController(
            _clienteHelperMock.Object, _linkGeneratorMock.Object, _clienteRepositoryMock.Object)
        {
            ControllerContext =
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Given("I have a list of clients")]
    public void GivenIHaveAListOfClients()
    {
        _clientes = new List<Cliente>
        {
            new() { Id = Guid.NewGuid(), Name = "John", LastName = "Doe" },
            new() { Id = Guid.NewGuid(), Name = "Jane", LastName = "Doe" }
        };

        _clienteRepositoryMock.Setup(x => x.FindAll())
            .Returns(_clientes.AsQueryable());
    }

    [When("I retrieve the list of clients")]
    public void WhenIRetrieveTheListOfClients()
    {
        _clienteHelperMock.Setup(x =>
                x.GetFilteredEntities(It.IsAny<IQueryable<Cliente>>(), It.IsAny<ClienteParameters>()))
            .Returns(_clientes);

        var shapedEntities = _genericDriver.GetShapedEntities(_clientes, string.Empty);
        _clienteHelperMock.Setup(x =>
                x.GetShapedEntities(It.IsAny<IEnumerable<Cliente>>(), It.IsAny<ClienteParameters>()))
            .Returns(new PagedList<ShapedEntity>(shapedEntities, shapedEntities.Count, 1, 10));

        _result = _clientesController.ClienteAll(new ClienteParameters());
    }

    [Then("the result should be a list of clients")]
    public void ThenTheResultShouldBeAListOfClients()
    {
        var okResult = _result?.Result as OkObjectResult;
        var clienteWrapper = okResult?.Value as LinkCollectionWrapper<Entity>;

        clienteWrapper.Should().NotBeNull();
        clienteWrapper?.Links.Should().NotBeNull();
        clienteWrapper?.Value.Should().HaveCount(2);
    }

    [Given("I have a client with id (.*)")]
    public void GivenIHaveAClientWithId(Guid id)
    {
        var clienteId = Guid.Parse(IdCliente);
        _clientes = new List<Cliente>
        {
            new() { Id = clienteId, Name = "John", LastName = "Doe" },
            new() { Id = Guid.NewGuid(), Name = "Jane", LastName = "Doe" }
        };

        _clienteRepositoryMock.Setup(x => x.GetClienteById(id))
            .ReturnsAsync(_clientes.FirstOrDefault(x => x.Id == id));
    }

    [When("I retrieve the client with id (.*)")]
    public async Task WhenIRetrieveTheClientWithId(Guid id)
    {
        _clienteHelperMock.Setup(x => x.GetShapedEntity(It.IsAny<Cliente>(), It.IsAny<ClienteParameters>()))
            .Returns(_genericDriver.GetShapedEntity(_clientes.FirstOrDefault(x => x.Id == id), string.Empty));

        _actionResult = await _clientesController.ClienteById(id);
    }

    [Then("the result should be a client with id (.*)")]
    public void ThenTheResultShouldBeAClientWithId(Guid id)
    {
        var okResult = _actionResult as OkObjectResult;
        var entity = okResult?.Value as Entity;

        entity.Should().NotBeNull();
        entity?.Values.ToArray()[0].Should().Be(Guid.Parse(IdCliente));
    }

    [When("I add a new client with the following data")]
    public async Task WhenIAddANewClientWithTheFollowingData(Table table)
    {
        var clienteRequest = table.CreateInstance<ClienteRequest>();
        var cliente = new Cliente
        {
            Name = clienteRequest.Name,
            LastName = clienteRequest.LastName,
            DateBirth = clienteRequest.DateBirth
        };

        _clienteRepositoryMock.Setup(x => x.AddCliente(It.IsAny<Cliente>()))
            .ReturnsAsync(cliente);

        _clienteHelperMock.Setup(x => x.GetShapedEntity(It.IsAny<Cliente>(), It.IsAny<ClienteParameters>()))
            .Returns(_genericDriver.GetShapedEntity(cliente, string.Empty));

        _actionResult = await _clientesController.ClienteSave(clienteRequest);
    }

    [Then("the result should be a new client with the following data")]
    public void ThenTheResultShouldBeANewClientWithTheFollowingData(Table table)
    {
        var okResult = _actionResult as OkObjectResult;
        var entity = okResult?.Value as Entity;

        entity.Should().NotBeNull();
        entity?.Values.ToArray()[0].Should().BeOfType<Guid>();
        entity?.Values.ToArray()[1].Should().Be("John");
        entity?.Values.ToArray()[2].Should().Be("Doe");
    }

    [When("I update the client with id (.*) with the following data")]
    public async Task WhenIUpdateTheClientWithIdWithTheFollowingData(Guid id, Table table)
    {
        var clienteRequest = table.CreateInstance<ClienteRequest>();
        var cliente = new Cliente
        {
            Id = id,
            Name = clienteRequest.Name,
            LastName = clienteRequest.LastName,
            DateBirth = clienteRequest.DateBirth
        };

        _clienteRepositoryMock.Setup(x => x.GetClienteById(It.IsAny<Guid>()))
            .ReturnsAsync(cliente);

        _clienteRepositoryMock.Setup(x => x.UpdateCliente(It.IsAny<Cliente>()))
            .ReturnsAsync(cliente);

        _clienteHelperMock.Setup(x => x.GetShapedEntity(It.IsAny<Cliente>(), It.IsAny<ClienteParameters>()))
            .Returns(_genericDriver.GetShapedEntity(cliente, string.Empty));

        _actionResult = await _clientesController.ClienteUpdate(id, clienteRequest);
    }

    [Then("the result should be a client with the following data")]
    public void ThenTheResultShouldBeAClientWithTheFollowingData(Table table)
    {
        var okResult = _actionResult as OkObjectResult;
        var entity = okResult?.Value as Entity;

        entity.Should().NotBeNull();
        entity?.Values.ToArray()[0].Should().BeOfType<Guid>();
        entity?.Values.ToArray()[1].Should().Be("John");
        entity?.Values.ToArray()[2].Should().Be("Modified");
    }

    [When("I delete the client with id (.*)")]
    public async Task WhenIDeleteTheClientWithId(Guid id)
    {
        _actionResult = await _clientesController.ClienteDelete(id);
    }

    [Then("the result from clientes controller should be No Content")]
    public void ThenTheResultFromClientesControllerShouldBeNoContent()
    {
        _actionResult.Should().BeOfType<NoContentResult>();
    }
}