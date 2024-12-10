using MediatR;
using MusicSchool.Commands.StudentCommands;
using MusicSchool.Exceptions;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Handlers.StudentHandlers;

public class DeleteStudentByIdHandler : IRequestHandler<DeleteStudentByIdCommand>
{
    private readonly IStudentService _studentService;

    public DeleteStudentByIdHandler(IStudentService studentService, IInstrumentService instrumentService)
    {
        _studentService = studentService;
    }

    public async Task Handle(DeleteStudentByIdCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetStudentByIdAsync(request.Id) ?? throw new NotFoundException($"Student {request.Id} not found");

        await _studentService.DeleteAsync(student);

        return; // todo check this
    }
}
