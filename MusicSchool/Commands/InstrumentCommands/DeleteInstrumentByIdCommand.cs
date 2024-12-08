using MediatR;
using MusicSchool.Models;
using MusicSchool.Responses;

namespace MusicSchool.Commands;

public record DeleteInstrumentByIdCommand(int Id) : IRequest<ApiResult<Instrument>>;
