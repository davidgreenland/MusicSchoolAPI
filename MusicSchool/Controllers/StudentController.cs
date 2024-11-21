using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Responses;

namespace MusicSchool.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly MusicSchoolDBContext _context;

    public StudentController(MusicSchoolDBContext context)
    {
        _context = context;
    }

    // GET: api/Student
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentResponse>>> GetStudent()
    {
        var students = await _context.Student
            .OrderBy(s => s.LastName)
            .Select(x => new StudentResponse(x.Id, $"{x.FirstName} {x.LastName}", x.DateOfBirth))
            .ToListAsync();

        return Ok(students);
    }

    // GET: api/Student/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentResponse>> GetStudent(int id)
    {
        var student = await _context.Student
            .Where(x => x.Id == id)
            .Select(x => new StudentResponse(x.Id, $"{x.FirstName} {x.LastName}", x.DateOfBirth, string.Join(", ", x.Instruments.Select(x => x.Name))))
            .FirstOrDefaultAsync();

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }
}
