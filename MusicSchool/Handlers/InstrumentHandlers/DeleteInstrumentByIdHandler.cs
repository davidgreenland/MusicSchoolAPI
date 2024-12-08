using MediatR;
using MusicSchool.Commands;
using MusicSchool.Models;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Handlers.CategoryHandlers;

public class DeleteInstrumentByIdHandler : IRequestHandler<DeleteInstrumentByIdCommand, ApiResult<Instrument>>
{
    private readonly IInstrumentService _instrumentService;

    public DeleteInstrumentByIdHandler(IInstrumentService instrumentService)
    {
        _instrumentService = instrumentService;
    }

    public async Task<ApiResult<Instrument>> Handle(DeleteInstrumentByIdCommand request, CancellationToken cancellationToken)
    {
        var instrument = await _instrumentService.GetInstrumentByIdAsync(request.Id);
        if (instrument == null)
        {
            return new ApiResult<Instrument>(HttpStatusCode.NotFound, $"Instrument ID {request.Id} not found");
        }

        if (await _instrumentService.InstrumentHasStudentsAsync(request.Id))
        {
            return new ApiResult<Instrument>(HttpStatusCode.Conflict, "Unable to delete instrument");
        }

        await _instrumentService.DeleteAsync(instrument);

        return new ApiResult<Instrument>(HttpStatusCode.NoContent, data: null);
    }
}
