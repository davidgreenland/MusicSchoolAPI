using MediatR;
using MusicSchool.Commands.CategoryCommands;
using MusicSchool.Exceptions;
using MusicSchool.Models;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Handlers.CategoryHandlers;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryByIdCommand>
{
    private readonly ICategoryService _categoryService;

    public DeleteCategoryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(request.Id) ?? throw new NotFoundException($"Category {request.Id} not found");

        if (await _categoryService.CategoryHasInstrument(request.Id))
        {
            throw new DeleteEntityConflict("Unable to delete category");
        }

        await _categoryService.DeleteAsync(category);

        return; // todo check
    }
}
