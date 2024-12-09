using MediatR;
using MusicSchool.Commands.StudentCommands;
using MusicSchool.Models;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Handlers.StudentHandlers;

public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, Student>
{
    private readonly IStudentService _studentService;

    public CreateStudentHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<Student> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth
        };

        await _studentService.InsertAsync(student);

        return student;
    }
}
