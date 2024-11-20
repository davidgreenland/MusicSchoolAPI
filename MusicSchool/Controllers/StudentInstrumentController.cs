using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;

namespace MusicSchool.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentInstrumentController : ControllerBase
{
    private readonly MusicSchoolDBContext _context;

    public StudentInstrumentController(MusicSchoolDBContext context)
    {
        _context = context;
    }

    // GET: api/StudentInstrument
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentInstrument>>> GetAllStudentInstruments()
    {
        return await _context.StudentInstrument.ToListAsync();
    }

    // GET: api/StudentInstrument/student/1
    [HttpGet("student/{id}")]
    public async Task<ActionResult<IEnumerable<StudentInstrument>>> GetSingleStudentInstruments(int id)
    {
        var studentInstrument = await _context.StudentInstrument.Where(x => x.StudentId == id).ToListAsync();

        if (studentInstrument == null || studentInstrument.Count == 0)
        {
            return NotFound();
        }

        return studentInstrument;
    }

    // GET: api/StudentInstrument/instrument/2
    [HttpGet("instrument/{id}")]
    public async Task<ActionResult<IEnumerable<StudentInstrument>>> GetSingleInstrumentStudents(int id)
    {
        var studentInstrument = await _context.StudentInstrument.Where(x => x.InstrumentId == id).ToListAsync();

        if (studentInstrument == null || studentInstrument.Count == 0)
        {
            return NotFound();
        }

        return studentInstrument;
    }
}
