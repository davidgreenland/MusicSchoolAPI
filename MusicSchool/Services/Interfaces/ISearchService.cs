using MusicSchool.Responses;

namespace MusicSchool.Services.Interfaces;

public interface ISearchService
{
    Task<IEnumerable<SearchResponse>> GetSearchResultsAsync(string q);
}
