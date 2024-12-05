using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MusicSchool.Models;
using MusicSchool.Queries;
using MusicSchool.Requests.Category;
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

    //[HttpPut("{id:int}")]
    //public async Task<ActionResult<Category>> UpdateCategory(int id, [FromBody] UpdateCategory request)
    //{
    //    var response = await _categoryService.UpdateCategoryAsync(id, request);

    //    return HandleApiResponse(response);
    //}

    //    // POST: api/category
    //    [HttpPost]
    //    public async Task<ActionResult<Category>> CreateCategory([FromBody] CreateCategoryRequest request)
    //    {
    //        var response = await _categoryService.CreateCategoryAsync(request);

    //        return HandleApiResponse(response);
    //    }

    //    // DELETE: api/category/{id}
    //    [HttpDelete("{id:int}")]
    //    public async Task<ActionResult> RemoveCategory(int id)
    //    {
    //        var response = await _categoryService.DeleteCategoryAsync(id);

    //        return HandleApiResponse(response);
    //    }

    private ObjectResult HandleApiResponse<T>(ApiResponse<T> response) where T : class
    {
        return response.IsSuccess
            ? StatusCode(response.StatusCode, response.Data)
            : StatusCode(response.StatusCode, response.Message);
    }
}
