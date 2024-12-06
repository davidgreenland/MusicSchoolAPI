using MediatR;
using MusicSchool.Commands;
using MusicSchool.Models;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Handlers;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, ApiResult<Category>>
{
    private readonly ICategoryService _categoryService;

    public DeleteCategoryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<ApiResult<Category>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(request.Id);
        if (category == null)
        { 
            return new ApiResult<Category>(HttpStatusCode.NotFound, $"Instrument {request.Id} not found");
        }
        if (await _categoryService.CategoryHasInstrument(request.Id))
        {
            return new ApiResult<Category>(HttpStatusCode.Conflict, "Unable to delete category");
        }

        await _categoryService.DeleteAsync(category);

        return new ApiResult<Category>(HttpStatusCode.NoContent, data: null);
    }
}
