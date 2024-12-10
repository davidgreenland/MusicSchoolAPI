using MediatR;
using MusicSchool.Models;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using MusicSchool.Commands.CategoryCommands;
using MusicSchool.Exceptions;

namespace MusicSchool.Handlers.CategoryHandlers;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Category>
{
    private readonly ICategoryService _categoryService;

    public CreateCategoryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await _categoryService.CheckCategoryExistsAsync(request.Name))
        {
            throw new NameConflictException(request.Name);
        }

        var newCategory = new Category { Name = request.Name };

        await _categoryService.InsertAsync(newCategory);

        return newCategory;
    }
}