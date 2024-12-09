using MediatR;
using MusicSchool.Models;
using MusicSchool.Responses;

namespace MusicSchool.Commands.InstrumentCommands;

public record UpdateInstrumentCommand(int Id, string NewName, int NewCategoryId) : IRequest<Instrument>;
