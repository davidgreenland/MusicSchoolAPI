using MediatR;
using MusicSchool.Models;
using MusicSchool.Responses;

namespace MusicSchool.Commands;

public record UpdateCategoryCommand(int Id, string NewName) : IRequest<ApiResult<Category>>;
