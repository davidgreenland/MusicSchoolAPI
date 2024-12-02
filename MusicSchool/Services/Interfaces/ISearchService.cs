using MusicSchool.Responses;

namespace MusicSchool.Services.Interfaces;

public interface ISearchService
{
    Task<ApiResponse<IEnumerable<SearchResponse>>> GetSearchResultsAsync(string q);
}
