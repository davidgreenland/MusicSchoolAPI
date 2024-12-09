using MediatR;
using MusicSchool.Commands.CategoryCommands;
using MusicSchool.Models;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Handlers.CategoryHandlers;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryByIdCommand, ApiResult<Category>>
{
    private readonly ICategoryService _categoryService;

    public DeleteCategoryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<ApiResult<Category>> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(request.Id);
        if (category == null)
        {
            return new ApiResult<Category>(HttpStatusCode.NotFound, $"Category ID {request.Id} not found");
        }
        if (await _categoryService.CategoryHasInstrument(request.Id))
        {
            return new ApiResult<Category>(HttpStatusCode.Conflict, "Unable to delete category");
        }

        await _categoryService.DeleteAsync(category);

        return new ApiResult<Category>(HttpStatusCode.NoContent, data: null);
    }
}
