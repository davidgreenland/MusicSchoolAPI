using MusicSchool.Responses;

namespace MusicSchool.Services.Interfaces;

public interface ISearchService
{
    Task<ApiResult<IEnumerable<SearchResponse>>> GetSearchResultsAsync(string q);
}
