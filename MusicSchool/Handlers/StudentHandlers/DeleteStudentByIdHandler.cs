using MediatR;
using MusicSchool.Commands.StudentCommands;
using MusicSchool.Models;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Handlers.StudentHandlers;

public class DeleteStudentByIdHandler : IRequestHandler<DeleteStudentByIdCommand, ApiResult<Student>>
{
    private readonly IStudentService _studentService;

    public DeleteStudentByIdHandler(IStudentService studentService, IInstrumentService instrumentService)
    {
        _studentService = studentService;
    }

    public async Task<ApiResult<Student>> Handle(DeleteStudentByIdCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetStudentByIdAsync(request.Id);
        if (student == null)
        {
            return new ApiResult<Student>(HttpStatusCode.NotFound, $"Student: {request.Id} not found");
        }

        await _studentService.DeleteAsync(student);

        return new ApiResult<Student>(HttpStatusCode.NoContent, message: null);
    }
}
