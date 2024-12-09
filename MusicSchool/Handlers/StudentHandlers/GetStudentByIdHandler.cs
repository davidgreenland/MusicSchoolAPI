using MediatR;
using MusicSchool.Queries;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Handlers.StudentHandlers;

public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, StudentResponse?>
{
    private readonly IStudentService _studentService;

    public GetStudentByIdHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<StudentResponse?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetStudentByIdAsync(request.Id);

        if (student == null)
        {
            return null;
        }

        var instruments = student.Instruments!.Count == 0
            ? "no instruments added"
            : string.Join(", ", student.Instruments.Select(x => x.Name));

        return new StudentResponse(student.Id, student.FirstName, student.LastName, student.DateOfBirth, instruments);
    }
}
