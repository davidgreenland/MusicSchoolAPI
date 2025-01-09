using MediatR;
using MusicSchool.Commands.CategoryCommands;
using MusicSchool.Exceptions;
using MusicSchool.Models;
using MusicSchool.Services.Interfaces;

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
        var category = await _categoryService.GetCategoryByIdAsync(request.Id) ?? throw new NotFoundException($"Category {request.Id} not found");

        if (await _categoryService.CheckCategoryExistsAsync(request.NewName))
        {
            throw new NameConflictException(request.NewName);
        }

        category.Name = request.NewName;
        await _categoryService.CommitAsync();

        return category;
    }
}
