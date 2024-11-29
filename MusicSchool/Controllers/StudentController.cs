using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;
using MusicSchool.Requests.Student;
using MusicSchool.Responses;
using MusicSchool.Services;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: api/Student
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentResponse>>> GetStudent()
    {
        var students = await _studentService.GetAllCategoriesAsync();

        return Ok(students);
    }

    // GET: api/Student/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentResponse>> GetStudent(int id)
    {
        var response = await _studentService.GetStudentAsync(id);

        return response.IsSuccess
            ? Ok(response.Data) : NotFound();
    }

    //// PUT: api/Student/1
    //[HttpPut("{id:int}")]
    //public async Task<ActionResult<StudentResponse>> UpdateInstrument(int id, [FromBody] UpdateStudentPut request)
    //{
    //    var student = await _context.Student
    //        .SingleOrDefaultAsync(x => x.Id == id);

    //    if (student == null)
    //    {
    //        return NotFound("Id not found");
    //    }

    //    student.FirstName = request.NewFirstName;
    //    student.LastName = request.NewLastName;
    //    student.DateOfBirth = request.NewDateOfBirth;
    //    await _context.SaveChangesAsync();

    //    return Ok(student);
    //}

    //[HttpPatch("{id:int}/instruments")]
    //public async Task<ActionResult<StudentResponse>> UpdateStudentInstruments(int id, [FromBody] UpdateStudentInstrumentsPatch request)
    //{
    //    var student = await _context.Student
    //                    .Include(x => x.Instruments)
    //                    .SingleOrDefaultAsync(x => x.Id == id);

    //    if (student == null)
    //    {
    //        return NotFound("student not found");
    //    }

    //    var newInstruments = await _context.Instrument
    //        .Where(existing => request.NewInstrumentIds.Contains(existing.Id))
    //        .ToListAsync();

    //    if (newInstruments.Count != request.NewInstrumentIds.Count())
    //    {
    //        var invalidInstrumentIds = request.NewInstrumentIds.Except(newInstruments.Select(x => x.Id));
    //        return NotFound($"Invalid instrument IDs: {string.Join(", ", invalidInstrumentIds)}");
    //    }

    //    student.Instruments = newInstruments;
    //    await _context.SaveChangesAsync();

    //    return Ok(new StudentResponse(student.Id, $"{student.FirstName} {student.LastName}", student.DateOfBirth, string.Join(", ", student.Instruments.Select(x => x.Name))));
    //}

    //// POST: api/Student
    //[HttpPost]
    //public async Task<ActionResult<Student>> CreateStudent([FromBody] CreateStudentRequest request)
    //{
    //    var student = new Student
    //    {
    //        FirstName = request.FirstName,
    //        LastName = request.LastName,
    //        DateOfBirth = request.DateOfBirth
    //    };

    //    _context.Student.Add(student);
    //    await _context.SaveChangesAsync();

    //    return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    //}

    //// DELETE: api/Student/5
    //[HttpDelete("{id:int}")]
    //public async Task<ActionResult> DeleteStudent(int id)
    //{
    //    var student = await _context.Student
    //        .Include(s => s.Instruments)
    //        .SingleOrDefaultAsync(s => s.Id == id);

    //    if (student == null)
    //    {
    //        return NotFound($"Student: {id} not found");
    //    }

    //    _context.Student.Remove(student);
    //    await _context.SaveChangesAsync();

    //    return NoContent();
    //}
}
