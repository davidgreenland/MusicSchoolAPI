using MediatR;
using MusicSchool.Commands;
using MusicSchool.Exceptions;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Handlers.InstrumentHandlers;

public class DeleteInstrumentByIdHandler : IRequestHandler<DeleteInstrumentByIdCommand>
{
    private readonly IInstrumentService _instrumentService;

    public DeleteInstrumentByIdHandler(IInstrumentService instrumentService)
    {
        _instrumentService = instrumentService;
    }

    public async Task Handle(DeleteInstrumentByIdCommand request, CancellationToken cancellationToken)
    {
        var instrument = await _instrumentService.GetInstrumentByIdAsync(request.Id) ?? throw new NotFoundException($"Instrument {request.Id} not found");

        if (await _instrumentService.InstrumentHasStudentsAsync(request.Id))
        {
            throw new DeleteEntityConflict("Unable to delete instrument");
        }

        await _instrumentService.DeleteAsync(instrument);

        return; //todo check
    }
}
