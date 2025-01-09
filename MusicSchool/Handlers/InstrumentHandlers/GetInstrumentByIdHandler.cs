using MediatR;
using MusicSchool.Queries;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Handlers.InstrumentHandlers;

public class GetInstrumentByIdHandler : IRequestHandler<GetInstrumentByIdQuery, InstrumentResponse?>
{
    private readonly IInstrumentService _instrumentService;

    public GetInstrumentByIdHandler(IInstrumentService instrumentService)
    {
        _instrumentService = instrumentService;
    }

    public async Task<InstrumentResponse?> Handle(GetInstrumentByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _instrumentService.GetInstrumentByIdAsync(request.Id);

        return result == null
            ? null
            : new InstrumentResponse(result.Id, result.Name, result.Category!.Name, result.Students);
    }
}
