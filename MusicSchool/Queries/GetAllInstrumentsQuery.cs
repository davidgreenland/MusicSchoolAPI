using MediatR;
using MusicSchool.Responses;

namespace MusicSchool.Queries;

public record GetAllInstrumentsQuery() : IRequest<IEnumerable<InstrumentResponse>>;
