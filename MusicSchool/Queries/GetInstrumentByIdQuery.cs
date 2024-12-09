using MediatR;
using MusicSchool.Responses;

namespace MusicSchool.Queries;

public record GetInstrumentByIdQuery(int Id) : IRequest<InstrumentResponse>;
