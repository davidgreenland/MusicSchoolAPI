using MediatR;

namespace MusicSchool.Commands.CategoryCommands;

public record DeleteCategoryByIdCommand(int Id) : IRequest;
