using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSchool.Queries;
using MusicSchool.Responses;

namespace MusicSchool.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly IMediator _mediator;

    public SearchController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/Search?q=pet
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SearchResponse>>> GetSearchResultsAsync([FromQuery] string q)
    {
        var result = await _mediator.Send(new GetSearchResultsQuery(q));

        return result
            .Select(x => new SearchResponse($"{x.FirstName} {x.LastName}", string.Join(", ", x.Instruments!.Select(x => x.Name))))
            .ToList();
    }
}