using MediatR;
using MusicSchool.Models;

namespace MusicSchool.Commands.CategoryCommands;

public record CreateCategoryCommand(string Name) : IRequest<Category>;
