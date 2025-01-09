using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSchool.Commands.CategoryCommands;
using MusicSchool.Models;
using MusicSchool.Queries;
using MusicSchool.Requests.CategoryRequests;
using MusicSchool.Responses;

namespace MusicSchool.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAllCategories()
    {
        var categories = await _mediator.Send(new GetAllCategoriesQuery());

        return Ok(categories);
    }

    // GET: api/Category/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryResponse>> GetCategoryById(int id)
    {
        var category = await _mediator.Send(new GetCategoryByIdQuery(id));

        return category == null
            ? NotFound("Id not found")
            : Ok(category);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Category>> UpdateCategory(int id, [FromBody] UpdateCategory request)
    {
        var category = await _mediator.Send(new UpdateCategoryCommand(id, request.NewCategoryName));

        return Ok(category);
    }

    // POST: api/category
    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        var category = await _mediator.Send(new CreateCategoryCommand(request.Name));

        return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
    }

    // DELETE: api/category/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> RemoveCategory(int id)
    {
        await _mediator.Send(new DeleteCategoryByIdCommand(id));

        return NoContent();
    }
}
