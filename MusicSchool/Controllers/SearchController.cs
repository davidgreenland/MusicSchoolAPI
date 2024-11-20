using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Resources;

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
        var students = await (from s in _context.Student
                    join si in _context.StudentInstrument
                    on s.Id equals si.StudentId
                    join i in _context.Instrument
                    on si.InstrumentId equals i.Id
                    where s.FirstName.Contains(q)
                    || s.LastName.Contains(q)
                    || i.Name.Contains(q)
                    select new { s.FirstName, s.LastName, i.Name }).ToListAsync();

        if (students == null || students.Count == 0)
        {
            return NotFound();
        }

        var response = new List<SearchResponse>();

        foreach (var student in students) 
        {
            response.Add(new SearchResponse($"{student.FirstName} {student.LastName}", student.Name));
        }

        return Ok(response);
    }
}