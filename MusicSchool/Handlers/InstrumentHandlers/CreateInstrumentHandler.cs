using MediatR;
using MusicSchool.Commands.InstrumentCommands;
using MusicSchool.Exceptions;
using MusicSchool.Models;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Handlers.InstrumentHandlers;

public class CreateInstrumentHandler : IRequestHandler<CreateInstrumentCommand, Instrument>
{
    private readonly IInstrumentService _instrumentService;

    public CreateInstrumentHandler(IInstrumentService instrumentService)
    {
        _instrumentService = instrumentService;
    }

    public async Task<Instrument> Handle(CreateInstrumentCommand request, CancellationToken cancellationToken)
    {
        if (await _instrumentService.InstrumentExistsAsync(request.Name))
        {
            throw new NameConflictException(request.Name);
        }

        if (!await _instrumentService.CategoryExistsAsync(request.CategoryId))
        {
            throw new NotFoundException($"Category {request.CategoryId} not found");
        }

        var instrument = new Instrument
        {
            Name = request.Name,
            CategoryId = request.CategoryId,
        };

        await _instrumentService.InsertAsync(instrument);

        return instrument;
    }
}
