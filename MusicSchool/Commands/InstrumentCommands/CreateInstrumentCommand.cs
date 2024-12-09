using MediatR;
using MusicSchool.Models;

namespace MusicSchool.Commands.InstrumentCommands;

public record CreateInstrumentCommand(string Name, int CategoryId) : IRequest<Instrument>;