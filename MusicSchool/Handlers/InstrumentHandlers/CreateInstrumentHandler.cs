using MediatR;
using MusicSchool.Commands.InstrumentCommands;
using MusicSchool.Models;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Handlers.InstrumentHandlers;

public class CreateInstrumentHandler : IRequestHandler<CreateInstrumentCommand, ApiResult<Instrument>>
{
    private readonly IInstrumentService _instrumentService;

    public CreateInstrumentHandler(IInstrumentService instrumentService)
    {
        _instrumentService = instrumentService;
    }

    public async Task<ApiResult<Instrument>> Handle(CreateInstrumentCommand request, CancellationToken cancellationToken)
    {
        if (await _instrumentService.InstrumentExistsAsync(request.Name))
        {
            return new ApiResult<Instrument>(HttpStatusCode.Conflict, $"Instrument {request.Name} already exists");
        }

        if (!await _instrumentService.CategoryExistsAsync(request.CategoryId))
        {
            return new ApiResult<Instrument>(HttpStatusCode.NotFound, $"Category: {request.CategoryId} not found");
        }

        var instrument = new Instrument
        {
            Name = request.Name,
            CategoryId = request.CategoryId,
        };

        await _instrumentService.InsertAsync(instrument);

        return new ApiResult<Instrument>(HttpStatusCode.Created, instrument);
    }
}
