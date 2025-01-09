using MediatR;
using MusicSchool.Queries;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Handlers.CategoryHandlers;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponse?>
{
    private readonly ICategoryService _categoryService;

    public GetCategoryByIdHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<CategoryResponse?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetCategoryByIdAsync(request.Id);

        return result == null
            ? null
            : new CategoryResponse(result.Id, result.Name, result.Instruments!);
    }
}
