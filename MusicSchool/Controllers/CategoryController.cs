using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;
using MusicSchool.Requests.Category;
using MusicSchool.Responses;

namespace MusicSchool.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly MusicSchoolDBContext _context;

    public CategoryController(MusicSchoolDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetCategory()
    {
        var categories = await _context.Category
            .OrderBy(c => c.CategoryName)
            .Select(c => new CategoryResponse(c.Id, c.CategoryName))
            .ToListAsync();

        return Ok(categories);
    }

    // GET: api/Category/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryResponse>> GetCategory(int id)
    {
        var category = await _context.Category
                .Include(c => c.Instruments)
                .SingleOrDefaultAsync(x => x.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(new CategoryResponse(category.Id, category.CategoryName, category.Instruments));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Category>> UpdateCategory(int id, [FromBody] UpdateCategory request)
    {
        var category = await _context.Category
            .SingleOrDefaultAsync(x => x.Id == id);

        if (category == null)
        {
            return BadRequest("Id not found");

        }
        
        category.CategoryName = request.NewCategoryName;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest("The database was not updated");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An unexpected error occurred: {e.Message}");
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        var existingCategory = await _context.Category
            .SingleOrDefaultAsync(c => c.CategoryName == request.CategoryName);

        if (existingCategory != null)
        {
            return BadRequest("Category already exists");
        }

        var newCategory = new Category { CategoryName = request.CategoryName };
        _context.Category.Add(newCategory);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest("The database was not updated");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An unexpected error occurred: {e.Message}");
        }

        return CreatedAtAction(nameof(GetCategory), new { id = newCategory.Id }, newCategory);
    }
}
