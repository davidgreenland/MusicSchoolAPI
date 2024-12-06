using Microsoft.EntityFrameworkCore;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Services;

public class SearchService : ISearchService
{
    private readonly MusicSchoolDBContext _context;

    public SearchService(MusicSchoolDBContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<IEnumerable<SearchResponse>>> GetSearchResultsAsync(string q)
    {
        var students = await _context.Student
            .Include(x => x.Instruments)
            .Where(x => x.FirstName.Contains(q) || x.LastName.Contains(q) || x.Instruments!.Any(x => x.Name.Contains(q)))
            .Select(x => new SearchResponse($"{x.FirstName} {x.LastName}", string.Join(", ", x.Instruments!.Select(x => x.Name))))
            .ToListAsync();

        return students == null || students.Count == 0
            ? new ApiResult<IEnumerable<SearchResponse>>(HttpStatusCode.NotFound, $"{q} not found")
            : new ApiResult<IEnumerable<SearchResponse>>(HttpStatusCode.OK, students);
    }
}
