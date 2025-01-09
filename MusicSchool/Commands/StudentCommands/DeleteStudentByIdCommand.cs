using MediatR;

namespace MusicSchool.Commands.StudentCommands;

public record DeleteStudentByIdCommand(int Id) : IRequest;
