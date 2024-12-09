using Microsoft.EntityFrameworkCore;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Services;

public class SearchService : ISearchService
{
    private readonly MusicSchoolDBContext _context;

    public SearchService(MusicSchoolDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SearchResponse>> GetSearchResultsAsync(string q)
    {
        return await _context.Student
            .Include(x => x.Instruments)
            .Where(x => x.FirstName.Contains(q) || x.LastName.Contains(q) || x.Instruments!.Any(x => x.Name.Contains(q)))
            .Select(x => new SearchResponse($"{x.FirstName} {x.LastName}", string.Join(", ", x.Instruments!.Select(x => x.Name))))
            .ToListAsync();
    }
}
