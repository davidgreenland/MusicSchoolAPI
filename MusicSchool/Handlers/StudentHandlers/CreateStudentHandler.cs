using MediatR;
using MusicSchool.Commands.StudentCommands;
using MusicSchool.Models;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Handlers.StudentHandlers;

public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, ApiResult<Student>>
{
    private readonly IStudentService _studentService;

    public CreateStudentHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<ApiResult<Student>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth
        };

        await _studentService.InsertAsync(student);

        return new ApiResult<Student>(HttpStatusCode.Created, student);
    }
}
