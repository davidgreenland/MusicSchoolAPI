using MediatR;
using MusicSchool.Commands.StudentCommands;
using MusicSchool.Models;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Handlers.StudentHandlers;

public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, ApiResult<Student>>
{
    private readonly IStudentService _studentService;

    public UpdateStudentHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<ApiResult<Student>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetStudentByIdAsync(request.Id);
        if (student == null)
        {
            return new ApiResult<Student>(HttpStatusCode.NotFound, "Id not found");
        }

        student.FirstName = request.NewFirstName;
        student.LastName = request.NewLastName;
        student.DateOfBirth = request.NewDateOfBirth;

        await _studentService.CommitAsync();

        return new ApiResult<Student>(HttpStatusCode.OK, student);
    }
}
