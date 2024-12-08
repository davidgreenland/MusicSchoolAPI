using MediatR;
using MusicSchool.Models;
using MusicSchool.Responses;

namespace MusicSchool.Commands.InstrumentCommands;

public record CreateInstrumentCommand(string Name, int CategoryId) : IRequest<ApiResult<Instrument>>;