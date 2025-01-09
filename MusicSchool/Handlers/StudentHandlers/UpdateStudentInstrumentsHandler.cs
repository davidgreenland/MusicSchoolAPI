using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Commands.StudentCommands;
using MusicSchool.Exceptions;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Handlers.StudentHandlers;

public class UpdateStudentInstrumentsHandler : IRequestHandler<UpdateStudentInstrumentsCommand, StudentResponse>
{
    private readonly IStudentService _studentService;
    private readonly IInstrumentService _instrumentService;

    public UpdateStudentInstrumentsHandler(IStudentService studentService, IInstrumentService instrumentService)
    {
        _studentService = studentService;
        _instrumentService = instrumentService;
    }

    public async Task<StudentResponse> Handle(UpdateStudentInstrumentsCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetStudentByIdAsync(request.Id) ??  throw new NotFoundException($"Student {request.Id} not found");  
        
        var allInstruments = await _instrumentService.GetAllInstrumentsAsync();

        var validRequestedInstruments = allInstruments
            .Where(i => request.NewInstrumentIds.Contains(i.Id)).ToList();

        if (validRequestedInstruments.Count != request.NewInstrumentIds.Count())
        {
            var invalidInstrumentIds = request.NewInstrumentIds.Except(validRequestedInstruments.Select(x => x.Id));
            throw new NotFoundException($"Instrument ID {string.Join(", ", invalidInstrumentIds)}, not found");
        }

        student.Instruments = validRequestedInstruments;
        await _studentService.CommitAsync();

        return new StudentResponse(student.Id, student.FirstName, student.LastName, student.DateOfBirth, string.Join(", ", student.Instruments.Select(x => x.Name)));
    }
}
