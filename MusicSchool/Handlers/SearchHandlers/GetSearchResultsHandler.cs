using MediatR;
using MusicSchool.Queries;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Handlers.SearchHandlers;

public class GetSearchResultsHandler : IRequestHandler<GetSearchResultsQuery, ApiResult<IEnumerable<SearchResponse>>>
{
    private readonly ISearchService _searchService;

    public GetSearchResultsHandler(ISearchService searchService)
    {
        _searchService = searchService;
    }

    public async Task<ApiResult<IEnumerable<SearchResponse>>> Handle(GetSearchResultsQuery request, CancellationToken cancellationToken)
    {
        var students = await _searchService.GetSearchResultsAsync(request.Query);

        return students == null || students.Count() == 0
            ? new ApiResult<IEnumerable<SearchResponse>>(HttpStatusCode.NotFound, $"{request.Query} not found")
            : new ApiResult<IEnumerable<SearchResponse>>(HttpStatusCode.OK, students);
    }
}
