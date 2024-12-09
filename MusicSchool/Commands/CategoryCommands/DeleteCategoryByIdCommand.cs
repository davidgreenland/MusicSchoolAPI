using MediatR;
using MusicSchool.Models;
using MusicSchool.Responses;

namespace MusicSchool.Commands.CategoryCommands;

public record DeleteCategoryByIdCommand(int Id) : IRequest<ApiResult<Category>>;
