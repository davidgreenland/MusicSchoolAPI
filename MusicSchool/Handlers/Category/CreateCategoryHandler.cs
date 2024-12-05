using MediatR;
using MusicSchool.Models;
using MusicSchool.Commands;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Handlers;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, ApiResult<Category>>
{
    private readonly ICategoryService _categoryService;

    public CreateCategoryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<ApiResult<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetAllCategoriesAsync();
        if (await _categoryService.CheckCategoryExistsAsync(request.Name))
        {
            return new ApiResult<Category>(HttpStatusCode.Conflict, $"Category with name {request.Name} already exists");
        }

        var newCategory = new Category { Name = request.Name };

        await _categoryService.InsertAsync(newCategory);
        await _categoryService.CommitAsync();

        return new ApiResult<Category>(HttpStatusCode.OK, newCategory);
    }
}