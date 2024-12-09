using MediatR;
using MusicSchool.Responses;

namespace MusicSchool.Queries;

public record GetCategoryByIdQuery(int Id) : IRequest<CategoryResponse>;
