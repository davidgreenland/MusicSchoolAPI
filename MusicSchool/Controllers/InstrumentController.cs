using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;
using MusicSchool.Requests.Instrument;
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
    public async Task<ActionResult<Instrument>> UpdateInstrument(int id, [FromBody] UpdateInstrumentPut request)
    {
        if (!await CategoryExistsAsync(request.NewCategoryId)) // foreign key
        {
            return NotFound($"Category: {request.NewCategoryId} does not exist");
        }

        if (await InstrumentExistsAsync(request.NewInstrumentName))
        {
            return Conflict($"Instrument: {request.NewInstrumentName}, is already in the database");
        }

        var instrument = await _context.Instrument
            .SingleOrDefaultAsync(x => x.Id == id);

        if (instrument == null)
        {
            return NotFound("Id not found");
        }

        instrument.Name = request.NewInstrumentName;
        instrument.CategoryId = request.NewCategoryId;
        await _context.SaveChangesAsync();

        var newInstrument = new Instrument { 
            Name = request.NewInstrumentName,
            CategoryId = request.NewCategoryId,
        };

        _context.Instrument.Add(newInstrument);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetInstrument), new { id = newInstrument.Id }, newInstrument);
    }

    // POST: api/Instrument
    [HttpPost]
    public async Task<ActionResult<Instrument>> CreateInstrument([FromBody] CreateInstrumentRequest request)
    {
        var existingInstrument = await _context.Instrument
            .Where(x => x.CategoryId == request.CategoryId)
            .SingleOrDefaultAsync(x => x.Name == request.Name);

        if (existingInstrument != null)
        {
            return Conflict($"Instrument {request.Name} already exists");
        }

        var newInstrument = new Instrument
        {
            Name = request.Name,
            CategoryId = request.CategoryId,
        };

        _context.Instrument.Add(newInstrument);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetInstrument), new { id = newInstrument.Id }, newInstrument);
    }

    // DELETE: api/Instrument/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteInstrument(int id)
    {
        var instrument = await _context.Instrument
            .SingleOrDefaultAsync(x => x.Id == id);

        if (instrument == null)
        {
            return NotFound("Instrument Id not found");
        }

        var studentIsPlayingInstrument = await _context.Student
            .Include(s => s.Instruments)
            .AnyAsync(x => x.Instruments.Any(x => x.Id == id)); 

        if (studentIsPlayingInstrument)
        {
            return Conflict($"Unable to delete instrument");
        }

        _context.Instrument.Remove(instrument);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> CategoryExistsAsync(int categoryId)
    {
        return await _context.Category.AnyAsync(x => x.Id == categoryId);
    }

    private async Task<bool> InstrumentExistsAsync(string name)
    {
        return await _context.Instrument.AnyAsync(x => x.Name == name);
    }
}
