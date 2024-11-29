using Microsoft.AspNetCore.Mvc;
using MusicSchool.Models;
using MusicSchool.Requests.Category;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetCategory()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        return Ok(categories);
    }

    // GET: api/Category/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryResponse>> GetCategory(int id)
    {
        var response = await _categoryService.GetCategoryAsync(id);

        return response.IsSuccess
            ? Ok(response.Data) : NotFound();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Category>> UpdateCategory(int id, [FromBody] UpdateCategory request)
    {
        var response = await _categoryService.UpdateCategoryAsync(id, request);

        return response.IsSuccess
            ? Ok(response.Data)
            : StatusCode(response.StatusCode, response.Message);
    }

    // POST: api/category
    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        var response = await _categoryService.CreateCategoryAsync(request);

        return response.IsSuccess
            ? StatusCode(response.StatusCode, response.Data)
            : StatusCode(response.StatusCode, response.Message);
    }

    // DELETE: api/category/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> RemoveCategory(int id)
    {
        var response = await _categoryService.DeleteCategoryAsync(id);

        return response.IsSuccess
            ? NoContent()
            : StatusCode(response.StatusCode, response.Message);
    }
}
