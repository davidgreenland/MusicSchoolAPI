using MediatR;
using MusicSchool.Queries;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Handlers.StudentHandlers;

public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentResponse>>
{
    private readonly IStudentService _studentService;

    public GetAllStudentsHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<IEnumerable<StudentResponse>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _studentService.GetAllStudentsAsync();

        return students.Select(x => new StudentResponse(x.Id, x.FirstName, x.LastName, x.DateOfBirth));
    }
}
