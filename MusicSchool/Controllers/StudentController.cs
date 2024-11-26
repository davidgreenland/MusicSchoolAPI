using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;
using MusicSchool.Requests.Student;
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
    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentResponse>> GetStudent(int id)
    {
        var student =  await _context.Student
            .Include(x => x.Instruments)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok( new StudentResponse(student.Id, $"{student.FirstName} {student.LastName}", student.DateOfBirth, string.Join(", ", student.Instruments.Select(x => x.Name))));
    }

    // PUT: api/Student/1
    [HttpPut("{id:int}")]
    public async Task<ActionResult<StudentResponse>> UpdateInstrument(int id, [FromBody] UpdateStudentPut request)
    {
        var student = await _context.Student
            .SingleOrDefaultAsync(x => x.Id == id);

        if (student == null)
        {
            return NotFound("Id not found");
        }

        student.FirstName = request.NewFirstName;
        student.LastName = request.NewLastName;
        student.DateOfBirth = request.NewDateOfBirth;
        await _context.SaveChangesAsync();

        return Ok(student);
    }

    [HttpPatch("{id:int}/instruments")]
    public async Task<ActionResult<Student>> UpdateStudentInstruments(int id, [FromBody] UpdateStudentInstrumentsPatch request)
    {
        var student = await _context.Student
                        .Include(x => x.Instruments)
                        .SingleOrDefaultAsync(x => x.Id == id);
        if (student == null)
        {
            return BadRequest("student not found");
        }

        // need to deal with invalid instrumentId?
        var newInstruments = await _context.Instrument
            .Where(x => request.NewInstrumentIds.Contains(x.Id))
            .ToListAsync();

        student.Instruments = newInstruments;
        await _context.SaveChangesAsync();

        return Ok(student);
    }
}
