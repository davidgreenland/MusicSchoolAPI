using MediatR;
using MusicSchool.Responses;

namespace MusicSchool.Queries;

public class GetCategoryByIdQuery : IRequest<CategoryResponse>
{
    public int Id { get; set; }

    public GetCategoryByIdQuery(int id)
    {
        Id = id;
    }
}
