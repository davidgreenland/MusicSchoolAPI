using Microsoft.AspNetCore.Mvc;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    // GET: api/Search?q=pet
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SearchResponse>>> GetSearchResultsAsync([FromQuery] string q)
    {
        var result = await _searchService.GetSearchResultsAsync(q);

        return result.IsSuccess
            ? StatusCode(result.StatusCode, result.Data)
            : StatusCode(result.StatusCode, result.Message);
    }
}