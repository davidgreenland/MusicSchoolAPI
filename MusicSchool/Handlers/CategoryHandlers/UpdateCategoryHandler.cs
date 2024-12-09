using MediatR;
using MusicSchool.Commands.CategoryCommands;
using MusicSchool.Exceptions;
using MusicSchool.Models;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Handlers.CategoryHandlers;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Category>
{
    private readonly ICategoryService _categoryService;

    public UpdateCategoryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(request.Id) ?? throw new CategoryNotFoundException(request.Id);

        if (await _categoryService.CheckCategoryExistsAsync(request.NewName))
        {
            throw new EntityNameConflictException(request.NewName);
        }

        category.Name = request.NewName;
        await _categoryService.CommitAsync();

        return category;
    }
}
