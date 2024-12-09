using MediatR;
using MusicSchool.Models;

namespace MusicSchool.Commands;

public record DeleteInstrumentByIdCommand(int Id) : IRequest<Instrument>;
