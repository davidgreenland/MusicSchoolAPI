using MediatR;
using MusicSchool.Commands;
using MusicSchool.Models;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Handlers.CategoryHandlers;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, ApiResult<Category>>
{
    private readonly ICategoryService _categoryService;

    public UpdateCategoryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<ApiResult<Category>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(request.Id);
        if (category == null)
        {
            return new ApiResult<Category>(HttpStatusCode.NotFound, $"Category ID {request.Id} not found");
        }
        if (await _categoryService.CheckCategoryExistsAsync(request.NewName))
        {
            return new ApiResult<Category>(HttpStatusCode.Conflict, $"Category with name {request.NewName} already exists");
        }

        category.Name = request.NewName;
        await _categoryService.CommitAsync();

        return new ApiResult<Category>(HttpStatusCode.OK, category);
    }
}
