using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSchool.Commands.StudentCommands;
using MusicSchool.Models;
using MusicSchool.Queries;
using MusicSchool.Requests.StudentRequests;
using MusicSchool.Responses;

namespace MusicSchool.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/Student
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentResponse>>> GetAllStudents()
    {
        var students = await _mediator.Send(new GetAllStudentsQuery());

        return Ok(students);
    }

    // GET: api/Student/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentResponse>> GetStudentById(int id)
    {
        var student = await _mediator.Send(new GetStudentByIdQuery(id));

        return student == null
            ? NotFound("Id not found")
            : Ok(student);
    }

    // PUT: api/Student/1
    [HttpPut("{id:int}")]
    public async Task<ActionResult<StudentResponse>> UpdateStudent(int id, [FromBody] UpdateStudentPut request)
    {
        var student = await _mediator.Send(new UpdateStudentCommand(id, request.NewFirstName, request.NewLastName, request.NewDateOfBirth));

        return Ok(student);
    }

    // PATCH: api/Student/2/instruments
    [HttpPatch("{id:int}/instruments")]
    public async Task<ActionResult<StudentResponse>> UpdateStudentInstruments(int id, [FromBody] UpdateStudentInstrumentsPatch request)
    {
        var student =  await _mediator.Send(new UpdateStudentInstrumentsCommand(id, request.NewInstrumentIds));

        return Ok(student);
    }

    // POST: api/Student
    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent([FromBody] CreateStudentRequest request)
    {
        var student = await _mediator.Send(new CreateStudentCommand(request.FirstName, request.LastName, request.DateOfBirth));

        return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
    }

    // DELETE: api/Student/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteStudent(int id)
    {
        await _mediator.Send(new DeleteStudentByIdCommand(id));

        return NoContent();
    }
}
