using MediatR;
using MusicSchool.Queries;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Handlers.InstrumentHandlers;

public class GetAllInstrumentsHandler : IRequestHandler<GetAllInstrumentsQuery, IEnumerable<InstrumentResponse>>
{
    private readonly IInstrumentService _instrumentService;

    public GetAllInstrumentsHandler(IInstrumentService instrumentService)
    {
        _instrumentService = instrumentService;
    }

    public async Task<IEnumerable<InstrumentResponse>> Handle(GetAllInstrumentsQuery request, CancellationToken cancellationToken)
    {
        var instruments = await _instrumentService.GetAllInstrumentsAsync();

        return instruments.Select(x => new InstrumentResponse(x.Id, x.Name));
    }
}
