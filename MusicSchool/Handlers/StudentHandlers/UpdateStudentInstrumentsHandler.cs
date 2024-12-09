using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Commands.StudentCommands;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Handlers.StudentHandlers;

public class UpdateStudentInstrumentsHandler : IRequestHandler<UpdateStudentInstrumentsCommand, ApiResult<StudentResponse>>
{
    private readonly IStudentService _studentService;
    private readonly IInstrumentService _instrumentService;

    public UpdateStudentInstrumentsHandler(IStudentService studentService, IInstrumentService instrumentService)
    {
        _studentService = studentService;
        _instrumentService = instrumentService;
    }

    public async Task<ApiResult<StudentResponse>> Handle(UpdateStudentInstrumentsCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetStudentByIdAsync(request.Id);
        if (student == null)
        {
            return new ApiResult<StudentResponse>(HttpStatusCode.NotFound, $"Student Id: {request.Id} not found");
        }

        var allInstruments = await _instrumentService.GetAllInstrumentsAsync();

        var validRequestedInstruments = allInstruments
            .Where(i => request.NewInstrumentIds.Contains(i.Id)).ToList();

        if (validRequestedInstruments.Count() != request.NewInstrumentIds.Count())
        {
            var invalidInstrumentIds = request.NewInstrumentIds.Except(validRequestedInstruments.Select(x => x.Id));
            return new ApiResult<StudentResponse>(HttpStatusCode.NotFound, $"Invalid instrument IDs: {string.Join(", ", invalidInstrumentIds)}");
        }

        student.Instruments = validRequestedInstruments;
        await _studentService.CommitAsync();

        return new ApiResult<StudentResponse>(HttpStatusCode.OK, new StudentResponse(student.Id, student.FirstName, student.LastName, student.DateOfBirth, string.Join(", ", student.Instruments.Select(x => x.Name))));
    }
}
