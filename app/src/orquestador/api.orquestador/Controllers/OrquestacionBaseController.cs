using api.orquestador.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.orquestador.Controllers;

[Authorize]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class OrquestacionBase : ControllerBase
{
    private protected readonly IClienteManager ClienteManager;

    protected OrquestacionBase(IClienteManager clienteManager)
    {
        ClienteManager = clienteManager;
    }
}