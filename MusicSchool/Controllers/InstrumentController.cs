using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;
using MusicSchool.Responses;

namespace MusicSchool.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstrumentController : ControllerBase
{
    private readonly MusicSchoolDBContext _context;

    public InstrumentController(MusicSchoolDBContext context)
    {
        _context = context;
    }

    // GET: api/Instrument
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InstrumentResponse>>> GetInstruments()
    {
        var instruments = await _context.Instrument
            .OrderBy(s => s.Name)
            .Select(x => new InstrumentResponse(x.Id, x.Name, x.Category.CategoryName))
            .ToListAsync();

        return Ok(instruments);
    }

    // GET: api/Instrument/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<InstrumentResponse>> GetInstrument(int id)
    {
        var instrument = await _context.Instrument
            .Include(i => i.Category)
            .Include(i => i.Students)
            .SingleOrDefaultAsync(i => i.Id == id);

        if (instrument == null)
        {
            return NotFound();
        }

        return Ok(new InstrumentResponse(instrument.Id, instrument.Name, instrument.Category.CategoryName, instrument.Students));
    }

    // PUT: api/Instrument/1
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Instrument>> UpdateInstrument(int id, [FromBody] UpdateInstrument? request)
    {
        if (request == null)
        {
            return BadRequest("Request body is missing.");
        }

        if (!await CategoryExists(request.NewCategoryId))
        {
            return BadRequest($"Category {request.NewCategoryId} does not exist");
        }

        var instrument = await _context.Instrument
            .SingleOrDefaultAsync(x => x.Id == id);

        if (instrument == null)
        {
            return BadRequest("Id not found");
        }

        instrument.Name = request.NewInstrumentName;
        instrument.CategoryId = request.NewCategoryId;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateException)
        { 
            return BadRequest("The database was not updated");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An unexpected error occurred: {e.Message}");
        }

        return Ok(instrument);
    }

    private async Task<bool> CategoryExists(int categoryId)
    {
        return await _context.Category.AnyAsync(c => c.Id == categoryId);
    }
}
