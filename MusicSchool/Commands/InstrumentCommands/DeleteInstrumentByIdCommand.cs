using MediatR;

namespace MusicSchool.Commands;

public record DeleteInstrumentByIdCommand(int Id) : IRequest;
