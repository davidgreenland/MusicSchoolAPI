using MediatR;
using MusicSchool.Commands;
using MusicSchool.Exceptions;
using MusicSchool.Models;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Handlers.CategoryHandlers;

public class DeleteInstrumentByIdHandler : IRequestHandler<DeleteInstrumentByIdCommand, Instrument>
{
    private readonly IInstrumentService _instrumentService;

    public DeleteInstrumentByIdHandler(IInstrumentService instrumentService)
    {
        _instrumentService = instrumentService;
    }

    public async Task<Instrument> Handle(DeleteInstrumentByIdCommand request, CancellationToken cancellationToken)
    {
        var instrument = await _instrumentService.GetInstrumentByIdAsync(request.Id) ?? throw new InstrumentNotFoundException(request.Id);

        if (await _instrumentService.InstrumentHasStudentsAsync(request.Id))
        {
            throw new DeleteEntityConflict();
        }

        await _instrumentService.DeleteAsync(instrument);

        return instrument;
    }
}
