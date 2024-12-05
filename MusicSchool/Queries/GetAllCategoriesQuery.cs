using MediatR;
using MusicSchool.Responses;

namespace MusicSchool.Queries;

public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryResponse>>
{
}
