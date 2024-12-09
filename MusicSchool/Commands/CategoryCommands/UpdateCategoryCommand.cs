using MediatR;
using MusicSchool.Models;

namespace MusicSchool.Commands.CategoryCommands;

public record UpdateCategoryCommand(int Id, string NewName) : IRequest<Category>;
