using MediatR;
using MusicSchool.Models;
using MusicSchool.Responses;

namespace MusicSchool.Commands.CategoryCommands;

public record UpdateCategoryCommand(int Id, string NewName) : IRequest<ApiResult<Category>>;
