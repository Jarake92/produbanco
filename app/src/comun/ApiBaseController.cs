using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using shared.comun.hetoas;
using shared.comun.hetoas.link;

namespace shared.comun;

[Authorize]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class ApiBaseController : ControllerBase
{
    private readonly LinkGenerator _linkGenerator;

    public ApiBaseController(LinkGenerator linkGenerator)
    {
        _linkGenerator = linkGenerator;
    }

    protected List<Link> CreateLinksForEntity(Guid id, string fields, string suffixAction)
    {
        var links = new List<Link>();

        links.AddRange(new[]
        {
            new Link(
                href: _linkGenerator.GetUriByAction(httpContext: HttpContext, action: $"{suffixAction}ById",
                    values: new { id, fields }),
                rel: "self",
                method: "GET"),

            new Link(
                href: _linkGenerator.GetUriByAction(httpContext: HttpContext, action: $"{suffixAction}Delete",
                    values: new { id }),
                rel: "delete",
                method: "DELETE"),

            new Link(
                href: _linkGenerator.GetUriByAction(httpContext: HttpContext, action: $"{suffixAction}Save",
                    values: new { }),
                rel: "post",
                method: "POST")
        });

        return links;
    }

    protected LinkCollectionWrapper<Entity> CreateLinksForEntities(
        LinkCollectionWrapper<Entity> entitiesWrapper,
        string suffixAction)
    {
        entitiesWrapper.Links.Add(new Link(
            href: _linkGenerator.GetUriByAction(httpContext: HttpContext, action: $"{suffixAction}All",
                values: new { }),
            rel: "self",
            method: "GET"));

        return entitiesWrapper;
    }
}