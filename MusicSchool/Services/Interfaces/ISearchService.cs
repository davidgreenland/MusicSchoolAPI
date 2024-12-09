using MusicSchool.Models;

namespace MusicSchool.Services.Interfaces;

public interface ISearchService
{
    Task<IEnumerable<Student>> GetSearchResultsAsync(string q);
}
