using Azure;
using Microsoft.AspNetCore.Mvc;
using MusicSchool.Models;
using MusicSchool.Requests.Student;
using MusicSchool.Responses;
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

        return HandleApiResponse(students);
    }

    // GET: api/Student/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentResponse>> GetStudent(int id)
    {
        var response = await _studentService.GetStudentAsync(id);

        return HandleApiResponse(response);
    }

    // PUT: api/Student/1
    [HttpPut("{id:int}")]
    public async Task<ActionResult<StudentResponse>> UpdateInstrument(int id, [FromBody] UpdateStudentPut request)
    {
        var response = await _studentService.UpdateInstrumentAsync(id, request);

        return HandleApiResponse(response);
    }

    // PATCH: api/Student/2/instruments
    [HttpPatch("{id:int}/instruments")]
    public async Task<ActionResult<StudentResponse>> UpdateStudentInstruments(int id, [FromBody] UpdateStudentInstrumentsPatch request)
    {
        var response = await _studentService.UpdateStudentInstrumentsAsync(id, request);

        return HandleApiResponse(response);
    }

    // POST: api/Student
    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent([FromBody] CreateStudentRequest request)
    {
        var response = await _studentService.CreateStudentAsync(request);

        return HandleApiResponse(response);
    }

    // DELETE: api/Student/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteStudent(int id)
    {
        var response = await _studentService.DeleteStudentAsync(id);

        return HandleApiResponse(response);
    }

    private ObjectResult HandleApiResponse<T>(ApiResponse<T> response) where T : class
    {
        return response.IsSuccess
            ? StatusCode(response.StatusCode, response.Data)
            : StatusCode(response.StatusCode, response.Message);
    }
}
