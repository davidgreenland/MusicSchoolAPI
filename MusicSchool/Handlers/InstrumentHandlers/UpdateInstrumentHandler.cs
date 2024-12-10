using MediatR;
using MusicSchool.Commands.InstrumentCommands;
using MusicSchool.Exceptions;
using MusicSchool.Models;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Handlers.InstrumentHandlers;

public class UpdateInstrumentHandler : IRequestHandler<UpdateInstrumentCommand, Instrument>
{
    private readonly IInstrumentService _instrumentService;

    public UpdateInstrumentHandler(IInstrumentService instrumentService)
    {
        _instrumentService = instrumentService;
    }

    public async Task<Instrument> Handle(UpdateInstrumentCommand request, CancellationToken cancellationToken)
    {
        var instrument = await _instrumentService.GetInstrumentByIdAsync(request.Id) ?? throw new NotFoundException($"Instrument {request.Id} not found");

        if (!await _instrumentService.CategoryExistsAsync(request.NewCategoryId)) // foreign key
        {
            throw new NotFoundException($"Category {request.NewCategoryId} not found");
        }

        if (instrument.Name != request.NewName && await _instrumentService.InstrumentExistsAsync(request.NewName))
        {
            throw new NameConflictException(request.NewName);
        }
        
        instrument.Name = request.NewName;
        instrument.CategoryId = request.NewCategoryId;
        await _instrumentService.CommitAsync();

        return instrument;
    }
}
