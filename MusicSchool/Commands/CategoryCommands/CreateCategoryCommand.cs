using MediatR;
using MusicSchool.Models;
using MusicSchool.Responses;

namespace MusicSchool.Commands.CategoryCommands;

public record CreateCategoryCommand(string Name) : IRequest<ApiResult<Category>>;
