using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            .Select(c => new CategoryResponse(c.Id, c.CategoryName, null))
            .ToListAsync();

        return Ok(categories);
    }

    // GET: api/Category/5
    [HttpGet("{id}")]
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
}
