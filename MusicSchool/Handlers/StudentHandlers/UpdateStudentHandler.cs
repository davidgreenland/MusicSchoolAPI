using MediatR;
using MusicSchool.Commands.StudentCommands;
using MusicSchool.Exceptions;
using MusicSchool.Models;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Handlers.StudentHandlers;

public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, Student>
{
    private readonly IStudentService _studentService;

    public UpdateStudentHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }
    public async Task<Student> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetStudentByIdAsync(request.Id) ?? throw new NotFoundException($"Student {request.Id} not found");

        student.FirstName = request.NewFirstName;
        student.LastName = request.NewLastName;
        student.DateOfBirth = request.NewDateOfBirth;

        await _studentService.CommitAsync();

        return student;
    }
}
