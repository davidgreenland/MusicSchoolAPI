using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Responses;

namespace MusicSchool.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly MusicSchoolDBContext _context;

    public SearchController(MusicSchoolDBContext context)
    {
        _context = context;
    }

    // GET: api/Search?q=pet
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SearchResponse>>> GetStudent([FromQuery] string q)
    {
        var students = await _context.Student
            .Include(x => x.Instruments)
            .Where(x => x.FirstName.Contains(q) || x.LastName.Contains(q) || x.Instruments.Any(x => x.Name.Contains(q)))
            .Select(x => new SearchResponse($"{x.FirstName} {x.LastName}", string.Join(", ", x.Instruments.Select(x => x.Name))))
            .ToListAsync();

        if (students == null || students.Count == 0)
        {
            return NotFound();
        }

        return Ok(students);
    }
}