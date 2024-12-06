using MediatR;
using MusicSchool.Models;
using MusicSchool.Responses;

namespace MusicSchool.Commands;

public record DeleteCategoryCommand(int Id) : IRequest<ApiResult<Category>>;
