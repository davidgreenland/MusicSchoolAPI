using MediatR;
using MusicSchool.Models;
using MusicSchool.Queries;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Handlers.SearchHandlers;

public class GetSearchResultsHandler : IRequestHandler<GetSearchResultsQuery, IEnumerable<Student>>
{
    private readonly ISearchService _searchService;

    public GetSearchResultsHandler(ISearchService searchService)
    {
        _searchService = searchService;
    }

    public async Task<IEnumerable<Student>> Handle(GetSearchResultsQuery request, CancellationToken cancellationToken)
    {
        var students = await _searchService.GetSearchResultsAsync(request.Query);

        return students;
    }
}
