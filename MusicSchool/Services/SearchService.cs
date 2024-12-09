using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Services;

public class SearchService : ISearchService
{
    private readonly MusicSchoolDBContext _context;

    public SearchService(MusicSchoolDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetSearchResultsAsync(string q)
    {
        return await _context.Student
            .Include(x => x.Instruments)
            .Where(x => x.FirstName.Contains(q) || x.LastName.Contains(q) || x.Instruments!.Any(x => x.Name.Contains(q)))
            .ToListAsync();
    }
}
