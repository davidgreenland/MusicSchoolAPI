using MediatR;
using MusicSchool.Commands.InstrumentCommands;
using MusicSchool.Models;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Handlers.InstrumentHandlers;

public class UpdateInstrumentHandler : IRequestHandler<UpdateInstrumentCommand, ApiResult<Instrument>>
{
    private readonly IInstrumentService _instrumentService;

    public UpdateInstrumentHandler(IInstrumentService instrumentService)
    {
        _instrumentService = instrumentService;
    }

    public async Task<ApiResult<Instrument>> Handle(UpdateInstrumentCommand request, CancellationToken cancellationToken)
    {
        var instrument = await _instrumentService.GetInstrumentByIdAsync(request.Id);

        if (instrument == null)
        {
            return new ApiResult<Instrument>(HttpStatusCode.NotFound, $"Instrument ID {request.Id} not found");

        }

        if (!await _instrumentService.CategoryExistsAsync(request.NewCategoryId)) // foreign key
        {
            return new ApiResult<Instrument>(HttpStatusCode.NotFound, $"Category: {request.NewCategoryId} not found");
        }

        if (instrument.Name != request.NewName && await _instrumentService.InstrumentExistsAsync(request.NewName))
        {
            return new ApiResult<Instrument>(HttpStatusCode.Conflict, $"Instrument with name {request.NewName}, is already in the database");
        }
        
        instrument.Name = request.NewName;
        instrument.CategoryId = request.NewCategoryId;
        await _instrumentService.CommitAsync();

        return new ApiResult<Instrument>(HttpStatusCode.OK, instrument);
    }
}
